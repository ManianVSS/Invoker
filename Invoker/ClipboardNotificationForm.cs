/*
 * Created by SharpDevelop.
 * User: Manian
 * Date: 1/4/2020
 * Time: 8:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Invoker
{
	/// <summary>
	/// Description of ClipboardNotificationForm.
	/// </summary>
	public partial class ClipboardNotificationForm : Form
	{
		// defined in winuser.h
		const int WM_DRAWCLIPBOARD = 0x308;
		const int WM_CHANGECBCHAIN = 0x030D;

		IntPtr nextClipboardViewer;
		protected bool clipboardNotificationsEnabled = false;

		public bool EnableClipBoardNotifications()
		{
			if (!clipboardNotificationsEnabled)
			{
				nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
				clipboardNotificationsEnabled = true;
				return true;
			}

			return false;
		}

		public bool DisableClipBoardNotifications()
		{
			if (clipboardNotificationsEnabled)
			{
				ChangeClipboardChain(Handle, nextClipboardViewer);
				clipboardNotificationsEnabled = false;
				return true;
			}
			return false;
		}

		public void ToggleClipBoardNotifications()
		{
			if (clipboardNotificationsEnabled)
			{
				DisableClipBoardNotifications();
			}
			else
			{
				EnableClipBoardNotifications();
			}
		}

		public event EventHandler<ClipboardChangedTextEventArgs> ClipboardChangedToText;
		public event EventHandler<ClipboardChangedImageEventArgs> ClipboardChangedToImage;
		public event EventHandler<ClipboardChangedFileListEventArgs> ClipboardChangedToFileList;
		public event EventHandler<ClipboardChangedAudioStreamEventArgs> ClipboardChangedToAudioStream;
		public event EventHandler<ClipboardChangedDataEventArgs> ClipboardChangedToData;

		[DllImport("User32.dll")]
		protected static extern int SetClipboardViewer(int hWndNewViewer);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (clipboardNotificationsEnabled)
			{
				switch (m.Msg)
				{
					case WM_DRAWCLIPBOARD:
						OnClipboardChanged();
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
						break;

					case WM_CHANGECBCHAIN:
						if (m.WParam == nextClipboardViewer)
							nextClipboardViewer = m.LParam;
						else
							SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
						break;
				}
			}
			base.WndProc(ref m);
		}

		void OnClipboardChanged()
		{
			try
			{
				if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					if (ClipboardChangedToText != null)
					{
						ClipboardChangedToText(this, new ClipboardChangedTextEventArgs(text));
					}
				}
				else if (Clipboard.ContainsFileDropList())
				{
					var dropList = Clipboard.GetFileDropList();
					if (ClipboardChangedToFileList != null)
					{
						ClipboardChangedToFileList(this, new ClipboardChangedFileListEventArgs(dropList));
					}
				}
				else if (Clipboard.ContainsImage())
				{
					Image image = Clipboard.GetImage();
					if (ClipboardChangedToImage != null)
					{
						ClipboardChangedToImage(this, new ClipboardChangedImageEventArgs(image));
					}
				}
				else if (Clipboard.ContainsAudio())
				{
					var audio = Clipboard.GetAudioStream();
					if (ClipboardChangedToAudioStream != null)
					{
						ClipboardChangedToAudioStream(this, new ClipboardChangedAudioStreamEventArgs(audio));
					}
				}
				else
				{
					var dataObj = Clipboard.GetDataObject();
					if (ClipboardChangedToData != null)
					{
						ClipboardChangedToData(this, new ClipboardChangedDataEventArgs(dataObj));
					}
					//Unknown data
				}
			}
			catch (Exception e)
			{
				// Swallow or pop-up, not sure
				// Trace.Write(e.ToString());
				MessageBox.Show(e.ToString());
			}
		}
	}

	public class ClipboardChangedTextEventArgs : EventArgs
	{
		public readonly string textData;

		public ClipboardChangedTextEventArgs(string textData)
		{
			this.textData = textData;
		}
	}

	public class ClipboardChangedImageEventArgs : EventArgs
	{
		public readonly Image image;

		public ClipboardChangedImageEventArgs(Image image)
		{
			this.image = image;
		}
	}

	public class ClipboardChangedFileListEventArgs : EventArgs
	{
		public readonly StringCollection fileList;

		public ClipboardChangedFileListEventArgs(StringCollection fileList)
		{
			this.fileList = fileList;
		}
	}

	public class ClipboardChangedAudioStreamEventArgs : EventArgs
	{
		public readonly Stream audioStream;

		public ClipboardChangedAudioStreamEventArgs(Stream audioStream)
		{
			this.audioStream = audioStream;
		}
	}


	public class ClipboardChangedDataEventArgs : EventArgs
	{
		public readonly IDataObject DataObject;

		public ClipboardChangedDataEventArgs(IDataObject dataObject)
		{
			DataObject = dataObject;
		}
	}
}
