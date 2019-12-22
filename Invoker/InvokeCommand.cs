/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/11/2019
 * Time: 4:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Invoker
{
	/// <summary>
	/// Description of InvokeCommand.
	/// </summary>
	public class InvokeCommand
	{
		public string name;
		public string description;
		public HotKey hotkey;
		public string hotkeyString;
		public bool enableButton;
		
		public List<ExecDetails> commands=new List<ExecDetails>();

		
		public InvokeCommand()
		{
		}
	}
}
