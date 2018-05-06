using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindBeacon.Models.ImageRepoImpl
{
    /// <summary>
    /// A repository that stores Images in an in memory cache.
    /// </summary>
    /// <remarks>
    ///     This repository has two obvious limitations:
    ///     
    ///     First, as all there is an obvious risk of an out of memory
    ///     exception as the number of images grows.
    ///     
    ///     Second, this cache is single instance.  Multiple instances
    ///     of this service will not be able to share the cache. This
    ///     will lead to excessive calls to the external service.
    ///     
    ///     This will work for demonstration purposes. Howerver, this
    ///     should be replaced with a repo pointing to a caching service
    ///     later.
    /// </remarks>
    public class InMemoryCacheImageRepo : ImageRepo
    {
        private readonly IDictionary<int, Image> _cache
            = new Dictionary<int, Image>();

        public void AddOrUpdate(Image image)
        {
            if (ImageExists(image.Id))
            {
                _cache[image.Id] = image;
            }
            else
            {
                _cache.Add(image.Id, image);
            }
        }

        public void Delete(int id)
        {
            if (ImageExists(id))
            {
                _cache.Remove(id);
            }
        }

        public List<Image> GetAll()
        {
            return _cache.Values.ToList();
        }

        private bool ImageExists(int id)
            => _cache.ContainsKey(id);
    }
}
