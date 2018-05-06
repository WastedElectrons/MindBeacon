using MindBeacon.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindBeacon.Tests.Unit_Tests
{
    [TestFixture]
    public class InMemoryCacheImageRepoTests
    {
        [Test]
        public void AddOrUpdate_ImageDoesNotExist_AddsImage()
        {
            var repo = GetRepo();

            repo.AddOrUpdate(Image1);

            AssertImagesEqual(Image1, repo.GetAll().Single());
        }

        [Test]
        public void AddOrUpdate_ImageExists_UpdatesImage()
        {
            var repo = GetRepo();

            repo.AddOrUpdate(Image1);

            var newImage = Image1;
            newImage.Name = "new Name";
            repo.AddOrUpdate(newImage);

            AssertImagesEqual(newImage, repo.GetAll().Single());
        }

        [Test]
        public void AddOrUpdate_WithOtherImages_AddsImage()
        {
            var repo = GetRepo();

            repo.AddOrUpdate(Image2);
            repo.AddOrUpdate(Image1);

            AssertImagesEqual(Image1, repo.GetAll().Single(i => i.Id == Image1.Id));
        }

        [Test]
        public void AddOrUpdate_WithExistingAndOtherImages_UpdatesImage()
        {
            var repo = GetRepo();

            repo.AddOrUpdate(Image2);
            repo.AddOrUpdate(Image1);

            var newImage = Image1;
            newImage.Name = "new Name";
            repo.AddOrUpdate(newImage);

            AssertImagesEqual(newImage, repo.GetAll().Single(i => i.Id == Image1.Id));
        }

        [Test]
        public void Delete_ImageExists_RemovesImage()
        {
            var repo = GetRepo();
            repo.AddOrUpdate(Image1);
            repo.Delete(Image1.Id);
            CollectionAssert.IsEmpty(repo.GetAll());
        }

        [Test]
        public void Delete_ImageDoesNotExist_DoesNothing()
        {
            var repo = GetRepo();
            repo.AddOrUpdate(Image2);
            Assert.DoesNotThrow(() => repo.Delete(1));
            Assert.AreEqual(1, repo.GetAll().Count());
        }

        [Test]
        public void GetAll_RepoEmpty_ReturnsEmptyList()
        {
            var repo = GetRepo();
            Assert.AreEqual(0, repo.GetAll().Count());
        }

        [Test]
        public void GetAll_RepoNotEmpty_ReturnsRepoContents()
        {
            var repo = GetRepo();
            repo.AddOrUpdate(Image1);
            AssertImagesEqual(Image1, repo.GetAll().Single());
        }

        private void AssertImagesEqual(Image expected, Image actual)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
                Assert.AreEqual(expected.ImageUrl, actual.ImageUrl);
            });
        }

        private Image Image1 => new Image()
        {
            Id = 1,
            Name = "Image 1",
            CreatedAt = new DateTime(11222333),
            ImageUrl = "image.com/1"
        };

        private Image Image2 => new Image()
        {
            Id = 2,
            Name = "Image 2",
            CreatedAt = new DateTime(11222443),
            ImageUrl = "image.com/2"
        };

        private ImageRepo GetRepo()
        {
            return new Models.ImageRepoImpl.InMemoryCacheImageRepo();
        }
    }
}
