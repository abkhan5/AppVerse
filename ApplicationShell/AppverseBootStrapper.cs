#region Namespace
using AppVerse.Desktop.AppCommon;
using AppVerse.Desktop.Services.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using AppVerse.Desktop.Services.Interfaces.Navigation;
using AppVerse.Desktop.Services.Navigation;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Windows;
#endregion
namespace AppVerse.Desktop.ApplicationShell
{
    public class AppverseBootStrapper : UnityBootstrapper
    {
                
        protected override DependencyObject CreateShell()
        {
          //  RegisterContainer();
            // Use the container to create an instance of the shell.
            AppverseShellView view = this.Container.TryResolve<AppverseShellView>();
            return view;
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.LoadUnityConfiguration();
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
