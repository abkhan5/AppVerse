#region Namespace
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion
namespace AppVerse.Desktop.Services.GameOfLife
{
    public class CellStateEvaluationService : ICellStateEvaluationService
    {

        #region Private member
        IUnityContainer _unityContainer;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public CellStateEvaluationService(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        #endregion

        public void EvaluateBoardForNextGeneration(Board board)
        {
            var lifeRules = _unityContainer.Resolve<LifeRules>().GetRules();
            var cells = board.CellsInBoard();

            CalculateCellState(lifeRules, cells);

            NewMethod(cells);
        }

        private static void NewMethod(IEnumerable<Cell> cells)
        {
            Parallel.ForEach(cells, (cellItem =>
            {
                try
                {
                    cellItem.State = cellItem.CalculatedState;
                }
                catch (System.Exception)
                {

                    throw;
                }
            }));
        }

        private static void CalculateCellState(IEnumerable<ILifeRule> lifeRules, IEnumerable<Cell> cells)
        {
            Parallel.ForEach(cells, (cellItem =>
            {
                try
                {
                    foreach (var rule in lifeRules)
                    {
                        rule.EvaluateCell(cellItem);
                    }
                }
                catch (System.Exception ex)
                {

                    throw;
                }
            }));
        }
    }
}
