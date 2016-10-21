using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public static class SimpleToDoConfig
    {
        #region Configuration

        public static T TryParse<T>(object value)
        {

            try
            {

                if (value == null)
                {
                    return default(T);
                }

                return (T)(System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value));

            }
            catch (Exception)
            {
                throw new Exception("Error trying to parse setting - " + value.ToString());

            }

        }

        public static T LoadSetting<T>(string SettingName, T defaultVal)
        {

            if ((GetAppSettingString(SettingName)) != null)
            {

                return TryParse<T>(GetAppSettingString(SettingName));

            }
            else
            {

                return defaultVal;

            }

        }

        public static string GetAppSettingString(string SettingName)
        {

            return System.Configuration.ConfigurationManager.AppSettings[SettingName];

        }

        #endregion
    }
}
