#region MyRegion
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using System.Linq;
#endregion

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal class RebirthLifeRule : ILifeRule
    {
        public void EvaluateCell(Cell cell)
        {
            if (cell.State==LifeState.Dead)
            {
                var liveCellCount = cell.NeighbouringCells.Count(cellItem => cellItem.State == LifeState.Alive);
                if (liveCellCount==3)
                {
                    cell.CalculatedState = LifeState.Alive;
                }
            }
        }
    }
}
