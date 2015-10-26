#region MyRegion
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
#endregion

namespace AppVerse.Desktop.Services.GameOfLife
{
    internal  class OverPopulationLifeRule : ILifeRule
    {
        public LifeState EvaluateCell(Cell cell)
        {
            return LifeState.Alive;
        }
    }
}
