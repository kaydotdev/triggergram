using System;
using System.IO;

using Microsoft.EntityFrameworkCore;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Contexts;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace PhotoAlbumDAL.Repositories
{
    /// <summary>
    /// 'Unit of work' provides set of repositories of 'POST_COMMENT', 'EMOJI_MARK', 'PHOTO', 'PHOTO_POST', 'SEARCH_TAG', 'USER', 'USER_ROLE' entites.
    /// Uses Entity Framework context as access point to database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private ApplicationContext _dbcontext;

        private ICommentRepository _commentRepository;
        private IEmojiRepository _emojiRepository;
        private IPhotoRepository _photoRepository;
        private IPostRepository _postRepository;
        private ISearchTagRepository _searchTagRepository;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// Configures connection to database via delegate of DbContextOptionsBuilder as input param
        /// Example: UOWConfigByBuilder(options => options. ...);
        /// </summary>
        /// <param name="optionsSetup">DbContextOptionsBuilder of ApplicationContext</param>
        public UnitOfWork(Func<DbContextOptionsBuilder<ApplicationContext>, DbContextOptionsBuilder<ApplicationContext>> optionsSetup)
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsSetup(optionsBuilder).Options;

            _dbcontext = new ApplicationContext(options);
        }

        /// <summary>
        /// Configures connection to database via full connection string as input param
        /// </summary>
        /// <param name="SQLServerConnectionString">Full connection string with Servername, Database e.t.c.</param>
        public UnitOfWork(string SQLServerConnectionString)
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlServer(SQLServerConnectionString).Options;

            _dbcontext = new ApplicationContext(options);
        }

        /// <summary>
        /// Configures connection to database via config file name and connection section as input param
        /// </summary>
        /// <param name="SQLServerConfigFile">Path to JSON configuration file</param>
        /// <param name="SQLServerConnectionName">Connection strings section name</param>
        public UnitOfWork(string SQLServerConfigFile, string SQLServerConnectionName)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(SQLServerConfigFile);
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString(SQLServerConnectionName);
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;

            _dbcontext = new ApplicationContext(options);
        }


        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_dbcontext);

                return _commentRepository;
            }
        }

        public IEmojiRepository Emojis
        {
            get
            {
                if (_emojiRepository == null)
                    _emojiRepository = new EmojiRepository(_dbcontext);

                return _emojiRepository;
            }
        }

        public IPhotoRepository Photos
        {
            get
            {
                if (_photoRepository == null)
                    _photoRepository = new PhotoRepository(_dbcontext);

                return _photoRepository;
            }
        }

        public IPostRepository Posts
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_dbcontext);

                return _postRepository;
            }
        }

        public ISearchTagRepository SearchTags
        {
            get
            {
                if (_searchTagRepository == null)
                    _searchTagRepository = new SearchTagRepository(_dbcontext);

                return _searchTagRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbcontext);

                return _userRepository;
            }
        }

        public IUserRoleRepository UserRoles
        {
            get
            {
                if (_userRoleRepository == null)
                    _userRoleRepository = new UserRoleRepository(_dbcontext);

                return _userRoleRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) { _dbcontext.Dispose(); }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges() { _dbcontext.SaveChanges(); }

        public async Task SaveChangesAsync() { await _dbcontext.SaveChangesAsync(); }
    }
}
