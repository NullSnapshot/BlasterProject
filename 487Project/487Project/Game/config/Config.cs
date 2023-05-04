using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BulletBlaster.Game.config
{
    // Global configs
    internal static class Config
    {
        internal static int TargetFPS { get; set; } = 60;

        internal static bool DebugMode { get; set; } = false;

        internal static string LevelName { get; set; } = "TeamBlaster";

        internal static void SaveConfig()
        {
            PropertyInfo[] Properties = typeof(Config).GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public);

            Dictionary<string, object> a = new Dictionary<string, object>();
            int i = 0;
            foreach (PropertyInfo property in Properties)
            {
                a[property.Name] = property.GetValue(null);
                i++;
            }
            JsonSerializerOptions writeConfig = new JsonSerializerOptions();
            writeConfig.WriteIndented = true;
            string json = JsonSerializer.Serialize(a,options:writeConfig);
            File.WriteAllText("config.json", json);
        }

        internal static void LoadConfig()
        {
            if(File.Exists("config.json"))
            {
                string json = File.ReadAllText("config.json");
                Dictionary<string, JsonElement> a = new Dictionary<string, JsonElement>();
                a = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                PropertyInfo[] Properties = typeof(Config).GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo property in Properties)
                {
                    if (a.ContainsKey(property.Name))
                    {
                        if (property.PropertyType == typeof(int))
                            {
                            int i;
                            if(a[property.Name].TryGetInt32(out i))
                                property.SetValue(typeof(Config), i);
                            }
                        else if (property.PropertyType == typeof(bool))
                        {
                            bool b = a[property.Name].GetBoolean();
                            property.SetValue(typeof(Config), b);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            string s = a[property.Name].GetString();
                            property.SetValue(typeof(Config), s);
                        }
                    }
                }
            }
        }
    }
}
