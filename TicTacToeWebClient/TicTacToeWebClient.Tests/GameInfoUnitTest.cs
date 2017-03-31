using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToeWebClient.Models;
using System.Collections.Generic;
using TicTacToeWebClient.Controllers;
using System.Web.Mvc;

namespace TicTacToeWebClient.Tests
{
    [TestClass]
    public class GameInfoUnitTest
    {
        [TestMethod]
        public void GetGameInfoNotNull()
        {
            // Arrange
            var mock = new Mock<GameInfoRepository>();
            mock.Setup(a => a.GetGameInfo()).Returns(new List<GameInfo>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            string result = controller.GetGameInfo();

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void SaveGameInfoTrue()
        {
            // Arrange
            var mock = new Mock<GameInfoRepository>();
            mock.Setup(a => a.SaveGameInfo(It.IsAny<GameInfo>()));
            HomeController controller = new HomeController(mock.Object);

            // Act
            bool result = controller.SaveGameInfo(It.IsAny<GameInfo>());

            // Assert
            Assert.IsTrue(result);
        }
    }
}
