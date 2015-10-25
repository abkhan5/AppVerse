#region Namespace
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;

#endregion
namespace AppVerse.Desktop.AppCommon.BaseClasses
{
    public abstract class BaseViewModel:BindableBase
    {
        #region Private memebers

        protected IUnityContainer _unityContainer;
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public BaseViewModel(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            Initialize();
        }
        #endregion


        #region methods
        protected abstract void Initialize();
        #endregion


    }
}
