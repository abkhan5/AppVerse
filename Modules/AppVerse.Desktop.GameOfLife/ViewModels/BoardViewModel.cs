#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;

#endregion
namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public   class BoardViewModel : BaseViewModel
    {
        #region Private members


     

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public BoardViewModel(IUnityContainer unityContainer):base(unityContainer)
        {
        }

        protected override void Initialize()
        {

        }
        #endregion


    }
}
