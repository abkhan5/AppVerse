#region Namespace
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using Microsoft.Practices.Unity;
#endregion
namespace AppVerse.Desktop.Services.GameOfLife
{
    public  class StillLifeEvaluationService: IStillLifeEvaluationService
    {

        #region Private member
        IUnityContainer _unityContainer;
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public StillLifeEvaluationService(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #endregion
    }
}
