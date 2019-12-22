/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/11/2019
 * Time: 4:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Invoker
{
	/// <summary>
	/// Description of ExecDetails.
	/// </summary>
	public class ExecDetails
	{
		public string type="info";
		public string comments="comments";
		public string path;
		public string []args;
		public bool shell;
		public Dictionary<string,string> env=new Dictionary<string, string>();		
		public bool waitForExit=false;
		public bool saveOutput;
		public string outputProperty="exec.out";
		public bool hideWindow=false;
		
		public ExecDetails()
		{
		
		}
	}
}
