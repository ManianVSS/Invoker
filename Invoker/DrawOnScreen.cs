/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/12/2019
 * Time: 3:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Invoker
{
	/// <summary>
	/// Description of DrawOnScreen.
	/// </summary>
	public class DrawOnScreen
	{
		
		[DllImport("User32.dll")]
		public static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("User32.dll")]
		public static extern void ReleaseDC(IntPtr dc);

		public static void writeStrToScreen(string textToWrite)
		{
//			if((EnvironmentSelectionComboBox.SelectedIndex==-1)&& string.IsNullOrEmpty(EnvironmentSelectionComboBox.SelectedText) )
//			{
//				return;
//			}
			
			IntPtr desktopDC = GetDC(IntPtr.Zero);

			Graphics g = Graphics.FromHdc(desktopDC);
			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;

			g.DrawString(textToWrite, new Font(FontFamily.GenericSerif, 48,FontStyle.Bold), Brushes.OrangeRed, Screen.PrimaryScreen.Bounds,format);
			//g.DrawRectangle(new Pen(Color.Red,10),Screen.PrimaryScreen.Bounds);
			
			g.DrawString(textToWrite, new Font(FontFamily.GenericSerif, 48,FontStyle.Bold), Brushes.Transparent, Screen.PrimaryScreen.Bounds,format);
//			Invalidate();
			g.Dispose();

			ReleaseDC(desktopDC);
		}
		
		
		public DrawOnScreen()
		{
		}
	}
}
