#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.ApplicationEvents.GameOfLife;
using AppVerse.Desktop.Models.GameOfLife;
using Microsoft.Practices.Unity;
#endregion

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
   public class GameDetailsViewModel : BaseViewModel
    {
        #region Private members
        private RunningGameViewModel _boardViewModel;
        private GameConfigurationViewModel _gameConfiguration;
        private bool _isGameConfigurable;
        private bool _isGameBoardVisisble;
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
        public GameDetailsViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }
        #endregion

        #region Methods

        protected override void Initialize()
        {
            _isGameConfigurable = true;
            _isGameBoardVisisble = false;
            _gameConfiguration = _unityContainer.Resolve<GameConfigurationViewModel>();
            _boardViewModel = _unityContainer.Resolve<RunningGameViewModel>();
            AppEventAggregator.GetEvent<GameStartEvent>().Subscribe(GameStartEventHandler);
            AppEventAggregator.GetEvent<GameStopEvent>().Subscribe(GameStopEventHandler);
            AppEventAggregator.GetEvent<GameCompleteEvent>().Subscribe(GameStopEventHandler);
        }

        private void GameStopEventHandler(GameHistory obj)
        {
            IsGameBoardVisisble = false;
            IsGameConfigurable = true;
        }

        private void GameStartEventHandler(GameHistory obj)
        {
            IsGameBoardVisisble = true;
            IsGameConfigurable = false;
        }

        #endregion

        #region Properties
        public bool IsGameConfigurable
        {
            get
            {
                return _isGameConfigurable;
            }
            set
            {
                SetProperty(ref _isGameConfigurable, value);
            }
        }

        public bool IsGameBoardVisisble
        {
            get
            {
                return _isGameBoardVisisble;
            }
            set
            {
                SetProperty(ref _isGameBoardVisisble, value);
            }
        }


        public RunningGameViewModel BoardView
        {
            get
            {
                return _boardViewModel;
            }
            set
            {
                SetProperty(ref _boardViewModel, value);
            }
        }

        public GameConfigurationViewModel GameConfiguration
        {
            get
            {
                return _gameConfiguration;
            }
            set
            {
                SetProperty(ref _gameConfiguration, value);
            }
        }


        #endregion

    }
}
