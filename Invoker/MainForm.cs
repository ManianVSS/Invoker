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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace Invoker
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : ClipboardNotificationForm
    {
        private const string ENV_JSON_EXTN = ".env.json";
        private const string SEARCH_PATTERN_ENV_JSON = "*.env.json";

        static string InvokerSettingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.invoker\\";
        static string assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        static string originalSettingsFile = assemblyLocation + "InvokerSettings.json";

        static string userSettingsFile = InvokerSettingDirectory + "InvokerSettings.json";

        InvokerSettings invokerSettings = null;
        List<object> clipBoardQueue = new List<object>();

        Dictionary<string, EnvironmentSettings> envSettingsMap = new Dictionary<string, EnvironmentSettings>();
        EnvironmentSettings currentEnvSettings = new EnvironmentSettings();

        Dictionary<string, string> execProperties = new Dictionary<string, string>();

        List<GlobalHotkey> customHotKeys = new List<GlobalHotkey>();
        Dictionary<GlobalHotkey, InvokeCommand> hotKeyCommandDict = new Dictionary<GlobalHotkey, InvokeCommand>();

        List<Button> commandButtons = new List<Button>();
        Dictionary<Button, InvokeCommand> commandButtonInvokeDict = new Dictionary<Button, InvokeCommand>();
        Dictionary<string, InvokeCommand> commandsDict = new Dictionary<string, InvokeCommand>();

        ConcurrentQueue<InvokeCommand> invokesToProcess = new ConcurrentQueue<InvokeCommand>();

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            LoadProperties();
            comoboBoxRefreshed(commandsComboBox);
            comoboBoxRefreshed(PropertiesComboBox);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_KEYDOWN = 0x0312;

            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    for (int i = 0; i < customHotKeys.Count; i++)
                    {
                        if (hotKeyCommandDict.ContainsKey(customHotKeys[i]))
                        {
                            if (m.WParam.ToInt32() == customHotKeys[i].HotkeyID)
                            {
                                invokesToProcess.Enqueue(hotKeyCommandDict[customHotKeys[i]]);
                                break;
                            }
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        void enableInvoke(InvokeCommand invoke, int commandNumber)
        {
            commandsComboBox.Items.Add(invoke.name);

            if ((invoke.hotkey == null) && !string.IsNullOrEmpty(invoke.hotkeyString))
            {
                invoke.hotkey = new HotKey(invoke.hotkeyString);
            }

            if ((invoke.hotkey != null) && string.IsNullOrEmpty(invoke.hotkeyString))
            {
                invoke.hotkeyString = invoke.hotkey.ToString();
            }

            if (invoke.enableButton)
            {

                Button commandButton = new Button();

                commandsFlowLayoutPanel.Controls.Add(commandButton);

                //commandButton.Location = new System.Drawing.Point(3, 3);
                commandButton.Name = "commandButton" + (commandNumber + 1);
                commandButton.AutoSize = true;
                commandButton.TabIndex = commandNumber + 1;
                commandButton.Text = invoke.name;
                commandButton.UseVisualStyleBackColor = true;
                commandButton.Click += LaunchCustomCommandUIButtonClick;

                commandButtons.Add(commandButton);

                string tooltip = (invoke.hotkey == null) ? invoke.description : invoke.description + "\rHotkey: " + invoke.hotkey;
                commandToolTips.SetToolTip(commandButton, tooltip);
                commandButtonInvokeDict.Add(commandButton, invoke);
            }

            commandsDict.Add(invoke.name, invoke);
        }

        void applySettings()
        {
            int i = 0;
            bool turnOnHotKeys = hotKeysOn;

            if (hotKeysOn)
            {
                disableHotKeys();
            }

            commandButtonInvokeDict.Clear();
            commandsDict.Clear();
            commandsComboBox.Items.Clear();
            commandToolTips.RemoveAll();

            PropertiesComboBox.Items.Clear();

            foreach (Button button in commandButtons)
            {
                button.Visible = false;
                commandsFlowLayoutPanel.Controls.Remove(button);
            }
            commandButtons.Clear();

            if ((currentEnvSettings != null) && (currentEnvSettings.invokes != null))
            {
                foreach (InvokeCommand invoke in currentEnvSettings.invokes)
                {
                    if (!commandsComboBox.Items.Contains(invoke.name))
                    {
                        enableInvoke(invoke, i);

                        if (invoke.enableButton)
                        {
                            i++;
                        }
                    }
                }
                PropertiesComboBox.Items.AddRange(currentEnvSettings.properties.Keys.ToArray());
            }

            EnvironmentTypeTextBox.Text = currentEnvSettings.type;

            if (invokerSettings.environmentSettings[currentEnvSettings.type].invokes != null)
            {
                foreach (InvokeCommand invoke in invokerSettings.environmentSettings[currentEnvSettings.type].invokes)
                {
                    if (!commandsComboBox.Items.Contains(invoke.name))
                    {
                        enableInvoke(invoke, i);

                        if (invoke.enableButton)
                        {
                            i++;
                        }
                    }
                }
            }

            commandsComboBox.SelectedIndex = commandsComboBox.Items.Count > 0 ? 0 : -1;
            PropertiesComboBox.SelectedIndex = PropertiesComboBox.Items.Count > 0 ? 0 : -1;
            if (turnOnHotKeys)
            {
                enableHotkeys();
            }
        }

        void LoadProperties(string envName = "default", string envType = "default")
        {
            if (!Directory.Exists(InvokerSettingDirectory) || (!Directory.GetFiles(InvokerSettingDirectory, SEARCH_PATTERN_ENV_JSON).Any()))
            {
                Directory.CreateDirectory(InvokerSettingDirectory);

                foreach (string envFile in Directory.GetFiles(assemblyLocation, SEARCH_PATTERN_ENV_JSON))
                {
                    FileInfo srcFI = new FileInfo(envFile);
                    srcFI.CopyTo(InvokerSettingDirectory + "\\" + srcFI.Name, false);
                }
            }

            if (!File.Exists(userSettingsFile))
            {
                if (File.Exists(originalSettingsFile))
                {
                    File.Copy(originalSettingsFile, userSettingsFile);
                }
                else
                {
                    MessageBox.Show("Settings file missing: " + originalSettingsFile + ". Restore the file or repair application from control panel and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                }
            }

            invokerSettings = File.Exists(userSettingsFile) ? InvokerSettings.getFromFile(userSettingsFile) : (File.Exists(originalSettingsFile) ? InvokerSettings.getFromFile(originalSettingsFile) : new InvokerSettings());
            currentEnvSettings = new EnvironmentSettings(invokerSettings.environmentSettings[envType].properties);

            execProperties.Clear();
            execProperties.Add("_invoker_general_properties_file", new FileInfo(userSettingsFile).FullName);
            execProperties.Add("_invoker_environment_settings_file", InvokerSettingDirectory + envName + ENV_JSON_EXTN);
            execProperties.Add("_specialChar_newLine", "\n");
            execProperties.Add("_specialChar_backspace", "\b");
            execProperties.Add("_specialChar_escape", '\x1b'.ToString());

            envSettingsMap.Clear();
            EnvironmentSelectionComboBox.Items.Clear();
            trayEnvSelectorComboBox.Items.Clear();
            EnvironmentSelectionComboBox.Items.Clear();
            trayEnvSelectorComboBox.Items.Clear();

            foreach (string envFile in Directory.GetFiles(InvokerSettingDirectory, SEARCH_PATTERN_ENV_JSON))
            {
                string baseFileName = Path.GetFileName(envFile);
                string envAlias = baseFileName.Substring(0, baseFileName.IndexOf(ENV_JSON_EXTN));
                EnvironmentSelectionComboBox.Items.Add(envAlias);
                trayEnvSelectorComboBox.Items.Add(envAlias);
                envSettingsMap.Add(envAlias, EnvironmentSettings.getFromFile(envFile));
            }


            //Selecting the environment will apply the settings
            if (EnvironmentSelectionComboBox.Items.Contains(envName))
            {
                EnvironmentSelectionComboBox.SelectedIndex = EnvironmentSelectionComboBox.Items.IndexOf(envName);
            }
            else
            {
                applySettings();
            }


            EnvironmentCollectionChanged();
        }

        string replaceProperties(string inputStr)
        {
            bool replacementFound = false;

            string processStr = inputStr;
            string envName = EnvironmentSelectionComboBox.Text;

            foreach (KeyValuePair<string, string> prop in execProperties)
            {
                string replaceStr = "$var{" + prop.Key + "}";

                if (processStr.Contains(replaceStr))
                {
                    replacementFound = true;
                    processStr = processStr.Replace(replaceStr, prop.Value);
                }
            }

            var envVars = Environment.GetEnvironmentVariables();
            foreach (string envVarKey in envVars.Keys)
            {
                string replaceStr = "$env{" + envVarKey + "}";

                if (processStr.Contains(replaceStr))
                {
                    replacementFound = true;
                    processStr = processStr.Replace(replaceStr, Environment.GetEnvironmentVariable(envVarKey));
                }
            }

            EnvironmentSettings envSettings = envSettingsMap[EnvironmentSelectionComboBox.Text];
            foreach (KeyValuePair<string, string> prop in envSettings.properties)
            {
                string replaceStr = "${" + prop.Key + "}";

                if (processStr.Contains(replaceStr))
                {
                    replacementFound = true;
                    processStr = processStr.Replace(replaceStr, prop.Value);
                }
            }

            string type = invokerSettings.environmentSettings.ContainsKey(currentEnvSettings.type) ? currentEnvSettings.type : "default";
            foreach (KeyValuePair<string, string> prop in invokerSettings.environmentSettings[type].properties)
            {
                string replaceStr = "${" + prop.Key + "}";

                if (processStr.Contains(replaceStr))
                {
                    replacementFound = true;
                    processStr = processStr.Replace(replaceStr, prop.Value);
                }
            }

            return replacementFound ? replaceProperties(processStr) : processStr;
        }

        void LoadEnvProperties()
        {
            applySettings();
        }

        public void enableHotkeys()
        {
            string type = invokerSettings.environmentSettings.ContainsKey(currentEnvSettings.type) ? currentEnvSettings.type : "default";
            foreach (InvokeCommand invoke in invokerSettings.environmentSettings[type].invokes)
            {
                if (invoke.hotkey != null)
                {
                    GlobalHotkey ghotkey = new GlobalHotkey();
                    customHotKeys.Add(ghotkey);
                    hotKeyCommandDict.Add(ghotkey, invoke);
                    ghotkey.RegisterGlobalHotKey(invoke.hotkey.key, invoke.hotkey.modifier, this.Handle);
                }
            }
        }

        void disableHotKeys()
        {
            if (hotKeysOn)
            {
                foreach (GlobalHotkey ghotKey in customHotKeys)
                {
                    ghotKey.Dispose();
                }
            }
        }

        void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            disableHotKeys();
            DisableClipBoardNotifications();
        }

        void EnvironmentSelectionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (EnvironmentSelectionComboBox.SelectedIndex != -1)
            {
                string envName = EnvironmentSelectionComboBox.Text;
                if (envSettingsMap.ContainsKey(envName))
                {
                    currentEnvSettings = envSettingsMap[envName];
                    applySettings();

                    if (!updatingEnvFromTray)
                    {
                        trayEnvSelectorComboBox.SelectedIndex = EnvironmentSelectionComboBox.SelectedIndex;
                    }

                    execProperties["_invoker_environment_settings_file"] = InvokerSettingDirectory + envName + ".env.json";
                }
            }
        }

        void reloadSettings()
        {
            string prevcommandstr = commandsComboBox.Text;
            string prevpropertystr = PropertiesComboBox.Text;
            LoadProperties(EnvironmentSelectionComboBox.Text, currentEnvSettings.type);
            comoboBoxRefreshed(commandsComboBox, prevcommandstr);
            comoboBoxRefreshed(PropertiesComboBox, prevpropertystr);
            //			comoboBoxRefreshed(EnvironmentSelectionComboBox);
        }

        void ReloadSettingsButtonClick(object sender, EventArgs e)
        {
            reloadSettings();
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

        EnvironmentSettings GetEnvProperties(string envAlias)
        {
            EnvironmentSettings envProperties = envSettingsMap.ContainsKey(envAlias) ? envSettingsMap[envAlias] : new EnvironmentSettings();
            return envProperties;
        }

        void SaveEnvProperties(string envAlias)
        {
            string envFilePath = InvokerSettingDirectory + envAlias + ".env.json";
            GetEnvProperties(envAlias).saveToFile(envFilePath);
        }

        void Add_EnvironmentButtonClick(object sender, EventArgs e)
        {
            string envName = EnvironmentSelectionComboBox.Text;

            if (!string.IsNullOrEmpty(envName) && !EnvironmentSelectionComboBox.Items.Contains(envName))
            {
                string envFilePath = InvokerSettingDirectory + envName + ".env.json";
                EnvironmentSelectionComboBox.Items.Add(envName);

                EnvironmentSettings newEnvSettings = null;


                if (!File.Exists(envFilePath))
                {
                    if (File.Exists(envFilePath + ".del"))
                    {
                        File.Move(envFilePath + ".del", envFilePath);
                    }
                    else
                    {
                        newEnvSettings = new EnvironmentSettings(currentEnvSettings.properties);
                        string type = invokerSettings.environmentSettings.ContainsKey(currentEnvSettings.type) ? currentEnvSettings.type : "default";
                        newEnvSettings.properties = invokerSettings.environmentSettings[type].properties;
                        newEnvSettings.saveToFile(envFilePath);
                    }
                }

                reloadSettings();
            }
        }

        //Following button is not required. Can be removed
        void Remove_EnvironmentButtonClick(object sender, EventArgs e)
        {
            int selectedIndex = EnvironmentSelectionComboBox.SelectedIndex;

            if (selectedIndex != -1)
            {
                string envName = EnvironmentSelectionComboBox.Text;
                string envFilePath = InvokerSettingDirectory + envName + ".env.json";
                EnvironmentSelectionComboBox.Items.RemoveAt(selectedIndex);
                if (File.Exists(envFilePath))
                {
                    File.Move(envFilePath, envFilePath + ".del");
                }

                EnvironmentCollectionChanged();
            }
        }

        void SaveSettingsButtonClick(object sender, EventArgs e)
        {
            invokerSettings.saveToFile(userSettingsFile);
        }

        void SaveEnvSettingsButtonClick(object sender, EventArgs e)
        {
            string envName = EnvironmentSelectionComboBox.Text;

            if (!string.IsNullOrEmpty(envName))
            {
                SaveEnvProperties(envName);

                if (!envSettingsMap.ContainsKey(envName))
                {
                    EnvironmentSelectionComboBox.Items.Add(envName);
                }

                envSettingsMap[EnvironmentSelectionComboBox.Text] = GetEnvProperties(EnvironmentSelectionComboBox.Text);
            }
        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("Invoker is a launcher tool to work with multiple environments.\rFor queries contact manianvss@hotmail.com.", "About");
        }

        bool hotKeysOn = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        void ToggleHotKeysButtonClick(object sender, EventArgs e)
        {
            if (hotKeysOn)
            {
                disableHotKeys();
                hotKeysOn = false;
                ToggleHotKeysButton.Text = "Enable Hotkeys";
            }
            else
            {
                enableHotkeys();
                hotKeysOn = true;
                ToggleHotKeysButton.Text = "Disable Hotkeys";
            }
            TrayToggleHotKeyMenuItem.Text = ToggleHotKeysButton.Text;

        }

        void PropertiesComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PropertiesComboBox.SelectedIndex != -1)
            {
                if (!string.IsNullOrEmpty(PropertiesComboBox.Text))
                {
                    propertyValueTextBox.Text = currentEnvSettings.properties[PropertiesComboBox.Text];
                }
            }
        }

        void PropertyCopyButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(propertyValueTextBox.Text))
            {
                Clipboard.SetText(propertyValueTextBox.Text);
            }
        }

        void Add_PropertyButtonClick(object sender, EventArgs e)
        {
            string propertyName = PropertiesComboBox.Text;

            if (!string.IsNullOrEmpty(propertyName))
            {
                currentEnvSettings.properties[propertyName] = propertyValueTextBox.Text;
                applySettings();
            }
        }

        void Remove_PropertyButton_Click(object sender, EventArgs e)
        {
            if (PropertiesComboBox.SelectedIndex != -1)
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

        void AutoSelectListBoxItem(ListBox lb)
        {
            if ((lb.SelectedIndex == -1) && (lb.Items.Count > 0))
            {
                lb.SelectedIndex = 0;
            }
        }

        //		void AutoSelectComboBoxItem(ComboBox comboBox)
        //		{
        //			if((comboBox.SelectedIndex==-1) && (comboBox.Items.Count>0))
        //			{
        //				if(comboBox.Items.Contains(comboBox.Text))
        //				{
        //					comboBox.SelectedIndex=comboBox.Items.IndexOf(comboBox.Text);
        //				}
        //				else
        //				{
        //					comboBox.SelectedIndex=0;
        //				}
        //			}
        //		}

        void SelectNextListBoxItem(ListBox listBox)
        {
            if (listBox.Items.Count > 0)
            {
                listBox.SelectedIndex = (listBox.SelectedIndex + 1) % listBox.Items.Count;
            }
        }

        void SelectNextComboBoxItem(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = (comboBox.SelectedIndex + 1) % comboBox.Items.Count;
            }
        }

        void NextEnvToolStripMenuItemClick(object sender, EventArgs e)
        {
            SelectNextComboBoxItem(EnvironmentSelectionComboBox);
        }

        void PrevEnvironmentToolStripMenuItemClick(object sender, EventArgs e)
        {
            if ((EnvironmentSelectionComboBox.SelectedIndex != -1) && (EnvironmentSelectionComboBox.Items.Count > 0))
            {
                EnvironmentSelectionComboBox.SelectedIndex = (EnvironmentSelectionComboBox.SelectedIndex + 1) % EnvironmentSelectionComboBox.Items.Count;
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

        bool updatingEnvFromTray = false;
        void TrayEnvSelectorComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            updatingEnvFromTray = true;
            EnvironmentSelectionComboBox.SelectedIndex = trayEnvSelectorComboBox.SelectedIndex;
            updatingEnvFromTray = false;
        }

        void ToolStripMenuItem1Click(object sender, EventArgs e)
        {
            ToggleHotKeysButtonClick(sender, e);
        }

        void waitForProcessExit(Process process)
        {
            Cursor.Current = Cursors.WaitCursor;
            process.WaitForExit();
            execProperties["exitCode"] = "" + process.ExitCode;
            Cursor.Current = Cursors.Default;
        }

        //		void performInvokeOnNewThread(InvokeCommand invoke)
        //		{
        //			new Thread (()=>performInvoke(invoke)).Start();
        //		}

        private readonly object invokeLock = new object();

        void performInvoke(InvokeCommand invoke)
        {
            lock (invokeLock)
            {
                foreach (ExecDetails command in invoke.commands)
                {
                    switch (command.type)
                    {
                        case "run":
                            Process process = ((command.args != null) && !string.IsNullOrEmpty(command.args[0]))
                                ? Process.Start(replaceProperties(command.path), replaceProperties(string.Join(" ", command.args)))
                                : Process.Start(replaceProperties(command.path));

                            if (command.waitForExit)
                            {
                                waitForProcessExit(process);
                            }
                            break;

                        case "exec":
                            process = new Process();
                            process.StartInfo.UseShellExecute = command.shell;
                            process.StartInfo.FileName = replaceProperties(command.path);
                            process.StartInfo.Arguments = replaceProperties(string.Join(" ", command.args));
                            process.StartInfo.CreateNoWindow = command.hideWindow;

                            if (!string.IsNullOrEmpty(command.workingDir) && Directory.Exists(command.workingDir))
                            {
                                process.StartInfo.WorkingDirectory = command.workingDir;
                            }

                            if (command.saveOutput)
                            {
                                process.StartInfo.RedirectStandardOutput = true;
                                process.StartInfo.RedirectStandardError = true;
                                process.StartInfo.UseShellExecute = false;
                            }

                            foreach (KeyValuePair<string, string> kvp in command.env)
                            {
                                process.StartInfo.EnvironmentVariables.Add(replaceProperties(kvp.Key), replaceProperties(kvp.Value));
                            }

                            process.Start();

                            if (command.saveOutput)
                            {
                                string output = process.StandardOutput.ReadToEnd();
                                string err = process.StandardError.ReadToEnd();
                                waitForProcessExit(process);

                                if (string.IsNullOrEmpty(command.outputProperty))
                                {
                                    command.outputProperty = "exec.out";
                                }

                                execProperties[replaceProperties(command.outputProperty)] = output + err;
                            }
                            else
                            {
                                if (command.waitForExit)
                                {
                                    waitForProcessExit(process);
                                }
                            }
                            break;

                        case "reloadSettings":
                            reloadSettings();
                            break;

                        case "OpenSettingsFile":
                            OpenSettingsFile();
                            break;

                        case "OpenEnvSettingsFile":
                            OpenEnvSettingsFile();
                            break;

                        case "toggleShowInvokerWindow":
                            toggleShowWindow();
                            break;

                        case "toggleClipBoardCapture":
                            ToggleClipBoardCapture();
                            break;

                        case "copyNextToClipBoard":
                            copyNextToClipBoard();
                            break;

                        case "copyFromClipBoard":
                            copyFromClipBoard();
                            break;

                        case "clearClipBoard":
                            clearClipBoard();
                            break;

                        case "exportClipBoard":
                            string directory = (!string.IsNullOrEmpty(command.workingDir) && Directory.Exists(command.workingDir)) ? command.workingDir : InvokerSettingDirectory;
                            exportClipBoard(directory);
                            break;

                        default:
                            MessageBox.Show(replaceProperties(command.comments), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            }
        }

        public void comoboBoxRefreshed(ComboBox comboBox)
        {
            comoboBoxRefreshed(comboBox, comboBox.Text);
        }

        public void comoboBoxRefreshed(ComboBox comboBox, string text)
        {
            if ((comboBox.SelectedIndex == -1) && comboBox.Items.Contains(text))
            {
                comboBox.SelectedIndex = comboBox.Items.IndexOf(text);
            }
            else
            {
                comboBox.SelectedIndex = (comboBox.Items.Count > 0) ? 0 : -1;
            }
        }

        void LaunchCustomCommandButtonClick(object sender, EventArgs e)
        {
            int selectedIndex = commandsComboBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                string prevcommandstr = commandsComboBox.Text;
                string prevpropertystr = PropertiesComboBox.Text;

                string type = invokerSettings.environmentSettings.ContainsKey(currentEnvSettings.type) ? currentEnvSettings.type : "default";
                InvokeCommand invoke = invokerSettings.environmentSettings[type].invokes[selectedIndex];
                invokesToProcess.Enqueue(invoke);

                comoboBoxRefreshed(commandsComboBox, prevcommandstr);
                comoboBoxRefreshed(PropertiesComboBox, prevpropertystr);
            }
        }

        void LaunchCustomCommandUIButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (commandButtonInvokeDict.ContainsKey(clickedButton))
            {
                invokesToProcess.Enqueue(commandButtonInvokeDict[clickedButton]);
            }
        }

        void CommandsComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (commandsComboBox.SelectedIndex != -1)
            {
                if (commandsDict.ContainsKey(commandsComboBox.Text))
                {
                    InvokeCommand invoke = commandsDict[commandsComboBox.Text];
                    string tooltip = (invoke.hotkey == null) ? invoke.description : invoke.description + "\rHotkey: " + invoke.hotkey;
                    commandToolTips.SetToolTip(LaunchCustomCommandButton, tooltip);
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

        void processClipBoard(Stream stream)
        {
            clipBoardQueue.Add(stream);
            ClipboardListBox.Items.Add("Audio: " + stream.Length);
        }

        void processClipBoard(string textData)
        {
            if (!string.IsNullOrEmpty(textData))
            {
                clipBoardQueue.Add(textData);
                ClipboardListBox.Items.Add("Text: " + textData);
            }
        }

        void processClipBoard(Image image)
        {
            clipBoardQueue.Add(image);
            ClipboardListBox.Items.Add("Image: " + image.Size);
        }

        void processClipBoard(StringCollection fileList)
        {
            clipBoardQueue.Add(fileList);
            ClipboardListBox.Items.Add("Files: " + fileList.Count);
        }

        void processClipBoard(IDataObject dataObject)
        {
            clipBoardQueue.Add(dataObject);
            ClipboardListBox.Items.Add("Data: " + dataObject);
        }

        string prevStr = null;
        void MainFormClipboardChangedToAudioStream(object sender, ClipboardChangedAudioStreamEventArgs e)
        {
            prevStr = null;
            processClipBoard(e.audioStream);
        }

        void MainFormClipboardChangedToData(object sender, ClipboardChangedDataEventArgs e)
        {
            prevStr = null;
            processClipBoard(e.DataObject);
        }

        void MainFormClipboardChangedToFileList(object sender, ClipboardChangedFileListEventArgs e)
        {
            prevStr = null;
            processClipBoard(e.fileList);
        }

        void MainFormClipboardChangedToImage(object sender, ClipboardChangedImageEventArgs e)
        {
            prevStr = null;
            processClipBoard(e.image);
        }

        void MainFormClipboardChangedToText(object sender, ClipboardChangedTextEventArgs e)
        {
            if (!e.textData.Equals(prevStr))
            {
                prevStr = e.textData;
                processClipBoard(e.textData);
            }
        }

        void ClipboardListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClipboardListBox.SelectedIndex != -1)
            {
                object data = clipBoardQueue[ClipboardListBox.SelectedIndex];

                if (data is string)
                {
                    ClipboardViewerTabControl.SelectedTab = CBV_TextTab;
                    //					ClipViewRTB.Clear();
                    ClipViewRTB.Text = (string)data;
                }
                else if (data is Image)
                {
                    ClipboardViewerTabControl.SelectedTab = CBV_ImageTab;
                    ClipViewPictureBox.Image = (Image)data;
                }
                else if (data is StringCollection)
                {
                    ClipboardViewerTabControl.SelectedTab = CBV_TextTab;
                    ClipViewRTB.Clear();

                    foreach (String str in (StringCollection)data)
                    {
                        ClipViewRTB.AppendText(str + "\n");
                    }
                }
                else
                {
                    ClipboardViewerTabControl.SelectedTab = CBV_DataTab;
                }

                ClipboardListBox.Focus();
            }
        }

        public void ToggleClipBoardCapture()
        {
            ToggleClipBoardNotifications();
            string captureControlText = (clipboardNotificationsEnabled ? "Disable" : "Enable") + " Clipboard Capture";
            clipboardCaptureToolStripMenuItem.Text = captureControlText;
            EnableClipboardCaptureButton.Text = captureControlText;
        }

        void EnableClipboardCaptureButtonClick(object sender, EventArgs e)
        {
            ToggleClipBoardCapture();
        }

        void ClipboardCaptureToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToggleClipBoardCapture();
        }

        void copyNextToClipBoard()
        {
            AutoSelectListBoxItem(ClipboardListBox);
            copyToClipBoard();
            SelectNextListBoxItem(ClipboardListBox);
        }

        void copyToClipBoard()
        {
            int selectedIndex = ClipboardListBox.SelectedIndex;

            if (selectedIndex != -1)
            {
                bool enableBack = DisableClipBoardNotifications();

                object data = clipBoardQueue[selectedIndex];

                if (data is string)
                {
                    string strToSet = (string)data;
                    if (!string.IsNullOrEmpty(strToSet))
                    {
                        Clipboard.SetText(strToSet);
                    }
                }
                else if (data is Image)
                {
                    Clipboard.SetImage((Image)data);
                }
                else if (data is StringCollection)
                {
                    Clipboard.SetFileDropList((StringCollection)data);
                }
                else if (data is Stream)
                {
                    Clipboard.SetAudio((Stream)data);
                }
                else
                {
                    Clipboard.SetDataObject((IDataObject)data);
                }

                if (enableBack)
                {
                    EnableClipBoardNotifications();
                }
            }
        }

        void CopyToClipBoardButtonClick(object sender, EventArgs e)
        {
            copyToClipBoard();
        }

        void ClipboardListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            copyToClipBoard();
        }

        void copyFromClipBoard()
        {
            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                processClipBoard(text);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                StringCollection fileList = Clipboard.GetFileDropList();
                processClipBoard(fileList);
            }
            else if (Clipboard.ContainsImage())
            {
                Image image = Clipboard.GetImage();
                processClipBoard(image);
            }
            else if (Clipboard.ContainsAudio())
            {
                Stream audioStream = Clipboard.GetAudioStream();
                processClipBoard(audioStream);
            }
            else
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                processClipBoard(dataObject);
            }
        }

        void ClipboardListBoxKeyDown(object sender, KeyEventArgs e)
        {
            int selectedIndex = ClipboardListBox.SelectedIndex;

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (selectedIndex != -1)
                    {
                        clipBoardQueue.RemoveAt(selectedIndex);
                        ClipboardListBox.Items.RemoveAt(selectedIndex);
                    }
                    break;

                case Keys.V:
                    if (e.Control)
                    {
                        copyFromClipBoard();
                    }
                    break;

                case Keys.C:
                    if (e.Control)
                    {
                        copyToClipBoard();
                    }
                    break;
            }
        }

        void clearClipBoard()
        {
            ClipboardListBox.Items.Clear();
            clipBoardQueue.Clear();
        }

        void ClearClipBoardButtonClick(object sender, EventArgs e)
        {
            clearClipBoard();
        }

        void PasteFromClipBoardButtonClick(object sender, EventArgs e)
        {
            copyFromClipBoard();
        }

        void exportClipBoard(string folder)
        {
            foreach (Object data in clipBoardQueue)
            {
                string tempFile = Path.GetTempFileName();

                if (tempFile.Contains("\\"))
                {
                    tempFile = tempFile.Substring(tempFile.LastIndexOf("\\") + 1);
                }
                if (tempFile.Contains("."))
                {
                    tempFile = tempFile.Substring(0, tempFile.IndexOf("."));
                }

                tempFile = Path.Combine(folder, tempFile);

                if (data is string)
                {
                    File.WriteAllText(tempFile + ".txt", (string)data);
                }
                else if (data is Image)
                {
                    ((Image)data).Save(tempFile + ".png");
                }
                else if (data is StringCollection)
                {
                    StringBuilder dataToWrite = new StringBuilder();

                    foreach (String str in (StringCollection)data)
                    {
                        dataToWrite.Append(str + "\n");
                    }
                    File.WriteAllText(tempFile + ".files", dataToWrite.ToString());
                }
                else
                {
                    //Can't save unknown data object.
                }
            }
        }


        void ExportClipBoardButtonClick(object sender, EventArgs e)
        {
            switch (ExportClipboardFolderBrowserDialog.ShowDialog())
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    string clipBoardExportDir = ExportClipboardFolderBrowserDialog.SelectedPath;
                    exportClipBoard(clipBoardExportDir);
                    break;
            }
        }

        void PickInvokeTimerTick(object sender, EventArgs e)
        {
            InvokeCommand invokeCommand = null;

            while (invokesToProcess.TryDequeue(out invokeCommand))
            {
                performInvoke(invokeCommand);
            }
        }


        private void OpenEnvSettingsFile()
        {
            Process.Start(Path.Combine(InvokerSettingDirectory, EnvironmentSelectionComboBox.Text + ENV_JSON_EXTN));
        }

        private void OpenSettingsFile()
        {
            Process.Start(userSettingsFile);
        }

        private void OpenSettingsButton_Click(object sender, EventArgs e)
        {
            OpenSettingsFile();
        }

        private void OpenEnvSettingsButton_Click(object sender, EventArgs e)
        {
            OpenEnvSettingsFile();
        }
    }
}
