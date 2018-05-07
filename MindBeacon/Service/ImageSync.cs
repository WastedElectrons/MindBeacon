using MindBeacon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MindBeacon.Service
{
    /// <summary>
    /// Syncronizes the local image cache with the remote service.
    /// </summary>
    public class ImageSync
    {
        private void Sync()
        {
            var scraper = new ImageScraper(
                id =>
                {
                    var request = WebRequest.Create(
                        $"https://5ad8d1c9dc1baa0014c60c51.mockapi.io/api/br/v1/magic/{id}");

                    try
                    {
                        using (var response = request.GetResponse())
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                return Image.FromJson(reader.ReadToEnd());
                            }
                        }
                    }
                    catch (WebException wex)
                    {
                        var statusCode = (wex.Response as HttpWebResponse)?.StatusCode;
                        if (statusCode != null && statusCode == HttpStatusCode.NotFound)
                        {
                            throw new NotFoundException(nameof(Image));
                        }
                        throw;
                    }
                },
                Image.Repo);

            //TODO: Replace this magic number with something configurable.
            scraper.Scrape(20);
        }
       
        public static Task StartSync(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var syncer = new ImageSync();

                while (!cancellationToken.IsCancellationRequested)
                {
                    syncer.Sync();

                    await Task.Delay((int)TimeSpan.FromHours(1).TotalMilliseconds);
                }
            });
        }
    }
}
