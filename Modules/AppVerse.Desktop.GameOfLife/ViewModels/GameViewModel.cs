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
        private DelegateCommand _triggerGame;
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
            TriggerGame = new DelegateCommand(ConfigureBoard);
            BoardView = _unityContainer.Resolve<BoardViewModel>();
            ConfigureBoard();
        }


        private void ConfigureBoard()
        {
            BoardView.ConfigureBoard(NumberOfRows, NumberOfColumns, NumberOfGenerations);
            OnPropertyChanged("BoardView");
        }



        #endregion
        #region Properties

        public DelegateCommand TriggerGame
        {
            get { return _triggerGame; }
            set { _triggerGame = value; } }

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
                ConfigureBoard();

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
                ConfigureBoard();

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
                ConfigureBoard();

            }
        }

        #endregion

    }
}
