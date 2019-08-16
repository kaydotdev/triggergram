using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Contexts;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    /// <summary>
    /// Default repository for 'PHOTO' entity.
    /// Supports sync and async repository methods.
    /// Type of record loading: explicit.
    /// </summary>
    public class PhotoRepository : IPhotoRepository
    {
        private ApplicationContext _dbcontext;
        public PhotoRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(Photo entity) { _dbcontext.Photos.Add(entity); }

        public async Task CreateAsync(Photo entity) { await _dbcontext.Photos.AddAsync(entity); }

        public void Delete(Photo entity) { _dbcontext.Photos.Remove(entity); }

        public async Task DeleteAsync(Photo entity) { await Task.Run(() => _dbcontext.Photos.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            Photo photo = _dbcontext.Photos.Find(key);
            if (photo != null) Delete(photo);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            Photo photo = await _dbcontext.Photos.FindAsync(key);
            if (photo != null) Delete(photo);
        }

        public IEnumerable<Photo> GetAll()
        {
            IEnumerable<Photo> photos = _dbcontext.Photos;

            foreach (var photo in photos)
                _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).Load();

            return photos;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            IEnumerable<Photo> photos = _dbcontext.Photos;

            foreach (var photo in photos)
                await _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).LoadAsync();

            return photos;
        }

        public IEnumerable<Photo> GetByCondition(Func<Photo, bool> predicate)
        {
            IEnumerable<Photo> photos = _dbcontext.Photos.Where(predicate);

            foreach (var photo in photos)
                _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).Load();

            return photos;
        }

        public async Task<IEnumerable<Photo>> GetByConditionAsync(Func<Photo, bool> predicate)
        {
            IEnumerable<Photo> photos = _dbcontext.Photos.Where(predicate);

            foreach (var photo in photos)
                await _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).LoadAsync();

            return photos;
        }

        public Photo GetByKey(int key)
        {
            Photo photo = _dbcontext.Photos.Find(key);

            if (photo != null)
                _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).Load();

            return photo;
        }

        public async Task<Photo> GetByKeyAsync(int key)
        {
            Photo photo = _dbcontext.Photos.Find(key);

            if (photo != null)
                await _dbcontext.Entry(photo).Reference(p => p.PhotoPostNav).LoadAsync();

            return photo;
        }

        public void Update(Photo entity) { _dbcontext.Photos.Update(entity); }

        public async Task UpdateAsync(Photo entity) { await Task.Run(() => _dbcontext.Photos.Update(entity)); }
    }
}
