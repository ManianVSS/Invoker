/*
 * Property of Manian VSS
 * Copyright Manian VSS 2019-2023
 * User: Manian VSS
 * Date: 1/8/2019
 * Time: 1:27 PM
 * 
 */

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Invoker
{
	/// <summary>
	/// Description of PropertyValue.
	/// </summary>
	public class PropertyValue
	{
		public string value, comments = "";

		public PropertyValue(string value)
		{
			this.value = value;
		}

		public PropertyValue(string value, string comments) : this(value)
		{
			this.comments = comments;
		}

		public void SetComment(string comments)
		{
			this.comments = comments;
		}

		public void AddComment(string comments)
		{
			if (!string.IsNullOrEmpty(this.comments))
			{
				this.comments = this.comments + "\r\n" + comments;
			}
			else
			{
				this.comments = comments;
			}
		}

		public override string ToString()
		{
			return value;
		}

	}

	/// <summary>
	/// Description of Properties.
	/// </summary>
	/// 
	public class Properties
	{
		string headerComments = "";
		string trailerComments = "";
		Dictionary<string, PropertyValue> propertyMap = new Dictionary<string, PropertyValue>();

		static Dictionary<char, char> unescapeKeyMap;
		static Dictionary<char, char> unescapeValueMap;
		
		static Dictionary<char, string> escapeKeyMap;
		static Dictionary<char, string> escapeValueMap;
		
		static Properties()
		{
			unescapeValueMap=new Dictionary<char, char>();
			unescapeValueMap.Add('\\','\\');
			unescapeValueMap.Add('t','\t');
			unescapeValueMap.Add('f','\f');
			unescapeValueMap.Add('r','\r');
			unescapeValueMap.Add('n','\n');
			unescapeValueMap.Add(' ',' ');
			unescapeValueMap.Add('=','=');
			unescapeValueMap.Add(':',':');
			
			unescapeKeyMap=new Dictionary<char, char>(unescapeValueMap);
			unescapeKeyMap.Add('#','#');
			unescapeKeyMap.Add('!','!');
			
			escapeValueMap=new Dictionary<char, string>();
			escapeValueMap.Add('\\',"\\\\");
			escapeValueMap.Add('\t',"\\t");
			escapeValueMap.Add('\f',"\\f");
			escapeValueMap.Add('\r',"\\r");
			escapeValueMap.Add('\n',"\\n");
			escapeValueMap.Add(' ',"\\ ");
			escapeValueMap.Add('=',"\\=");
			escapeValueMap.Add(':',"\\:");
			
			escapeKeyMap=new Dictionary<char, string>(escapeValueMap);
			escapeKeyMap.Add('#',"\\#");
			escapeKeyMap.Add('!',"\\!");
		}

		static readonly string[] lineSplitter = { "\r\n", "\n" };

		public List<string> Keys
		{
			get
			{
				return propertyMap.Keys.ToList();
			}
		}

		public string HeaderComments
		{
			get
			{
				return headerComments;
			}

			set
			{
				headerComments = value;
			}
		}

		public string TrailerComments
		{
			get
			{
				return trailerComments;
			}

			set
			{
				trailerComments = value;
			}
		}

		public Properties()
		{

		}

		public Properties(string file)
		{
			LoadFrom(file);
		}
		
		public void LoadFrom(string file)
		{
			string previousHeaderComments = headerComments;
			string comments = null;
			bool propertiesFound = false;

			foreach (string line in File.ReadAllLines(file))
			{
				string processedLine = line.Trim();
				bool isComment = string.IsNullOrEmpty(processedLine) || (processedLine[0] == '#') || (processedLine[0] == '!');

				//scan for = or : or space
				if (!isComment)
				{
					bool isProperty = false;

					for (int i = 0; i < processedLine.Length; i++)
					{
						switch (processedLine[i])
						{
							case '\\':
								i++;
								continue;
							case ':':
							case ' ':
							case '=':
								isProperty = true;
								break;
						}
					}

					isComment = !isProperty;
				}

				if (isComment)
				{
					if (propertiesFound)
					{
						if (!string.IsNullOrEmpty(comments))
						{
							comments = comments + "\r\n" + line;
						}
						else
						{
							comments = line;
						}
					}
					else
					{
						previousHeaderComments = headerComments;
						comments = line;

						if (!string.IsNullOrEmpty(headerComments))
						{
							headerComments = headerComments + "\r\n" + line;
						}
						else
						{
							headerComments = line;
						}
					}
				}
				else
				{
					if (!propertiesFound)
					{
						propertiesFound = true;
						headerComments = previousHeaderComments;
					}

					string propertyLine = line.TrimStart();
					int i = 0;
					string propName = null, propValue = null;
					int marker = propertyLine.Length;

					while (i < propertyLine.Length)
					{
						if (propertyLine[i] == '\\')
						{
							i = i + 2;
							continue;
						}

						if ((propertyLine[i] == ' ') || (propertyLine[i] == ':') || (propertyLine[i] == '='))
						{
							marker = i;
							break;
						}
						i++;
					}

					switch (propertyLine[i])
					{
						case ' ':
						case '\t':

							i = i++;
							while ((i < propertyLine.Length) && char.IsWhiteSpace(propertyLine[i]))
							{
								i++;
							}

							goto case ':';

						case ':':
						case '=':
							if (((propertyLine[i] == '=') || (propertyLine[i] == ':')) && (!propertyLine.Contains('\t')))
							{
								i = i++;
							}
							//if a : or = found after space(s) then = and : should be ignored
							if ((i < propertyLine.Length) && ((propertyLine[i] == '=') || (propertyLine[i] == ':')))
							{
								i++;
							}

							//Get escaped key and value
							string escapedKey = propertyLine.Substring(0, marker);
							string escapedValue = (i < propertyLine.Length) ? propertyLine.Substring(i).TrimStart() : "";

							//Now unescape key and value
							propName = UnescapePropertyKey(escapedKey);
							propValue = UnescapePropertyValue(escapedValue);
							break;

					}

					if (!string.IsNullOrEmpty(propName))
					{
						set(propName, propValue, comments);
						comments = null;
					}

				}

			}

			if (!string.IsNullOrEmpty(comments))
			{
				if (!string.IsNullOrEmpty(TrailerComments))
				{
					TrailerComments = TrailerComments + "\r\n" + comments;
				}
				else
				{
					TrailerComments = comments;
				}
			}

		}

		public String get(string key, string defValue)
		{
			string value=get(key);			
			return string.IsNullOrEmpty(value) ? (defValue) : (value);
		}

		public String get(string key)
		{
			return (propertyMap.ContainsKey(key)) ? (propertyMap[key]).value : (null);
		}

		public void set(string key, string value)
		{
			if (!propertyMap.ContainsKey(key))
			{
				propertyMap.Add(key, new PropertyValue(value));
			}
			else if (propertyMap[key] == null)
			{
				propertyMap[key] = new PropertyValue(value);
			}
			else
			{
				propertyMap[key].value = value;
			}
		}

		public bool hasKey(string key)
		{
			return propertyMap.ContainsKey(key);
		}
		
		public bool remove(string key)
		{
			return propertyMap.Remove(key);
		}

		public void set(string key, string value, string comments)
		{
			if (!propertyMap.ContainsKey(key))
			{
				propertyMap.Add(key, new PropertyValue(value, comments));
			}
			else if (propertyMap[key] == null)
			{
				propertyMap[key] = new PropertyValue(value, comments);
			}
			else
			{
				propertyMap[key].value = value;
				propertyMap[key].AddComment(comments);
			}
		}


		public string this[string key]
		{
			get
			{
				return propertyMap.ContainsKey(key)?((propertyMap[key]==null)?"":propertyMap[key].value):"";
				//return (propertyMap[key]==null)?"":propertyMap[key].value;
			}
		}

		public void Save(string filename)
		{
			StringBuilder sb = new StringBuilder();

			if (!string.IsNullOrEmpty(headerComments))
			{
				sb.AppendLine(headerComments);
			}

			foreach (string prop in propertyMap.Keys)
			{
				if (!string.IsNullOrEmpty(propertyMap[prop].comments))
				{
					sb.AppendLine(propertyMap[prop].comments);
				}

				sb.AppendLine(EscapePropertyKey(prop) + "=" + EscapePropertyValue(propertyMap[prop].value));
			}

			if (!string.IsNullOrEmpty(TrailerComments))
			{
				sb.AppendLine(TrailerComments);
			}

			File.WriteAllText(filename, sb.ToString());
		}


		static string UnescapePropertyKey(string escapedKey)
		{
			StringBuilder unescapsedString=new StringBuilder();
			
			for(int i=0;i<escapedKey.Length;i++)
			{
				if(escapedKey[i]=='\\')
				{
					if(i==(escapedKey.Length-1))
					{
						unescapsedString.Append(escapedKey[i]);
						continue;
					}
					
					if(unescapeKeyMap.ContainsKey(escapedKey[i+1]))
					{
						unescapsedString.Append(unescapeKeyMap[escapedKey[i+1]]);
					}
					else
					{
						unescapsedString.Append("\\"+escapedKey[i+1]);
					}
					
					i++;
				}
				else
				{
					unescapsedString.Append(escapedKey[i]);
				}
			}
			return unescapsedString.ToString();
			
		}

		static string UnescapePropertyValue(string escapedValue)
		{
			StringBuilder unescapsedString=new StringBuilder();
			
			for(int i=0;i<escapedValue.Length;i++)
			{
				if(escapedValue[i]=='\\')
				{
					if(i==(escapedValue.Length-1))
					{
						unescapsedString.Append(escapedValue[i]);
						continue;
					}
					
					if(unescapeValueMap.ContainsKey(escapedValue[i+1]))
					{
						unescapsedString.Append(unescapeValueMap[escapedValue[i+1]]);
					}
					else
					{
						unescapsedString.Append("\\"+escapedValue[i+1]);
					}
					
					i++;
				}
				else
				{
					unescapsedString.Append(escapedValue[i]);
				}
			}
			return unescapsedString.ToString();
		}
		
		
		static string EscapePropertyKey(string unscapedKey)
		{
			StringBuilder escapsedString=new StringBuilder();
			
			for(int i=0;i<unscapedKey.Length;i++)
			{
				if(escapeKeyMap.ContainsKey(unscapedKey[i]))
				{
					escapsedString.Append(escapeKeyMap[unscapedKey[i]]);
				}
				else
				{
					escapsedString.Append(unscapedKey[i]);
				}
			}
			
			return escapsedString.ToString();
		}

		static string EscapePropertyValue(string unescapedValue)
		{
			StringBuilder escapsedString=new StringBuilder();
			
			for(int i=0;i<unescapedValue.Length;i++)
			{
				if(escapeValueMap.ContainsKey(unescapedValue[i]))
				{
					escapsedString.Append(escapeValueMap[unescapedValue[i]]);
				}
				else
				{
					escapsedString.Append(unescapedValue[i]);
				}				
			}
			
			return escapsedString.ToString();
		}


	}
}
