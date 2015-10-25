using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.Services.GameOfLife
{
    public interface ILifeRule
    {
        CellState EvaluateCell(Cell cell);
    }
}
