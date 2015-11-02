#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.ApplicationEvents.GameOfLife;
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
  public   class RunningGameViewModel : BaseViewModel
    {

        #region Constants
        private const string GenerationMessageFormat = "{0} of {1}";
        #endregion


        #region Private members
        private string _generationProgress;
        private Board _gameBoard;
        private ICellStateEvaluationService _cellStateService;
        private bool _isGameCancelled;
        private SubscriptionToken _gameStartSubsriptionToken;
        private SubscriptionToken _gameStopSubsriptionToken;
        private Task _gameTask;
        private CancellationToken _cancellationToken;
        private GameHistory _gameHistory;
        #endregion


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public RunningGameViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }

        protected override void Initialize()
        {
            _isGameCancelled = false;
            _gameStartSubsriptionToken = AppEventAggregator.GetEvent<GameStartEvent>().Subscribe(GameStartEventHandler);
            _gameStopSubsriptionToken = AppEventAggregator.GetEvent<GameStopEvent>().Subscribe(GameStopEventHandler);
            _cellStateService = _unityContainer.Resolve<ICellStateEvaluationService>();
            _cancellationToken = new CancellationToken();
            _gameTask = new Task(RunGameInTask, _cancellationToken);
        }


        #endregion

        #region Properties

        
        public Board GameBoard
        {
            get { return _gameBoard; }
            set
            {
                SetProperty(ref _gameBoard, value);
            }
        }
        public string GenerationProgress
        {
            get { return _generationProgress; }
            set
            {
                SetProperty(ref _generationProgress, value);
            }
        }

        #endregion


        #region Methods

        internal void StopGame()
        {
            _isGameCancelled = true;
            
        }


        private void GameStartEventHandler(GameHistory gameHistory)
        {
            _gameHistory = gameHistory;
            GameBoard = _gameHistory.GameBoard;
            RunGame();

        }

        private void GameStopEventHandler(GameHistory gameHistory)
        {
            _isGameCancelled = true;
        }



        public void RunGame()
        {
            _gameTask = new Task(RunGameInTask);
            _isGameCancelled = false;
            _gameBoard.RelateCellNeighbours();
            _gameTask.Start();
        }

        private void UpdateGenerationMessage(int generationNumber)
        {
            if (generationNumber == 0)
            {
                return;
            }
            GenerationProgress = string.Format(GenerationMessageFormat, generationNumber, _gameHistory.TotalGenerations);
        }

        private void RunGameInTask()
        {
            try
            {
                for (int i = 0; i < _gameHistory.TotalGenerations && !_isGameCancelled; i++)
                {
                    _cellStateService.EvaluateBoardForNextGeneration(_gameBoard);
                    UpdateGenerationMessage(i + 1);
                    Thread.Sleep(200);
                }
                AppEventAggregator.GetEvent<GameCompleteEvent>().Publish(_gameHistory);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
