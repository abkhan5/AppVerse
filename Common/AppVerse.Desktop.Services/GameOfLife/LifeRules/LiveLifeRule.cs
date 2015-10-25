#region MyRegion
using AppVerse.Desktop.Models.GameOfLife;
#endregion

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal  class LiveLifeRule : ILifeRule
    {
        public CellState EvaluateCell(Cell cell)
        {
            return CellState.Alive;
        }
    }
}
