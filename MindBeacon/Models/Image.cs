using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindBeacon.Models
{
    /// <summary>
    /// Describes a repository to manage Images.
    /// </summary>
    public interface ImageRepo
    {
        /// <summary>
        /// Fetches all images.
        /// </summary>
        /// <returns>A list of all images</returns>
        List<Image> GetAll();

        /// <summary>
        /// Adds an Image, or updates an Image if it exists in the repo.
        /// </summary>
        /// <param name="image">The image to Add or Update</param>
        void AddOrUpdate(Image image);

        /// <summary>
        /// Removes an image from the repository if it exists.
        /// </summary>
        /// <param name="id">The Id of the image to remove.</param>
        void Delete(int id);
    }

    /// <summary>
    /// A galary image model.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// The Id of the Image.  This is a FK to the origional metadata.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The time the Image was uploaded.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The name of the Image.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL the image can be found at.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// A repository to manage Image persistance.
        /// </summary>
        public static ImageRepo Repo
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
