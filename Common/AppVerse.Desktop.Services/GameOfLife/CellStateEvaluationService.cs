#region Namespace
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using System.Linq;
#endregion
namespace AppVerse.Desktop.Services.GameOfLife
{
    public class CellStateEvaluationService: ICellStateEvaluationService
    {

        public void EvaluateBoardForNextGeneration(Board board)
        {
            var cells = board.Cells.SelectMany(cellRowItem => cellRowItem.Select(cellItem => cellItem)).ToList();
            foreach (Cell cellItem in cells)
            {

            }

        }
    }
}
