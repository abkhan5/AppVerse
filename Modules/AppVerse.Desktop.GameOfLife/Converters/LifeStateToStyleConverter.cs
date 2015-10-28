#region Namespace
using AppVerse.Desktop.Models.GameOfLife;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
#endregion

namespace AppVerse.Desktop.GameOfLife.Converters
{

    public class LifeStateToStyleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string txt = "";

            if ((!(value is LifeState)))
                return txt;

            var enumValue = (LifeState)value;

            switch (enumValue)
            {
                case LifeState.Alive:
                    txt = "AliveButtonStyle"; 
                    break;
                case LifeState.Dead:
                    txt = "DeadButtonStyle";;

                    break;

            }

            return GetStyleFormApplicationGlobalResources(txt);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Gets the style form application global resources.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns></returns>
        public static Style GetStyleFormApplicationGlobalResources(string resourceKey)
        {
            Style returnStyle = null;
            if (Application.Current != null)
            {
                returnStyle = Application.Current.Resources[resourceKey] as Style;
            }
            return returnStyle;
        }
    }

}
