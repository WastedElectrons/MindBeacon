using MindBeacon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindBeacon.Service
{
    public class ImageScraper
    {
        private readonly Func<int, Image> _fetchImage;
        private readonly ImageRepo _imageRepo;

        public ImageScraper(
            Func<int, Image> fetchImage,
            ImageRepo imageRepo)
        {
            _fetchImage = fetchImage;
            _imageRepo = imageRepo;
        }

        public void Scrape(int maxNotFoundStreak)
        {
            int notFoundStreak = 0;

            for (int id = 0; notFoundStreak < maxNotFoundStreak; id++)
            {
                try
                {
                    _imageRepo.AddOrUpdate(_fetchImage(id));
                    notFoundStreak = 0;
                }
                catch (NotFoundException)
                {
                    _imageRepo.Delete(id);
                    notFoundStreak++;
                }
            }
        }
    }
}
