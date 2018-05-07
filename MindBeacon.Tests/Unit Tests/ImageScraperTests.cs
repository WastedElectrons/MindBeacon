using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MindBeacon.Tests.Mocks;
using MindBeacon.Models;
using MindBeacon.Service;
using System.Linq;

namespace MindBeacon.Tests.Unit_Tests
{
    [TestFixture]
    public class ImageScraperTests
    {
        [Test]
        public void Scrape_ImageFound_AddsOrUpdatesImage()
        {
            var testff = new TestFetchFunction();
            testff.AddImage(1);
            var mockImageRepo = new MockImageRepo();

            var scraper = new ImageScraper(testff.CreateFunc(), mockImageRepo.Object);
            SuppressAbort(() => scraper.Scrape(3));

            mockImageRepo.VerifyAddOrCreateCalled();
        }

        [Test]
        public void Scrape_ImageNotFound_DeletesImage()
        {
            var testff = new TestFetchFunction();
            testff.AddNotFound(1);
            var mockImageRepo = new MockImageRepo();

            var scraper = new ImageScraper(testff.CreateFunc(), mockImageRepo.Object);
            SuppressAbort(() => scraper.Scrape(3));

            mockImageRepo.VerifyDeleteCalled();
        }

        [Test]
        [MaxTime(1000)]
        public void Scrape_NotFoundCountExceded_Returns()
        {
            var testff = new TestFetchFunction();
            testff.AddNotFound(3);

            var scraper = new ImageScraper(testff.CreateFunc(), new MockImageRepo().Object);

            Assert.DoesNotThrow(() => SuppressAbort(() => scraper.Scrape(3)));
            testff.AssertActionsExausted();
        }

        [Test]
        [MaxTime(1000)]
        public void Scrape_ImageFound_NotFoundCountReset()
        {
            var testff = new TestFetchFunction();
            testff.AddNotFound(1);
            testff.AddImage(2);
            testff.AddNotFound(3);

            var scraper = new ImageScraper(testff.CreateFunc(), new MockImageRepo().Object);

            Assert.DoesNotThrow(() => SuppressAbort(() => scraper.Scrape(3)));
            testff.AssertActionsExausted();
        }

        private class TestFetchFunction
        {
            Queue<Func<int, Image>> _fetchFuncs = new Queue<Func<int, Image>>();

            public void AddImage(int times)
            {
                for (int i = 0; i < times; i++)
                {
                    _fetchFuncs.Enqueue(id => new Image() { Id = id });
                }
            }
                
            public void AddNotFound(int times)
            {
                for (int i = 0; i < times; i++)
                {
                    _fetchFuncs.Enqueue(id => throw new NotFoundException(nameof(Image)));
                }
            }
                
            public Func<int, Image> CreateFunc()
            {
                return id =>
                {
                    if (!_fetchFuncs.TryDequeue(out var func))
                        throw new AbortScrapeException();

                    return func(id);
                };
            }

            public void AssertActionsExausted()
            {
                CollectionAssert.IsEmpty(_fetchFuncs, "Scraper aborted before it should have.");
            }
        }

        private class AbortScrapeException : Exception
        {
            public AbortScrapeException() : base() { }
        }

        private static void SuppressAbort(Action action)
        {
            try
            {
                action();
            }
            catch (AbortScrapeException)
            {
                //Suppress Exception
            }
        }
    }
}
