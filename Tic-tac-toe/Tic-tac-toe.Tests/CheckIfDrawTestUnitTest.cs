using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tic_tac_toe.Tests
{
    [TestClass]
    public class CheckIfDrawTest
    {
        [TestMethod]
        public void CheckIfDrawFalse()
        {
            //Arrange
            Form1 form = new Form1();
            
            //Act
            bool res = form.checkIfDraw();

            //Assert
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void CheckIfDrawTrue()
        {
            //Arrange
            Form1 form = new Form1();

            //заполняем все клетки
            for (int i = 0; i < Settings.GRID_SIZE; i++) {
                for (int j = 0; j < Settings.GRID_SIZE; j++) {
                    form.cells[i, j].type = CellType.PC;
                }
            }

            //Act
            bool res = form.checkIfDraw();

            //Assert
            Assert.IsTrue(res);
        }
    }
}
