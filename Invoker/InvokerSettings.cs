/*
 * Property of Manian VSS
 * Copyright Manian VSS 2019
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

namespace Invoker
{
    /// <summary>
    /// Description of InvokerSettings.
    /// </summary>
    public class InvokerSettings
    {
        public Dictionary<string, EnvironmentSettings> environmentSettings;

        public void Initialize()
        {
            if (environmentSettings == null)
            {
                environmentSettings = new Dictionary<string, EnvironmentSettings>();
            }
            if (!environmentSettings.ContainsKey("default"))
            {
                environmentSettings["default"] = new EnvironmentSettings();
            }
        }

        public InvokerSettings()
        {
            Initialize();
        }

        public static InvokerSettings getFromFile(string file)
        {
            InvokerSettings invokerSettings = JsonConvert.DeserializeObject<InvokerSettings>(File.ReadAllText(file));
            invokerSettings.Initialize();
            return invokerSettings;
        }

        public void saveToFile(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
