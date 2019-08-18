using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Interfaces;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Services
{
    public class EmojiService : IEmojiService
    {
        private IUnitOfWork _dbcontext;
        public EmojiService(IUnitOfWork context) { _dbcontext = context; }

        public async Task AddEmoji(EmojiDTO emoji)
        {
            if (emoji.Source == null || emoji.Source.Length > 0)
                throw new ArgumentException("Emoji picure cannot be empty!");

            if (_dbcontext.Emojis.GetByCondition(e => e.Name == emoji.Name).FirstOrDefault() == null)
            {
                await _dbcontext.Emojis.CreateAsync(new EmojiMark {
                    Name = emoji.Name,
                    Source = emoji.Source
                });

                await _dbcontext.SaveChangesAsync();
            }
            else
                throw new ArgumentException("Emoji with this name already exists!");
        }

        public async Task<IEnumerable<EmojiDTO>> GetAllEmojies()
        {
            Queue<EmojiDTO> emojis = new Queue<EmojiDTO>();
            IEnumerable<EmojiMark> emojiMarks = await _dbcontext.Emojis.GetAllAsync();

            foreach (var emoji in emojiMarks)
                emojis.Enqueue(new EmojiDTO {
                    Name = emoji.Name,
                    Source = emoji.Source
                });

            return emojis;
        }

        public async Task<IEnumerable<EmojiDTO>> GetAllEmojiesFromPost(PostDTO post)
        {
            Queue<EmojiDTO> emojis = new Queue<EmojiDTO>();
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (seekedPost == null)
                return null;

            IEnumerable<PostsEmojiMark> postsEmojiMarks = seekedPost.PostsEmojiMarks;

            foreach (var emoji in postsEmojiMarks)
            {
                emojis.Enqueue(new EmojiDTO {
                    Name = emoji.EmojiMarkNav.Name,
                    Source = emoji.EmojiMarkNav.Source
                });
            }

            return emojis;
        }

        public async Task<IEnumerable<GroupedEmojiDTO>> GetAmmoutOfEmojiesOnPost(PostDTO post)
        {
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            IEnumerable<EmojiMark> marks = await _dbcontext.Emojis.GetAllAsync();

            if (seekedPost == null)
                return null;

            var EmojiMarkCount = seekedPost.PostsEmojiMarks.GroupBy(g => g.EmojiName).Select(g => new { EmojiName = g.Key, EmojiCount = g.Count() });
            return EmojiMarkCount.Join(marks,
                e => e.EmojiName,
                m => m.Name,
                (count, emoji) => new GroupedEmojiDTO
                {
                    Emoji = new EmojiDTO { Name = emoji.Name, Source = emoji.Source },
                    Count = count.EmojiCount
                });
        }

        public async Task<IEnumerable<UsersEmojiDTO>> GetEmojiesAndUsersOnPost(PostDTO post)
        {
            Queue<UsersEmojiDTO> result = new Queue<UsersEmojiDTO>();
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (seekedPost == null)
                return null;

            IEnumerable <PostsEmojiMark> postsEmojiMarks = seekedPost.PostsEmojiMarks;

            foreach (var mark in postsEmojiMarks)
                result.Enqueue(new UsersEmojiDTO
                {
                    Emoji = new EmojiDTO { Name = mark.EmojiMarkNav.Name, Source = mark.EmojiMarkNav.Source },
                    Username = (mark.UserId == null) ? null : mark.UserNav.Nickname
                }) ;

            return result;
        }

        public async Task MarkEmojiToPost(EmojiDTO emoji, PostDTO post, UserDTO user)
        {
            PhotoPost postToMark = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            EmojiMark mark = await _dbcontext.Emojis.GetByKeyAsync(emoji.Name);
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User userThatPostsEmoji = users.FirstOrDefault();

            if (userThatPostsEmoji == null)
                throw new ArgumentException("This user doesn't exist!");

            if (mark == null)
                throw new ArgumentException("This emoji doesn't exist!");

            if (postToMark == null)
                throw new ArgumentException("This post doesn't exist!");


            postToMark.PostsEmojiMarks.Add(new PostsEmojiMark
            {
                EmojiName = mark.Name,
                PhotoPostId = postToMark.Id,
                UserId = userThatPostsEmoji.Id
            });

            await _dbcontext.SaveChangesAsync();
        }

        public async Task RemoveEmojiToPost(EmojiDTO emoji, PostDTO post, UserDTO user)
        {
            PhotoPost postToMark = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            EmojiMark mark = await _dbcontext.Emojis.GetByKeyAsync(emoji.Name);
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User userThatPostsEmoji = users.FirstOrDefault();

            if (mark == null)
                throw new ArgumentException("This emoji doesn't exist!");

            if (postToMark == null)
                throw new ArgumentException("This post doesn't exist!");

            IEnumerable<PostsEmojiMark> postMarks = postToMark.PostsEmojiMarks.Where(
                p => p.EmojiName == mark.Name && 
                p.PhotoPostId == postToMark.Id &&
                p.UserId == userThatPostsEmoji?.Id);

            if (postMarks.FirstOrDefault() != null)
                postToMark.PostsEmojiMarks.Remove(postMarks.FirstOrDefault());

            await _dbcontext.SaveChangesAsync();
        }

        public async Task RemoveEmoji(EmojiDTO emoji)
        {
            EmojiMark emojiToDelete = await _dbcontext.Emojis.GetByKeyAsync(emoji.Name);

            if (emojiToDelete != null)
                _dbcontext.Emojis.Delete(emojiToDelete);

            await _dbcontext.SaveChangesAsync();
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
