using AppVerse.Desktop.Models.GameOfLife;

namespace AppVerse.Desktop.Services.Interfaces.GameOfLife
{
    public interface ICellStateEvaluationService
    {
        void EvaluateBoardForNextGeneration(Board board);
    }
}
