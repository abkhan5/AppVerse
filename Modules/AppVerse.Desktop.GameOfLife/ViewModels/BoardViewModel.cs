#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.Models.GameOfLife;
using Microsoft.Practices.Unity;

#endregion
namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        #region Private members

        private Board _gameBoard;


        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public BoardViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }

        protected override void Initialize()
        {
            GameBoard = new Board();
        }

        internal void ConfigureBoard(int numberOfRows, int numberOfColumns, int numberOfGenerations)
        {
            GameBoard.ConfigureBoard(numberOfRows, numberOfColumns);
            OnPropertyChanged("GameBoard");
        }




        #endregion

        #region Properties
        public Board GameBoard
        {
            get { return _gameBoard; }
            set
            {
                _gameBoard = value;
                SetProperty(ref _gameBoard, value);
            }
        }
        #endregion
    }
}
