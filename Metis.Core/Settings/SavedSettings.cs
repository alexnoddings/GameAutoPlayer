using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Metis.Core.Settings
{
    /// <summary>
    ///     A class who persists it's settings marked with <see cref="SavedSettingAttribute" /> when it is updated.
    /// </summary>
    public abstract class SavedSettings : Observable
    {
        private const string ConfigPath = "./metis.config.json";
        private static readonly Encoding Encoding = Encoding.UTF8;
        private static readonly IDictionary<string, string> EmptyDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        protected abstract string SettingsName { get; }
        private bool IsLoading { get; set; } = false;

        public SavedSettings()
        {
            IDictionary<string, IDictionary<string, string>> allData = LoadAllModulesData();
            string moduleName = SettingsName;
            IDictionary<string, string> data = allData.ContainsKey(moduleName) ? allData[moduleName] : EmptyDictionary;
            Load(data);
        }

        /// <inheritdoc />
        protected override bool Set<T>(ref T backing, T value, bool forceUpdate = false, string propertyName = "")
        {
            if (!base.Set(ref backing, value, forceUpdate, propertyName))
                return false;

            // Don't save when loading the properties
            if (IsLoading)
                return true;

            SaveModuleData(SettingsName, Save());
            return true;
        }

        #region Save
        protected virtual string SerializeProperty(string propertyName, object propertyValue)
            => JsonSerializer.Serialize(propertyValue);

        private IDictionary<string, string> Save()
        {
            Dictionary<string, string> properties = 
                GetType()
                    .GetProperties()
                    .Where(p => p.GetCustomAttribute<SavedSettingAttribute>() != null)
                    .ToDictionary(p => p.Name, p => SerializeProperty(p.Name, p.GetValue(this)));
            return properties;
        }

        private static void SaveModuleData(string moduleName, IDictionary<string, string> data)
        {
            IDictionary<string, IDictionary<string, string>> allData = LoadAllModulesData();
            allData[moduleName] = data;
            SaveAllModulesData(allData);
        }

        private static void SaveAllModulesData(IDictionary<string, IDictionary<string, string>> allData)
        {
            string json = JsonSerializer.Serialize(allData);
            byte[] bytes = Encoding.GetBytes(json);
            using FileStream fs = File.Create(ConfigPath);
            fs.Write(bytes);
        }
        #endregion

        #region Load
        protected virtual void LoadProperty(string propertyName, string jsonValue)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            if (property == null)
                return;
            Type targetType = property.PropertyType;
            object value = JsonSerializer.Deserialize(jsonValue, targetType);
            property.SetValue(this, value);
        }

        private void Load(IDictionary<string, string> moduleData)
        {
            IsLoading = true;
            foreach (string propertyName in moduleData.Keys)
                LoadProperty(propertyName, moduleData[propertyName]);
            IsLoading = false;
        }

        private static IDictionary<string, IDictionary<string, string>> LoadAllModulesData()
        {
            string json = File.Exists(ConfigPath) ? File.ReadAllText(ConfigPath, Encoding) : "{}";
            return JsonSerializer.Deserialize<IDictionary<string, IDictionary<string, string>>>(json);
        }
        #endregion
    }
}
