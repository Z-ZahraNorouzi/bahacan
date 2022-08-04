using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configs
{
    public class Config
    {
        #region fields
        static ConcurrentDictionary<string, string> settings = null;

        /// <summary>the locket object</summary>
        static readonly object objLock = new object();
        #endregion

        #region All Configs

        public static Dictionary<string, string> GetAllConfigs()
        {
            if (settings != null)
                return settings.ToDictionary(x => x.Key, x => x.Value);

            var settingsDic = new Dictionary<string, string>();
            var allKeys = System.Configuration.ConfigurationManager.AppSettings.AllKeys;
            for (int i = 0; i < allKeys.Length; i++)
                settingsDic.Add(allKeys[i], System.Configuration.ConfigurationManager.AppSettings[allKeys[i]]);
            return settingsDic;
        }

        #endregion

        #region private helpers

        private static string getConfig(string name)
        {
            if (settings == null)
            {
                lock (objLock)
                {
                    var settingsDic = GetAllConfigs();
                    settings = new ConcurrentDictionary<string, string>(settingsDic);
                }
            }
            if (settings.ContainsKey(name))
                return settings[name];
            return null;
        }
        private static int getIntConfig(string name)
        {
            return int.Parse(getConfig(name));
        }
        private static long getLongConfig(string name)
        {
            return long.Parse(getConfig(name));
        }
        private static bool getBoolConfig(string name)
        {
            return getConfig(name) == "true";
        }

        #endregion

        #region WebAPI

        public static string WebAPI_BaseUrl { get { return getConfig("WebAPI_BaseUrl"); } }

        public static string WebAPI_AirTourUrl { get { return getConfig("WebAPI_AirTourUrl"); } }

        #endregion

        #region LoginStatus

        public static int LoginStatus_Successful { get { return getIntConfig("LoginStatus_Successful"); } }
        public static int LoginStatus_IncorrectPassword { get { return getIntConfig("LoginStatus_IncorrectPassword"); } }
        public static int LoginStatus_InactiveUser { get { return getIntConfig("LoginStatus_InactiveUser"); } }
        public static int LoginStatus_LockUser { get { return getIntConfig("LoginStatus_LockUser"); } }

        public static int LoginStatus_OutOfDate { get { return getIntConfig("LoginStatus_OutOfDate"); } }

        #endregion
    }
}