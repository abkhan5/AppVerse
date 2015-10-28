using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.ModelsTest
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void StateTest()
        {
            var cell = new Cell(0,0,0,0);
            Assert.AreEqual(LifeState.Dead, cell.State);
            Assert.AreEqual(LifeState.Dead, cell.CalculatedState);

            cell.State = LifeState.Dead;
            Assert.AreEqual(LifeState.Dead, cell.State);
            Assert.AreEqual(LifeState.Dead, cell.CalculatedState);

            cell.CalculatedState = LifeState.Alive;
            Assert.AreEqual(LifeState.Dead, cell.State);
            Assert.AreEqual(LifeState.Alive, cell.CalculatedState);


            cell.State = cell.CalculatedState;
            Assert.AreEqual(LifeState.Alive, cell.State);
            Assert.AreEqual(LifeState.Alive, cell.CalculatedState);

        }
    }
}
