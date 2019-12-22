/*
 * Property of Manian VSS
 * Copyright Manian VSS 2019
 * User: Manian VSS
 * Date: 16-05-2019
 * Time: 09:57
 * 
 */


using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClipUtil
{
	class Program
	{
		public static void printUsage(bool error)
		{
			const string usage="<option> <parameter>\n ";
			
			if(error)
			{
				Console.Error.WriteLine(usage);
			}
			else
			{
				Console.WriteLine(usage);
			}
			
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			if(args.Length<1)
			{
				Console.WriteLine(args);
				printUsage(true);
				Environment.Exit(1);
			}
			
			//Console.WriteLine("Options used  are  "+string.Join(" ",args));
			
			switch(args[0])
			{
				case "CopyTextToClipBoard":
					
					if(args.Length>1)
					{
						Clipboard.SetText(args[1]);
					}
					
					break;
					
				case "CopyFileToClipBoard":					
					var stc = new System.Collections.Specialized.StringCollection();
					
					for(int i=1;i<args.Length;i++)
					{
						stc.Add(args[i]);
					}
					
					Clipboard.SetFileDropList(stc);
					break;
					
				case "CopyFileTextToClipBoard":
					Clipboard.SetText(File.ReadAllText(args[1]));
					break;
					
				case "CopyInputToClipBoard":
					string textToCopy=null;
					
					using (StreamReader sr = new StreamReader(Console.OpenStandardInput()))
					{
						textToCopy=sr.ReadToEnd();
					}			
					
					if(textToCopy!=null)
					{
						Clipboard.SetText(textToCopy);
					}
					
					break;
					
				default:
					Console.WriteLine(Clipboard.GetText());
					break;
			}
		}
	}
}