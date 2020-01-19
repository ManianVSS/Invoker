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
using System.Threading;

namespace ClipUtil
{
	internal sealed class Program
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
		
		public static void setClipBoardText(String text, int copyRetryMax=5)
		{
			int currentTry=0;
			
			while(currentTry<copyRetryMax)
			{
				try
				{
					Clipboard.Clear();
					Clipboard.SetText(text);
					break;
				}
				catch(Exception e)
				{
					currentTry++;
					Thread.Sleep(1000);
				}
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
						setClipBoardText(args[1]);
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
					setClipBoardText(File.ReadAllText(args[1]));
					break;
					
				case "CopyInputToClipBoard":
					string textToCopy=null;
					
					using (StreamReader sr = new StreamReader(Console.OpenStandardInput()))
					{
						textToCopy=sr.ReadToEnd();
					}
					
					if(textToCopy!=null)
					{
						setClipBoardText(textToCopy);
					}
					
					break;
					
				default:
					Console.WriteLine(Clipboard.GetText());
					break;
			}
		}
	}
}