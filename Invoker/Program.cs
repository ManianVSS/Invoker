/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/30/2018
 * Time: 12:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Threading;

namespace Invoker
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		private static Mutex mutex = null;
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			const string appName = "mmz.Invoker";
			
			bool createdNew;
			
			mutex = new Mutex(true, appName, out createdNew);
			
			if (!createdNew)
			{
				//app is already running! Exiting the application
				return;
			}
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
