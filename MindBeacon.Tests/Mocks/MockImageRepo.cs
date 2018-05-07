using System;
using System.Collections.Generic;
using System.Text;
using MindBeacon.Models;
using Moq;

namespace MindBeacon.Tests.Mocks
{
    public class MockImageRepo : Mock<ImageRepo>
    {
        public void VerifyAddOrCreateCalled()
        {
            Verify(t => t.AddOrUpdate(It.IsAny<Image>()), Times.Once);
        }

        public void VerifyDeleteCalled()
        {
            Verify(t => t.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
