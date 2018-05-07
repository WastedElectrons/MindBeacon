using MindBeacon.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindBeacon.Tests.Unit_Tests
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void FromJson_WithValidJson_ParsesObject()
        {
            int id = 12;
            DateTime createdAt = FromUnixTimestamp(1524158544L);
            string name = "name 12";
            string imageUrl = "https://unsplash.it/500?image=12";

            var json = "{\"id\":\"12\",\"createdAt\":1524158544,\"name\":\"name 12\",\"imageUrl\":\"https://unsplash.it/500?image=12\"}";

            Image image = null;

            Assert.DoesNotThrow(() => image = Image.FromJson(json));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(id, image.Id);
                Assert.AreEqual(createdAt, image.CreatedAt);
                Assert.AreEqual(name, image.Name);
                Assert.AreEqual(imageUrl, image.ImageUrl);
            });
        }

        private static DateTime FromUnixTimestamp(long timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
        }
    }
}
