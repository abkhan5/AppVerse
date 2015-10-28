#region MyRegion
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using System.Linq;
#endregion

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal class UnderPopulationLifeRule : ILifeRule
    {
        public void EvaluateCell(Cell cell)
        {
            if (cell.State == LifeState.Alive)
            {
                var liveCellCount = cell.NeighbouringCells.Count(cellItem => cellItem.State == LifeState.Alive);
                if (liveCellCount < 2)
                {
                    cell.CalculatedState = LifeState.Dead;
                }
            }
        }
    }
}
