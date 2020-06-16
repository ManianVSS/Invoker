using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Invoker
{
    public class EnvironmentSettings
    {
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        public List<InvokeCommand> invokes = new List<InvokeCommand>();
        public string type = "default";

        public void Initialize()
        {
            if (this.properties == null)
            {
                this.properties = new Dictionary<string, string>();
            }

            if (this.invokes == null)
            {
                this.invokes = new List<InvokeCommand>();
            }

            if (this.type == null)
            {
                this.type = "default";
            }
        }

        public EnvironmentSettings()
        {
            Initialize();
        }

        public EnvironmentSettings(Dictionary<string, string> properties)
        {
            foreach (KeyValuePair<string, string> kvp in properties)
            {
                this.properties.Add(kvp.Key, kvp.Value);
            }
        }

        public static EnvironmentSettings getFromFile(string file)
        {
            EnvironmentSettings environmentSettings = JsonConvert.DeserializeObject<EnvironmentSettings>(File.ReadAllText(file));
            environmentSettings.Initialize();
            return environmentSettings;
        }

        public void saveToFile(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
