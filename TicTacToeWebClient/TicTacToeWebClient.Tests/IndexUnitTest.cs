using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeWebClient.Controllers;
using System.Web.Mvc;
using Moq;
using TicTacToeWebClient.Models;
using System.Collections.Generic;

namespace TicTacToeWebClient.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            // Arrange
            var mock = new Mock<GameInfoRepository>();
            mock.Setup(a => a.GetGameInfo()).Returns(new List<GameInfo>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }
    }
}
