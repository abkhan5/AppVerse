using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

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
        private bool _canConfigureGrid;
        private string _gameStateMessage;
        #endregion

        #region Constant
        public const string StartSimulationMessage = "Start simulation";
        public const string StopSimulationMessage = "Stop simulation";
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
            _canConfigureGrid = true;
            _gameStateMessage = StartSimulationMessage;
            TriggerGame = new DelegateCommand(TriggerGameAction);
            BoardView = _unityContainer.Resolve<BoardViewModel>();
            ConfigureBoard();
        }


        private void ConfigureBoard()
        {
            BoardView.ConfigureBoard(NumberOfRows, NumberOfColumns, NumberOfGenerations);
        }

        private void TriggerGameAction()
        {
            if (CanConfigureGrid)
            {
                CanConfigureGrid = false;
                GameStateMessage = StopSimulationMessage;
                BoardView.RunGame();


            }
            else
            {
                CanConfigureGrid = true;
                GameStateMessage = StartSimulationMessage;
                BoardView.StopGame();

            }

        }


        private  void RunGame()
        {

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


        public bool CanConfigureGrid
        {
            get
            {

                return _canConfigureGrid;
            }

            set
            {
                SetProperty(ref _canConfigureGrid, value);

            }
        }
        public string GameStateMessage
        {
            get
            {

                return _gameStateMessage;
            }

            set
            {
                SetProperty(ref _gameStateMessage, value);

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
