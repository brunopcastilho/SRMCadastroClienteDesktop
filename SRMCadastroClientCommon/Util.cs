using System;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;


namespace SRMCadastroClientCommon
{
    public static class Util
    {

        public static dynamic ParseJson(String filePath)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                return JsonConvert.DeserializeObject(reader.ReadToEnd().ToString());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static String getAppSetting(String key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            String value = appSettings[key];
            if (value == null)
                return "";
            else
                return value;

        }

    }
}
