#region MyRegion
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
#endregion

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal class UnderPopulationLifeRule : ILifeRule
    {
        public CellState EvaluateCell(Cell cell)
        {
            return CellState.Alive;
        }
    }
}
