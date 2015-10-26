using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Private members

        private BoardViewModel _boardViewModel;
        private int _numberOfRows;
        private int _numberOfGenerations;
        private int _numberOfColumns;
        private DelegateCommand _configureBoardCommand;
        #endregion



        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public GameViewModel(IUnityContainer unityContainer):base(unityContainer)
        {

        }
        #endregion
        #region Methods

        protected override void Initialize()
        {
            _numberOfColumns = 10;
            _numberOfRows= 10;
            _numberOfGenerations = 100;
            ConfigureBoardCommand = new DelegateCommand(ConfigureBoard,CanConfigureBoard);
            BoardView = _unityContainer.Resolve<BoardViewModel>();
            ConfigureBoard();
        }

        private bool CanConfigureBoard()
        {

            if (NumberOfColumns>0&&NumberOfRows>0&NumberOfGenerations>0)
            {
                return true;
            }
            return false;
        }

        private void ConfigureBoard()
        {
            BoardView.ConfigureBoard(NumberOfRows, NumberOfColumns, NumberOfGenerations);
        }



        #endregion
        #region Properties

        public DelegateCommand ConfigureBoardCommand
        {
            get { return _configureBoardCommand; }
            set { _configureBoardCommand = value; } }

        public BoardViewModel BoardView
        {
            get { return _boardViewModel; }
            set
            {
                SetProperty(ref _boardViewModel, value);
            }
        }


        public int NumberOfGenerations
        {
            get
            {

                return _numberOfGenerations;
            }

            set
            {
                SetProperty(ref _numberOfGenerations, value);
            }
        }

        public int NumberOfRows
        {
            get
            {

                return _numberOfRows;
            }

            set
            {
                SetProperty(ref _numberOfRows, value);
            }
        }
        public int NumberOfColumns
        {
            get
            {

                return _numberOfColumns;
            }

            set
            {
                SetProperty(ref _numberOfColumns, value);
            }
        }

        #endregion

    }
}
