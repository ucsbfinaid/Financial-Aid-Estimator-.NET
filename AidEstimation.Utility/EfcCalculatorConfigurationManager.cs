using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Hosting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    /// <summary>
    /// Constructs <see cref="EfcCalculator"/>s using the paths to XML files specified within
    /// the appSettings configuration section
    /// </summary>
    public static class EfcCalculatorConfigurationManager
    {
        /// <summary>
        /// Template used when locating a source path within the appSettings configuration section.
        /// The placeholder "{key}" in this string will be replaced with the key value provided
        /// to <see cref="GetEfcCalculator"/> method below
        /// </summary>
        public static string AppSettingKeyTemplate { get; set; }

        private const string DefaultAppSettingsKeyTemplate = "EfcCalculation.Constants.{key}";
        private const string KeyPlaceholder = "{key}";
        private const string RelativePathPlaceholder = "~/";

        private static readonly Dictionary<string, EfcCalculator> _cache = new Dictionary<string, EfcCalculator>();

        static EfcCalculatorConfigurationManager()
        {
            AppSettingKeyTemplate = DefaultAppSettingsKeyTemplate;
        }

        /// <summary>
        /// Constructs <see cref="EfcCalculator"/>s using the paths to XML files specified within
        /// the appSettings configuration section. By default, this class will search for the source
        /// paths in appSettings with keys set to "EfcCalculation.Constants.{key}" where "{key}"
        /// is replaced with the key being passed into this method.
        /// 
        /// For example, if "1011" was passed as the key into this method, the following appSettings value would
        /// be used:
        /// 
        /// &lt;add key="EfcCalculation.Constants.1011" value="~/Constants/EfcCalculationConstants.1011.xml"/&gt;
        /// 
        /// To reduce the number of times files are read, constructed <see cref="EfcCalculator"/>s are cached
        /// for the duration of this <see cref="EfcCalculatorConfigurationManager"/>'s lifetime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static EfcCalculator GetEfcCalculator(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("No EFC Calculator key provided");
            }

            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }

            if (String.IsNullOrEmpty(AppSettingKeyTemplate))
            {
                throw new ArgumentException("No appSettings EFC Calculator key template was specified");
            }

            string appSettingsKey = AppSettingKeyTemplate.Replace(KeyPlaceholder, key);
            string xmlSourcePath = ConfigurationManager.AppSettings[appSettingsKey];

            if (String.IsNullOrEmpty(xmlSourcePath))
            {
                throw new ArgumentException("No source path was specified for the EFC Calculator in appSettings");
            }

            // If a relative web path is used, resolve the application's physical path
            if (xmlSourcePath.StartsWith(RelativePathPlaceholder))
            {
                xmlSourcePath = xmlSourcePath.Replace(RelativePathPlaceholder, HostingEnvironment.ApplicationPhysicalPath);
            }

            EfcCalculatorFactory factory = new EfcCalculatorFactory(xmlSourcePath);
            EfcCalculator calculator = factory.GetEfcCalculator();
            _cache[key] = calculator;

            return calculator;
        }

        /// <summary>
        /// Overload version that constructs <see cref="EfcCalculator"/>s using the paths to XML files passed. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="xmlSourcePath"></param>
        /// <returns></returns>
        public static EfcCalculator GetEfcCalculator(string key, string xmlSourcePath)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("No EFC Calculator key provided");
            }

            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }

            if (String.IsNullOrEmpty(xmlSourcePath))
            {
                throw new ArgumentException("No source path was specified for the EFC Calculator in appSettings");
            }

            // If a relative web path is used, resolve the application's physical path
            if (xmlSourcePath.StartsWith(RelativePathPlaceholder))
            {
                xmlSourcePath = xmlSourcePath.Replace(RelativePathPlaceholder, HostingEnvironment.ApplicationPhysicalPath);
            }

            EfcCalculatorFactory factory = new EfcCalculatorFactory(xmlSourcePath);
            EfcCalculator calculator = factory.GetEfcCalculator();
            _cache[key] = calculator;

            return calculator;
        }
    }
}
