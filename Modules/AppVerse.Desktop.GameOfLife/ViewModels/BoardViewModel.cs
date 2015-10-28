#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.Models.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Threading.Tasks;
using System;

#endregion
namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {

        #region Constants
        private const string GenerationMessageFormat = "{0} of {1}";
        #endregion


        #region Private members
        private bool _isBoardEnabled;
        private string  _generationProgress;
        private Board _gameBoard;
        private int _numberOfGenerations;
        ICellStateEvaluationService _cellStateService;
        bool  _isGameCancelled;
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
            _isGameCancelled = false;
            _isBoardEnabled = true;
            GameBoard = new Board();
            _cellStateService = _unityContainer.Resolve<ICellStateEvaluationService>();
            _gameTask = new Task(RunGameInTask);
        }

        internal void ConfigureBoard(int numberOfRows, int numberOfColumns, int numberOfGenerations)
        {
            UpdateGenerationMessage(0);
            _numberOfGenerations = numberOfGenerations;
            GameBoard.ConfigureBoard(numberOfRows, numberOfColumns);
            OnPropertyChanged("GameBoard");
        }




        #endregion

        #region Properties

        public bool IsBoardEnabled
        {
            get { return _isBoardEnabled; }
            set
            {
                _isBoardEnabled = value;
                SetProperty(ref _isBoardEnabled, value);
            }
        }

        internal void StopGame()
        {
            
        }

        public Board GameBoard
        {
            get { return _gameBoard; }
            set
            {
                _gameBoard = value;
                SetProperty(ref _gameBoard, value);
            }
        }
        public string GenerationProgress
        {
            get { return _generationProgress; }
            set
            {
                _generationProgress = value;
                SetProperty(ref _generationProgress, value);
            }
        }
        Task _gameTask;
        public void RunGame()
        {
            _gameTask = new Task(RunGameInTask);

            IsBoardEnabled = false;
            UpdateGenerationMessage(1);
            _gameTask.Start();
        }

        private void UpdateGenerationMessage(int generationNumber)
        {
            if (generationNumber==0)
            {
                return;
            }
           GenerationProgress = string.Format(GenerationMessageFormat, generationNumber, _numberOfGenerations);

        }

        private void RunGameInTask()
        {

            try
            {
                for (int i = 0; i < _numberOfGenerations&& !_isGameCancelled; i++)
                {                    
                    _cellStateService.EvaluateBoardForNextGeneration(GameBoard);
                    UpdateGenerationMessage(i+1);
                    Thread.Sleep(200);
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
