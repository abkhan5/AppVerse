using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.Services.Interfaces.GameOfLife
{
    public interface ILifeRule
    {
        CellState EvaluateCell(Cell cell);
    }
}
