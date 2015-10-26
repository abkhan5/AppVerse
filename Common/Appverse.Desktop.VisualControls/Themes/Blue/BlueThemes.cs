using System.Collections.Generic;

namespace Appverse.Desktop.VisualControls.Themes.Blue
{

    internal static class BlueThemes
    {
        internal const string FolderPath = "Themes/Blue/";

        private static List<string> DefaultStyles = new List<string>
        {
            "ButtonStyles.xaml",
            "ScrollBarStyles.xaml",
                    "AppStoryBoards.xaml",
                    "Generic.xaml",
                    "TextBlockStyles.xaml",
                    "WindowsStyles.xaml"
        };

        public static IEnumerable<string> GetFilePaths()
        {
            foreach (var item in DefaultStyles)
            {
                var source = ApplicationConfiguration.FilePath + FolderPath + item;
                yield return source;
            }
        }

    }

}
