using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SAPUserControl.Converters
{
    class ParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                //List<SAPDataParameter> paras = value as List<SAPDataParameter>;
                //if(paras != null)
                //{
                //    string paraString = string.Empty;
                //}
                //object[] parameters = value as object[];
                //string paraString = string.Empty;
                //foreach (object obj in parameters)
                //{
                //    paraString += obj.ToString() + ";";
                //}
                //paraString = paraString.Substring(0, paraString.Length - 1);
                //return paraString;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value != null)
            //{
            //    string paraString = value.ToString();
            //    return paraString.Split(';');
            //}
            return null;
        }
    }
}
