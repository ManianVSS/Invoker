/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/11/2019
 * Time: 4:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace Invoker
{
	
	/// <summary>
	/// Description of HotKey.
	/// </summary>
	public class HotKey
	{
		public const byte WIN=8;
		public const byte SHIFT=4;
		public const byte CONTROL=2;
		public const byte ALT=1;
		
		public int key;
		public int modifier=0;
		
		public void fromString(string hotkeyStr)
		{
			string [] keyStrings=hotkeyStr.Split(new char[]{'+',' ','\t'},StringSplitOptions.RemoveEmptyEntries);
			
			foreach(string keyString in keyStrings)
			{
				string sKey=keyString.Trim().ToUpper();
				
				switch(sKey)
				{
					case "WIN":
					case "WINDOWS":
						this.modifier|=WIN;
						break;
						
					case "CTRL":
					case "CONTROL":
						this.modifier|=CONTROL;
						break;
						
					case "SHIFT":
						this.modifier|=SHIFT;
						break;
						
					case "ALT":
						this.modifier|=ALT;
						break;
						
					default:
						this.key=(int)((Keys) Enum.Parse(typeof(Keys), keyString, true));
						break;
						
				}
				
			}
		}
		
		public HotKey()
		{
		}
		
		public HotKey(string str)
		{
			fromString(str);
		}
		
		public override string ToString()
		{
			string toReturn="";
			
			if((modifier&WIN)==WIN)
			{
				toReturn+="+WIN";
			}
			
			
			if((modifier&CONTROL)==CONTROL)
			{
				toReturn+="+CTRL";
			}
			
			if((modifier&SHIFT)==SHIFT)
			{
				toReturn+="+SHIFT";
			}
			
			if((modifier&ALT)==ALT)
			{
				toReturn+="+ALT";
			}
			
			toReturn+="+"+((Keys)key);
			
			return toReturn.Trim('+');
		}
	}
}
