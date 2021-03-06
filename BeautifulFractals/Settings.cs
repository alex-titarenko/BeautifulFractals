﻿using System;
using System.IO;
using System.Reflection;
using TAlex.BeautifulFractals.Helpers;
using TAlex.BeautifulFractals.Services;

namespace TAlex.BeautifulFractals.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings : IAppSettings {
        
        public Settings() {                
            if (CallUpgrade)
            {
                UpgradeSettings();
                CallUpgrade = false;
            }
        }

        private void UpgradeSettings()
        {
            try
            {
                string path = Environment.ExpandEnvironmentVariables(FractalsCollectionPath);
                if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    FractalsHelper.GetEmbeddedFractalsStream().CopyTo(file);
                }
            }
            catch (IOException) {}

            Upgrade();
        }
    }
}
