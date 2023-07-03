/*
 * Property of Manian VSS
 * Copyright Manian VSS 2019-2023
 * User: Manian VSS
 * Date: 08-10-2019
 * Time: 15:21
 * 
 */


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
    /// <summary>
    /// Description of InvokerSettings.
    /// </summary>
    public class InvokerSettings
    {
        public Dictionary<string, EnvironmentSettings> environmentSettings;

        private static readonly IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        public InvokerSettings Initialize()
        {
            if (environmentSettings == null)
            {
                environmentSettings = new Dictionary<string, EnvironmentSettings>();
            }
            if (!environmentSettings.ContainsKey("default"))
            {
                environmentSettings["default"] = new EnvironmentSettings();
            }
            return this;
        }

        public InvokerSettings()
        {
            Initialize();
        }

        public static InvokerSettings getFromFile(string file)
        {           
            if (file.ToLower().EndsWith(".yaml") || file.ToLower().EndsWith(".yml"))
            {
                return deserializer.Deserialize<InvokerSettings>(File.ReadAllText(file)).Initialize();
            }
            else
            {
                return  JsonConvert.DeserializeObject<InvokerSettings>(File.ReadAllText(file)).Initialize();
            }           
        }

        public void saveToFile(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
