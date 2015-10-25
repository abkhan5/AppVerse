using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal  class OverPopulationLifeRule : ILifeRule
    {
        public CellState EvaluateCell(Cell cell)
        {
            return CellState.Alive;
        }
    }
}
