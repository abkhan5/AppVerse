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
        public GameHistory _gameHistory;
        private Board  _gameBoardLayout;
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
            _gameHistory = new GameHistory();

            _gameHistory.ToatlColumns= 30;
            _gameHistory.TotalRows= 20;
            _gameHistory.TotalGenerations= 50;
            _canConfigureGrid = true;
            _gameStateMessage = StartSimulationMessage;
            _gameBoardLayout = _unityContainer.Resolve<Board>();
            _gameStopSubsriptionToken = AppEventAggregator.GetEvent<GameCompleteEvent>().Subscribe(GameCompleteEventHandler);
            TriggerGame = new DelegateCommand(TriggerGameAction, CanTriggerGame);
            ConfigureBoard();
        }

        private bool CanTriggerGame()
        {
            return NumberOfGenerations >=1;
        }

        private void GameCompleteEventHandler(GameHistory gameBoard)
        {
            CanConfigureGrid = true;
            GameStateMessage = StartSimulationMessage;
        }

        private void ConfigureBoard()
        {
            _gameBoardLayout.ConfigureBoard(_gameHistory.TotalRows, _gameHistory.ToatlColumns);
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
        public int NumberOfGenerations
        {
            get
            {

                return _gameHistory.TotalGenerations;
            }

            set
            {
                _gameHistory.TotalGenerations = value;
                RereshButtons();
            }
        }

        public int NumberOfRows
        {
            get
            {

                return _gameHistory.TotalRows;
            }

            set
            {
                _gameHistory.TotalRows = value;

                ConfigureBoard();
                RereshButtons();

            }
        }
        public int NumberOfColumns
        {
            get
            {

                return _gameHistory.ToatlColumns;
            }

            set
            {
                _gameHistory.ToatlColumns = value;

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
