/*
 * Property of Manian VSS
 * Copyright Manian VSS 2019
 * User: Manian VSS
 * Date: 1/8/2019
 * Time: 1:27 PM
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Runtime.CompilerServices;

using System.Windows.Forms;

namespace Invoker
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		static string InvokerSettingDirectory=Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\";
		
		static string settingsFile=InvokerSettingDirectory+"InvokerSettings.json";
		
		InvokerSettings invokerSettings=null;
		
		Dictionary<string,InvokerSettings> envSettingsMap=new Dictionary<string, InvokerSettings>();
		InvokerSettings currentEnvSettings;
		Dictionary<string, string> execProperties=new Dictionary<string, string>();

		List<GlobalHotkey> customHotKeys=new List<GlobalHotkey>();
		Dictionary<GlobalHotkey,InvokeCommand> hotKeyCommandDict=new Dictionary<GlobalHotkey, InvokeCommand>();
		
		List<Button> commandButtons=new List<Button>();
		Dictionary<Button,InvokeCommand> commandButtonInvokeDict=new Dictionary<Button, InvokeCommand>();
		Dictionary<string,InvokeCommand> commandsDict=new Dictionary<string, InvokeCommand>();
		
		void enableInvoke(InvokeCommand invoke, int commandNumber)
		{
			commandsComboBox.Items.Add(invoke.name);
			
			if((invoke.hotkey==null)&& !string.IsNullOrEmpty(invoke.hotkeyString))
			{
				invoke.hotkey=new HotKey(invoke.hotkeyString);
			}
			
			if((invoke.hotkey!=null)&& string.IsNullOrEmpty(invoke.hotkeyString))
			{
				invoke.hotkeyString=invoke.hotkey.ToString();
			}
			
			if(invoke.enableButton)
			{
				
				Button commandButton=new Button();
				
				commandsFlowLayoutPanel.Controls.Add(commandButton);
				
				//commandButton.Location = new System.Drawing.Point(3, 3);
				commandButton.Name = "commandButton"+(commandNumber+1);
				commandButton.AutoSize=true;
				commandButton.TabIndex = commandNumber+1;
				commandButton.Text = invoke.name;
				commandButton.UseVisualStyleBackColor = true;
				commandButton.Click += LaunchCustomCommandUIButtonClick;
				
				commandButtons.Add(commandButton);
				
				string tooltip=(invoke.hotkey==null)?invoke.description:invoke.description+"\rHotkey: "+invoke.hotkey;
				commandToolTips.SetToolTip(commandButton,tooltip);
				commandButtonInvokeDict.Add(commandButton,invoke);
				commandsDict.Add(invoke.name,invoke);
			}
		}
		
		void applySettings()
		{
			int i=0;
			bool turnOnHotKeys=hotKeysOn;
			
			if(hotKeysOn)
			{
				disableHotKeys();
			}
			
			commandButtonInvokeDict.Clear();
			commandsDict.Clear();
			commandsComboBox.Items.Clear();
			commandToolTips.RemoveAll();
			
			PropertiesComboBox.Items.Clear();
			
			foreach(Button button in commandButtons)
			{
				button.Visible=false;
				commandsFlowLayoutPanel.Controls.Remove(button);
			}
			commandButtons.Clear();
			
			if(invokerSettings.invokes!=null)
			{
				foreach(InvokeCommand invoke in invokerSettings.invokes)
				{
					enableInvoke(invoke,i);
					
					if(invoke.enableButton)
					{
						i++;
					}
				}
			}
			
			if((currentEnvSettings!=null) && (currentEnvSettings.invokes!=null))
			{
				foreach(InvokeCommand invoke in currentEnvSettings.invokes)
				{
					enableInvoke(invoke,i);
					
					if(invoke.enableButton)
					{
						i++;
					}
				}
				PropertiesComboBox.Items.AddRange(currentEnvSettings.properties.Keys.ToArray());
			}
			
			if(turnOnHotKeys)
			{
				enableHotkeys();
			}
		}
		
		
		void LoadProperties(string envName="default")
		{
			invokerSettings=File.Exists(settingsFile)?InvokerSettings.getFromFile(settingsFile):new InvokerSettings();
			
			execProperties.Clear();
			execProperties.Add("_invoker_general_properties_file",new FileInfo(settingsFile).FullName);
			execProperties.Add("_invoker_environment_settings_file",Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\"+envName+".env.json");
			execProperties.Add("_specialChar_newLine","\n");
			execProperties.Add("_specialChar_backspace","\b");
			execProperties.Add("_specialChar_escape",'\x1b'.ToString());
			
			
			if(File.Exists(settingsFile))
			{
				envSettingsMap.Clear();
				EnvironmentSelectionComboBox.Items.Clear();
				trayEnvSelectorComboBox.Items.Clear();
				EnvironmentSelectionComboBox.Items.Clear();
				trayEnvSelectorComboBox.Items.Clear();
				
				foreach(string envFile in Directory.GetFiles(InvokerSettingDirectory,"*.env.json"))
				{
					string baseFileName=Path.GetFileName(envFile);
					string envAlias=baseFileName.Substring(0,baseFileName.IndexOf(".env.json"));
					EnvironmentSelectionComboBox.Items.Add(envAlias);
					trayEnvSelectorComboBox.Items.Add(envAlias);
					envSettingsMap.Add(envAlias,InvokerSettings.getFromFile(envFile));
				}
				
				
				//Selecting the environment will apply the settings
				if(EnvironmentSelectionComboBox.Items.Contains(envName))
				{
					EnvironmentSelectionComboBox.SelectedIndex=EnvironmentSelectionComboBox.Items.IndexOf(envName);
				}
				else
				{
					applySettings();
				}
			}
			else
			{
				switch(MessageBox.Show("Settings json file "+settingsFile+" was not found. Do you want to create one?","<!>",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation))
				{
					case DialogResult.OK:
					case DialogResult.Yes:
						SaveSettingsButtonClick(null,null);
						break;
				}
			}
			EnvironmentCollectionChanged();
		}
		
		string replaceProperties(string inputStr)
		{
			bool replacementFound=false;
			
			string processStr=inputStr;
			string envName=EnvironmentSelectionComboBox.Text;
			
			foreach(KeyValuePair<string,string> prop in execProperties)
			{
				string replaceStr="$var{"+prop.Key+"}";
				
				if(processStr.Contains(replaceStr))
				{
					replacementFound=true;
					processStr=processStr.Replace(replaceStr,prop.Value);
				}
			}
			
			var envVars=Environment.GetEnvironmentVariables();
			foreach(string envVarKey in envVars.Keys)
			{
				string replaceStr="$env{"+envVarKey+"}";
				
				if(processStr.Contains(replaceStr))
				{
					replacementFound=true;
					processStr=processStr.Replace(replaceStr,Environment.GetEnvironmentVariable(envVarKey));
				}
			}
			
			InvokerSettings envSettings=envSettingsMap[EnvironmentSelectionComboBox.Text];
			foreach(KeyValuePair<string,string> prop in envSettings.properties)
			{
				string replaceStr="${"+prop.Key+"}";
				
				if(processStr.Contains(replaceStr))
				{
					replacementFound=true;
					processStr=processStr.Replace(replaceStr,prop.Value);
				}
			}
			
			foreach(KeyValuePair<string,string> prop in invokerSettings.properties)
			{
				string replaceStr="${"+prop.Key+"}";
				
				if(processStr.Contains(replaceStr))
				{
					replacementFound=true;
					processStr=processStr.Replace(replaceStr,prop.Value);
				}
			}
			
			return replacementFound?replaceProperties(processStr):processStr;
		}
		
		void LoadEnvProperties()
		{
			applySettings();
		}
		
		public void enableHotkeys()
		{
			foreach(InvokeCommand invoke in invokerSettings.invokes)
			{
				if(invoke.hotkey!=null)
				{
					GlobalHotkey ghotkey=new GlobalHotkey();
					customHotKeys.Add(ghotkey);
					hotKeyCommandDict.Add(ghotkey,invoke);
					ghotkey.RegisterGlobalHotKey(invoke.hotkey.key,invoke.hotkey.modifier,this.Handle);
				}
			}
		}
		
		void disableHotKeys()
		{
			if(hotKeysOn)
			{
				foreach(GlobalHotkey ghotKey in customHotKeys)
				{
					ghotKey.Dispose();
				}
			}
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			if(Directory.Exists(InvokerSettingDirectory))
			{
				Directory.CreateDirectory(InvokerSettingDirectory);
			}
			
			//commandButtons.AddRange(new Button[]{commandButton1,commandButton2,commandButton3,commandButton4,commandButton5,commandButton6,commandButton7,commandButton8,commandButton9,commandButton10,commandButton11,commandButton12,commandButton13,commandButton14,commandButton15,commandButton16,commandButton17,commandButton18,commandButton19,commandButton20});
			LoadProperties();
			comoboBoxRefreshed(commandsComboBox);
			comoboBoxRefreshed(PropertiesComboBox);
		}

		protected override void WndProc(ref Message m)
		{
			for(int i=0;i<customHotKeys.Count;i++)
			{
				if(hotKeyCommandDict.ContainsKey(customHotKeys[i]))
				{
					if (m.Msg == 0x0312 && m.WParam.ToInt32() == customHotKeys[i].HotkeyID)
					{
						performInvoke(hotKeyCommandDict[customHotKeys[i]]);
						break;
					}
				}
			}
			
			base.WndProc(ref m);
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			disableHotKeys();
		}
		
		void EnvironmentSelectionComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if(EnvironmentSelectionComboBox.SelectedIndex!=-1)
			{
				if(envSettingsMap.ContainsKey(EnvironmentSelectionComboBox.Text))
				{
					currentEnvSettings=envSettingsMap[EnvironmentSelectionComboBox.Text];
					applySettings();
					
					if(!updatingEnvFromTray)
					{
						trayEnvSelectorComboBox.SelectedIndex=EnvironmentSelectionComboBox.SelectedIndex;
					}
				}
			}
		}
		
		void ReloadSettings()
		{
			string prevcommandstr=commandsComboBox.Text;
			string prevpropertystr=PropertiesComboBox.Text;
			LoadProperties(EnvironmentSelectionComboBox.Text);
			comoboBoxRefreshed(commandsComboBox,prevcommandstr);
			comoboBoxRefreshed(PropertiesComboBox,prevpropertystr);
//			comoboBoxRefreshed(EnvironmentSelectionComboBox);
		}
		
		void ReloadSettingsButtonClick(object sender, EventArgs e)
		{
			ReloadSettings();
		}
		
		bool MainWindowHidden = false;
		
		void toggleShowWindow()
		{
			if (MainWindowHidden)
			{
				this.Show();
				MainWindowHidden = false;
			}
			else
			{
				this.Hide();
				MainWindowHidden = true;
			}
		}
		
		InvokerSettings GetEnvProperties(string envAlias)
		{
			InvokerSettings envProperties=envSettingsMap.ContainsKey(envAlias)?envSettingsMap[envAlias]:new InvokerSettings();
			return envProperties;
		}
		
		void SaveEnvProperties(string envAlias)
		{
			string envFilePath=InvokerSettingDirectory+envAlias+".env.json";
			GetEnvProperties(envAlias).saveToFile(envFilePath);
		}
		
		void Add_EnvironmentButtonClick(object sender, EventArgs e)
		{
			string envName=EnvironmentSelectionComboBox.Text;
			
			if(!string.IsNullOrEmpty(envName) &&  !EnvironmentSelectionComboBox.Items.Contains(envName) )
			{
				string envFilePath=InvokerSettingDirectory+envName+".env.json";
				EnvironmentSelectionComboBox.Items.Add(envName);
				
				InvokerSettings newEnvSettings=null;
				
				
				if(!File.Exists(envFilePath))
				{
					if(File.Exists(envFilePath+".del"))
					{
						File.Move(envFilePath+".del",envFilePath);
					}
					else
					{
						newEnvSettings=new InvokerSettings();
						newEnvSettings.properties=invokerSettings.properties;
						newEnvSettings.saveToFile(envFilePath);
					}
				}

				ReloadSettings();
			}
		}
		
		//Following button is not required. Can be removed
		void Remove_EnvironmentButtonClick(object sender, EventArgs e)
		{
			int selectedIndex=EnvironmentSelectionComboBox.SelectedIndex;
			
			if(selectedIndex!=-1)
			{
				string envName=EnvironmentSelectionComboBox.Text;
				string envFilePath=InvokerSettingDirectory+envName+".env.json";
				EnvironmentSelectionComboBox.Items.RemoveAt(selectedIndex);
				if(File.Exists(envFilePath))
				{
					File.Move(envFilePath,envFilePath+".del");
				}
				
				EnvironmentCollectionChanged();
			}
		}
		
		void SaveSettingsButtonClick(object sender, EventArgs e)
		{
			invokerSettings.saveToFile(settingsFile);
		}
		
		void SaveEnvSettingsButtonClick(object sender, EventArgs e)
		{
			string envName=EnvironmentSelectionComboBox.Text;
			
			if(!string.IsNullOrEmpty(envName))
			{
				SaveEnvProperties(envName);
				
				if(!envSettingsMap.ContainsKey(envName))
				{
					EnvironmentSelectionComboBox.Items.Add(envName);
				}
				
				envSettingsMap[EnvironmentSelectionComboBox.Text]=GetEnvProperties(EnvironmentSelectionComboBox.Text);
			}
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Invoker is launcher tool to work with multiple environments.\rFor queries contact manianvss@hotmail.com.","About");
		}
		
		bool hotKeysOn=false;
		
		[MethodImpl(MethodImplOptions.Synchronized)]
		void ToggleHotKeysButtonClick(object sender, EventArgs e)
		{
			if(hotKeysOn)
			{
				disableHotKeys();
				hotKeysOn=false;
				ToggleHotKeysButton.Text="Enable Hotkeys";
			}
			else
			{
				enableHotkeys();
				hotKeysOn=true;
				ToggleHotKeysButton.Text="Disable Hotkeys";
			}
			TrayToggleHotKeyMenuItem.Text=ToggleHotKeysButton.Text;
			
		}
		
		void PropertiesComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if(PropertiesComboBox.SelectedIndex!=-1)
			{
				if(!string.IsNullOrEmpty(PropertiesComboBox.Text))
				{
					propertyValueTextBox.Text=envSettingsMap[EnvironmentSelectionComboBox.Text].properties[PropertiesComboBox.Text];
				}
			}
		}
		
		void PropertyCopyButtonClick(object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(propertyValueTextBox.Text))
			{
				Clipboard.SetText(propertyValueTextBox.Text);
			}
		}
		
		void Add_PropertyButtonClick(object sender, EventArgs e)
		{
			string propertyName=PropertiesComboBox.Text;
			
			if(!string.IsNullOrEmpty(propertyName))
			{
				currentEnvSettings.properties[propertyName]=propertyValueTextBox.Text;
				applySettings();
			}
		}
		
		void Remove_PropertyButtonClick(object sender, EventArgs e)
		{
			if(PropertiesComboBox.SelectedIndex!=-1)
			{
				currentEnvSettings.properties.Remove(PropertiesComboBox.Text);
				applySettings();
			}
		}
		
//		void PropertySaveButtonClick(object sender, EventArgs e)
//		{
//			if(PropertiesComboBox.SelectedIndex!=-1)
//			{
//				if(!string.IsNullOrEmpty(PropertiesComboBox.Text))
//				{
//					currentEnvSettings.properties[PropertiesComboBox.Text]=propertyValueTextBox.Text;
//				}
//			}
//		}
		
		void NextEnvToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(EnvironmentSelectionComboBox.Items.Count>0)
			{
				EnvironmentSelectionComboBox.SelectedIndex=(EnvironmentSelectionComboBox.SelectedIndex+1)%EnvironmentSelectionComboBox.Items.Count;
			}
		}
		
		void PrevEnvironmentToolStripMenuItemClick(object sender, EventArgs e)
		{
			if((EnvironmentSelectionComboBox.SelectedIndex!=-1) && (EnvironmentSelectionComboBox.Items.Count>0))
			{
				EnvironmentSelectionComboBox.SelectedIndex=(EnvironmentSelectionComboBox.SelectedIndex+1)%EnvironmentSelectionComboBox.Items.Count;
			}
		}
		
		void ToggleShowToolStripMenuItemClick(object sender, EventArgs e)
		{
			toggleShowWindow();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		
		void EnvironmentCollectionChanged()
		{
			trayEnvSelectorComboBox.Items.Clear();
			foreach (object env in EnvironmentSelectionComboBox.Items)
			{
				trayEnvSelectorComboBox.Items.Add((string)env);
			}
		}
		
		bool updatingEnvFromTray=false;
		void TrayEnvSelectorComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			updatingEnvFromTray=true;
			EnvironmentSelectionComboBox.SelectedIndex=trayEnvSelectorComboBox.SelectedIndex;
			updatingEnvFromTray=false;
		}
		
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			ToggleHotKeysButtonClick(sender,e);
		}
		
		void performInvoke(InvokeCommand invoke)
		{
			foreach(ExecDetails command in invoke.commands)
			{
				switch(command.type)
				{
					case "exec":
						Process cmd = new Process();
						cmd.StartInfo.UseShellExecute=command.shell;
						cmd.StartInfo.FileName = replaceProperties(command.path);
						cmd.StartInfo.Arguments=replaceProperties(string.Join(" ",command.args));
						cmd.StartInfo.CreateNoWindow=command.hideWindow;
						
						if(command.saveOutput)
						{
							cmd.StartInfo.RedirectStandardOutput=true;
							cmd.StartInfo.RedirectStandardError=true;
							cmd.StartInfo.UseShellExecute=false;
						}
						
						foreach(KeyValuePair<string,string> kvp in command.env)
						{
							cmd.StartInfo.EnvironmentVariables.Add(replaceProperties(kvp.Key),replaceProperties(kvp.Value));
						}
						
						cmd.Start();
						
						if(command.saveOutput)
						{
							string output = cmd.StandardOutput.ReadToEnd();
							string err = cmd.StandardError.ReadToEnd();
							Cursor.Current = Cursors.WaitCursor;
							cmd.WaitForExit();
							Cursor.Current=Cursors.Default;
							
							if(string.IsNullOrEmpty(command.outputProperty))
							{
								command.outputProperty="exec.out";
							}
							
							execProperties[replaceProperties(command.outputProperty)]=output+err;
						}
						else
						{
							if(command.waitForExit)
							{
								Cursor.Current = Cursors.WaitCursor;
								cmd.WaitForExit();
								Cursor.Current=Cursors.Default;
							}
						}
						break;
						
					case "reloadSettings":
						ReloadSettings();
						break;
						
					case "toggleShowInvokerWindow":
						toggleShowWindow();
						break;
						
					default:
						MessageBox.Show(replaceProperties(command.comments),"Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
						break;
				}
			}
		}
		
		public void comoboBoxRefreshed(ComboBox comboBox)
		{
			comoboBoxRefreshed(comboBox,comboBox.Text);
		}
		
		public void comoboBoxRefreshed(ComboBox comboBox, string text)
		{
			if((comboBox.SelectedIndex==-1) && comboBox.Items.Contains(text))
			{
				comboBox.SelectedIndex=comboBox.Items.IndexOf(text);
			}
			else
			{
				comboBox.SelectedIndex=(comboBox.Items.Count>0)?0:-1;
			}
		}
		
		void LaunchCustomCommandButtonClick(object sender, EventArgs e)
		{
			int selectedIndex=commandsComboBox.SelectedIndex;
			if(selectedIndex!=-1)
			{
				string prevcommandstr=commandsComboBox.Text;
				string prevpropertystr=PropertiesComboBox.Text;
				
				InvokeCommand invoke=invokerSettings.invokes[selectedIndex];
				performInvoke(invoke);
				
				comoboBoxRefreshed(commandsComboBox,prevcommandstr);
				comoboBoxRefreshed(PropertiesComboBox,prevpropertystr);
			}
		}
		
		void LaunchCustomCommandUIButtonClick(object sender, EventArgs e)
		{
			Button clickedButton=(Button)sender;
			
			if(commandButtonInvokeDict.ContainsKey(clickedButton))
			{
				performInvoke(commandButtonInvokeDict[clickedButton]);
			}
		}
		
		void CommandsComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if(commandsComboBox.SelectedIndex!=-1)
			{
				if(commandsDict.ContainsKey(commandsComboBox.Text))
				{
					InvokeCommand invoke=commandsDict[commandsComboBox.Text];
					string tooltip=(invoke.hotkey==null)?invoke.description:invoke.description+"\rHotkey: "+invoke.hotkey;
					commandToolTips.SetToolTip(LaunchCustomCommandButton,tooltip);
				}
			}
		}
		
		void MainNotifyIconDoubleClick(object sender, EventArgs e)
		{
			toggleShowWindow();
		}
		
		void HideInvokerWindowButtonClick(object sender, EventArgs e)
		{
			toggleShowWindow();			
		}
	}
}
