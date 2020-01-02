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
		public Dictionary<string,string> properties=new Dictionary<string, string>();
		public List<InvokeCommand> invokes=new List<InvokeCommand>();
		
		public InvokerSettings()
		{
		}
		
		public InvokerSettings(Dictionary<string,string> properties)
		{
			foreach(KeyValuePair<string,string>kvp in properties)
			{
				this.properties.Add(kvp.Key,kvp.Value);
			}
		}
		
		public static InvokerSettings getFromFile(string file)
		{
			InvokerSettings invokerSettings= JsonConvert.DeserializeObject<InvokerSettings>(File.ReadAllText(file));
			
			if(invokerSettings.properties==null)
			{
				invokerSettings.properties=new Dictionary<string, string>();
			}
			
			if(invokerSettings.invokes==null)
			{
				invokerSettings.invokes=new List<InvokeCommand>();
			}
			
			return invokerSettings;
		}
		
		public void saveToFile(string file)
		{
			File.WriteAllText(file,JsonConvert.SerializeObject(this));
		}
	}
}
