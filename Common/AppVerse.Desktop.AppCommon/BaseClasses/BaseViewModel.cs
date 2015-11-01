#region Namespace
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

#endregion
namespace AppVerse.Desktop.AppCommon.BaseClasses
{
    public abstract class BaseViewModel : BindableBase
    {
        #region Private memebers

        protected IUnityContainer _unityContainer;
        private IEventAggregator _evenAggregator;
        protected ILoggerFacade _logger;
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

        #region Properties
        public IEventAggregator AppEventAggregator
        {
            get
            {
                return _evenAggregator ?? (_evenAggregator = _unityContainer.Resolve<IEventAggregator>());
            }
        }
        #endregion

        #region methods
        protected abstract void Initialize();
        #endregion


    }
}
