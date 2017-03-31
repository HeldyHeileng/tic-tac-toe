using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tic_tac_toe.Tests
{
    [TestClass]
    public class GameInfoTest
    {
        [TestMethod]
        public void GetGameInfoIsNotNull()
        {
            // Arrange
            var mock = new Mock<Requester>();
            mock.Setup(a => a.GetRequest(It.IsAny<string>())).Returns(String.Empty);
            Form1 form = new Form1(mock.Object);

            // Act
            string result = form.requester.GetRequest("");

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
