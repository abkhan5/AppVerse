using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.Services.Interfaces.GameOfLife
{
    public interface ILifeRule
    {
        void EvaluateCell(Cell cell);
    }
}
