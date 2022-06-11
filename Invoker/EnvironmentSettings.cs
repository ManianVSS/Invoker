using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Invoker
{
    public class EnvironmentSettings
    {
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        public List<InvokeCommand> invokes = new List<InvokeCommand>();
        public string type = "default";

        private static readonly IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();


        public EnvironmentSettings Initialize()
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
            return this;
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
            if (file.ToLower().EndsWith(".yaml") || file.ToLower().EndsWith(".yml"))
            {
                return deserializer.Deserialize<EnvironmentSettings>(File.ReadAllText(file)).Initialize();
            }
            else
            {
                return JsonConvert.DeserializeObject<EnvironmentSettings>(File.ReadAllText(file)).Initialize();
            }
        }

        public void saveToFile(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
