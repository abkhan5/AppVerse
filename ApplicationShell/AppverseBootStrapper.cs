#region Namespace
using AppVerse.Desktop.Services.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Unity;
using AppVerse.Desktop.Services.Interfaces.Navigation;
using AppVerse.Desktop.Services.Navigation;
#endregion
namespace AppVerse.Desktop.ApplicationShell
{
    public class AppverseBootStrapper : UnityBootstrapper
    {
                
        protected override DependencyObject CreateShell()
        {
            RegisterContainer();
            // Use the container to create an instance of the shell.
            AppverseShellView view = this.Container.TryResolve<AppverseShellView>();
            return view;
        }


        private void RegisterContainer()
        {
            Container.RegisterType<ICellStateEvaluationService, CellStateEvaluationService>();
            Container.RegisterType<IStillLifeEvaluationService, StillLifeEvaluationService>();
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
