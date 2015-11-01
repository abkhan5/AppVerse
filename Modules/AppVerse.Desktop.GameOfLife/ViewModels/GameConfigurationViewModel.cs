#region Namespace
using System;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.ApplicationEvents.GameOfLife;
using AppVerse.Desktop.Models.GameOfLife;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

#endregion


namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class GameConfigurationViewModel : BaseViewModel
    {
        #region Private members
        private GameHistory _gameHistory;
        private Board  _gameBoardLayout;
        private int? _numberOfRows;
        private int? _numberOfGenerations;
        private int? _numberOfColumns;
        private DelegateCommand _triggerGame;
        private bool _canConfigureGrid;
        private string _gameStateMessage;
        private SubscriptionToken _gameStopSubsriptionToken;
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
        public GameConfigurationViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            _numberOfColumns = 30;
            _numberOfRows = 20;
            _numberOfGenerations = 50;
            _canConfigureGrid = true;
            _gameStateMessage = StartSimulationMessage;
            _gameHistory = new GameHistory();
            _gameBoardLayout = _unityContainer.Resolve<Board>();
            _gameStopSubsriptionToken = AppEventAggregator.GetEvent<GameCompleteEvent>().Subscribe(GameCompleteEventHandler);
            TriggerGame = new DelegateCommand(TriggerGameAction, CanTriggerGame);
            ConfigureBoard();
        }

        private bool CanTriggerGame()
        {
            return NumberOfColumns.HasValue && NumberOfRows.HasValue && NumberOfGenerations.HasValue;
        }

        private void GameCompleteEventHandler(GameHistory gameBoard)
        {
            CanConfigureGrid = true;
            GameStateMessage = StartSimulationMessage;
        }

        private void ConfigureBoard()
        {
            _gameBoardLayout.ConfigureBoard(_numberOfRows.Value, _numberOfColumns.Value);
        }

        private void TriggerGameAction()
        {
            SetupGameHistory();

            if (CanConfigureGrid)
            {
                CanConfigureGrid = false;
                GameStateMessage = StopSimulationMessage;
                AppEventAggregator.GetEvent<GameStartEvent>().Publish(_gameHistory);
            }
            else
            {
                CanConfigureGrid = true;
                GameStateMessage = StartSimulationMessage;
                AppEventAggregator.GetEvent<GameStopEvent>().Publish(_gameHistory);
            }
        }

        private void SetupGameHistory()
        {
            var gameBoard = _gameBoardLayout.GetCopy();
            _gameHistory.GameBoard = gameBoard;
            _gameHistory.ToatlColumns = NumberOfColumns.Value;
            _gameHistory.TotalRows = NumberOfRows.Value;
            _gameHistory.TotalGenerations = NumberOfGenerations.Value;
        }

        #endregion

        #region Properties

        public DelegateCommand TriggerGame
        {
            get { return _triggerGame; }
            set { _triggerGame = value; }
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
        public int? NumberOfGenerations
        {
            get
            {

                return _numberOfGenerations;
            }

            set
            {
                SetProperty(ref _numberOfGenerations, value);
                RereshButtons();
            }
        }

        public int? NumberOfRows
        {
            get
            {

                return _numberOfRows;
            }

            set
            {
                SetProperty(ref _numberOfRows, value);
                ConfigureBoard();
                RereshButtons();

            }
        }
        public int? NumberOfColumns
        {
            get
            {

                return _numberOfColumns;
            }

            set
            {
                SetProperty(ref _numberOfColumns, value);
                ConfigureBoard();
                RereshButtons();
            }
        }
        public Board GameBoardLayout
        {
            get { return _gameBoardLayout; }
            set
            {
                SetProperty(ref _gameBoardLayout, value);
            }
        }

        private void RereshButtons()
        {
            TriggerGame.RaiseCanExecuteChanged();
        }

        #endregion

    }
}
