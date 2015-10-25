using Appverse.Desktop.VisualControls.Themes.Blue;
using Appverse.Desktop.VisualControls.Themes.Red;
using System.Collections.Generic;

namespace Appverse.Desktop.VisualControls
{
    public static class ApplicationConfiguration
    {
        internal const string FilePath = "pack://application:,,,/Appverse.Desktop.VisualControls;component/";

        private static IEnumerable<string> StyleSelector(AppThemes appThemes)
        {
            IEnumerable<string> styleSource;
            switch (appThemes)
            {
                case AppThemes.Red:
                    styleSource = RedTheme.GetFilePaths();
                    break;

                case AppThemes.Blue:
                default:
                    styleSource = BlueThemes.GetFilePaths();
                    break;
            }

            return styleSource;
        }

        public static void SetDefaultResources(AppThemes appThemes)
        {

            var styleSource = StyleSelector(appThemes);
            foreach (var item in styleSource)
            {
                var source =  item;
                Extensions.SetResource(source);

            }
        }


    }


}
