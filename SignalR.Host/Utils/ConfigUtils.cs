using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Host.Utils
{
    public static class ConfigUtils
    {
        private  static readonly AppSettingsReader configurationAppSettings;

       

        static ConfigUtils()
        {
            configurationAppSettings = new System.Configuration.AppSettingsReader();
        }

        public static T GetConfig<T>(string key, T defaultValue)
        {
            try
            {
                var result = configurationAppSettings.GetValue(key, typeof(T));
                return (T)result;
            }
            catch (Exception)
            {
                if (defaultValue!=null)
                {
                    return defaultValue;
                }
                return default(T);
            }
        }

        public static T GetConfig<T>(string key)
        {
            try
            {
                var result = configurationAppSettings.GetValue(key, typeof(T));
                return (T)result;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("没有在配置文件中的appSettings中找到{0}的配置，请检查配置文件配置！", key));
            }
        }
    }
}
