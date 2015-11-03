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
        private int _totalGeneration;
        private int _progress;
        private int _appTick;
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
            _appTick = 10;
            _progress = 1;
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
     

        public int Progress
        {
            get { return _progress; }
            set
            {
                SetProperty(ref _progress, value);
            }
        }


        public int TotalGeneration
        {
            get
            {
                return _totalGeneration;
            }
            set
            {
                SetProperty(ref _totalGeneration, value);
            }
        }
            public int AppTick
        {
            get
            {
                return _appTick;
            }
            set
            {
                SetProperty(ref _appTick, value);
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
            TotalGeneration = _gameHistory.TotalGenerations;
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

        private void RunGameInTask()
        {
            try
            {
                _gameHistory.BoardHistory.Add(_gameBoard.GetCopy());

                for (int i = 0; i < _gameHistory.TotalGenerations && !_isGameCancelled; i++)
                {
                    _cellStateService.EvaluateBoardForNextGeneration(_gameBoard);

                    _gameHistory.BoardHistory.Add(_gameBoard.GetCopy());
                    Progress++;
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
