using System;
using System.Windows;
using Appverse.Desktop.VisualControls;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.ApplicationShell
{
    /// <summary>
    /// Interaction logic for AppverseShellView.xaml
    /// </summary>
    public partial class AppverseShellView : AppVerseWindow
    {

        public AppverseShellView(ApplicationShellViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        
    }
}
