/*
 * Created by SharpDevelop.
 * User: Manian VSS
 * Date: 12/30/2018
 * Time: 12:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Invoker
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.StatusStrip StatusStrip_InvokerMain;
		private System.Windows.Forms.TabControl TabControl_InvokerMain;
		private System.Windows.Forms.TabPage MainTab;
		private System.Windows.Forms.SplitContainer SC_DashBoardMain;
		private System.Windows.Forms.Label label_EnvSelect;
		private System.Windows.Forms.ComboBox EnvironmentSelectionComboBox;
		
		private System.Windows.Forms.Button ReloadSettingsButton;
		private System.Windows.Forms.NotifyIcon MainNotifyIcon;
		private System.Windows.Forms.GroupBox GroupBox_Tools;
		private System.Windows.Forms.Button Add_EnvironmentButton;
		private System.Windows.Forms.Button SaveSettingsButton;
		private System.Windows.Forms.Button SaveEnvSettingsButton;
		private System.Windows.Forms.MenuStrip Invoker_MainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Button ToggleHotKeysButton;
		private System.Windows.Forms.Label label_Properties;
		private System.Windows.Forms.TextBox propertyValueTextBox;
		private System.Windows.Forms.Button PropertyCopyButton;
		private System.Windows.Forms.ComboBox PropertiesComboBox;
		private System.Windows.Forms.Button Remove_PropertyButton;
		private System.Windows.Forms.Button Add_PropertyButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ContextMenuStrip trayContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toggleShowToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox trayEnvSelectorComboBox;
		private System.Windows.Forms.ToolStripMenuItem TrayToggleHotKeyMenuItem;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button LaunchCustomCommandButton;
		private System.Windows.Forms.ComboBox commandsComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip commandToolTips;
		private System.Windows.Forms.Button Remove_EnvironmentButton;
		private System.Windows.Forms.FlowLayoutPanel commandsFlowLayoutPanel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button HideInvokerWindowButton;
		private System.Windows.Forms.TabPage ClipboardTab;
		private System.Windows.Forms.SplitContainer SC_CipboardViewSplitter;
		private System.Windows.Forms.ListBox ClipboardListBox;
		private System.Windows.Forms.TabControl ClipboardViewerTabControl;
		private System.Windows.Forms.TabPage CBV_TextTab;
		private System.Windows.Forms.TabPage CBV_ImageTab;
		private System.Windows.Forms.TabPage CBV_DataTab;
		private System.Windows.Forms.RichTextBox ClipViewRTB;
		private System.Windows.Forms.PictureBox ClipViewPictureBox;
		private System.Windows.Forms.ToolStripMenuItem clipboardCaptureToolStripMenuItem;
		private System.Windows.Forms.SplitContainer SC_ClipBoardViewerMain;
		private System.Windows.Forms.Button EnableClipboardCaptureButton;
		private System.Windows.Forms.Button CopyToClipBoardButton;
		private System.Windows.Forms.Button ClearClipBoardButton;
		private System.Windows.Forms.Button PasteFromClipBoardButton;
		private System.Windows.Forms.Button ExportClipBoardButton;
		private System.Windows.Forms.FolderBrowserDialog ExportClipboardFolderBrowserDialog;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatusStrip_InvokerMain = new System.Windows.Forms.StatusStrip();
            this.MainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TrayToggleHotKeyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboardCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayEnvSelectorComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.Invoker_MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TabControl_InvokerMain = new System.Windows.Forms.TabControl();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.SC_DashBoardMain = new System.Windows.Forms.SplitContainer();
            this.HideInvokerWindowButton = new System.Windows.Forms.Button();
            this.ToggleHotKeysButton = new System.Windows.Forms.Button();
            this.SaveEnvSettingsButton = new System.Windows.Forms.Button();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.Remove_EnvironmentButton = new System.Windows.Forms.Button();
            this.Add_EnvironmentButton = new System.Windows.Forms.Button();
            this.ReloadSettingsButton = new System.Windows.Forms.Button();
            this.EnvironmentSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.label_EnvSelect = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GroupBox_Tools = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LaunchCustomCommandButton = new System.Windows.Forms.Button();
            this.commandsComboBox = new System.Windows.Forms.ComboBox();
            this.Remove_PropertyButton = new System.Windows.Forms.Button();
            this.Add_PropertyButton = new System.Windows.Forms.Button();
            this.PropertiesComboBox = new System.Windows.Forms.ComboBox();
            this.PropertyCopyButton = new System.Windows.Forms.Button();
            this.label_Properties = new System.Windows.Forms.Label();
            this.propertyValueTextBox = new System.Windows.Forms.TextBox();
            this.commandsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ClipboardTab = new System.Windows.Forms.TabPage();
            this.SC_ClipBoardViewerMain = new System.Windows.Forms.SplitContainer();
            this.ExportClipBoardButton = new System.Windows.Forms.Button();
            this.PasteFromClipBoardButton = new System.Windows.Forms.Button();
            this.ClearClipBoardButton = new System.Windows.Forms.Button();
            this.CopyToClipBoardButton = new System.Windows.Forms.Button();
            this.EnableClipboardCaptureButton = new System.Windows.Forms.Button();
            this.SC_CipboardViewSplitter = new System.Windows.Forms.SplitContainer();
            this.ClipboardListBox = new System.Windows.Forms.ListBox();
            this.ClipboardViewerTabControl = new System.Windows.Forms.TabControl();
            this.CBV_TextTab = new System.Windows.Forms.TabPage();
            this.ClipViewRTB = new System.Windows.Forms.RichTextBox();
            this.CBV_ImageTab = new System.Windows.Forms.TabPage();
            this.ClipViewPictureBox = new System.Windows.Forms.PictureBox();
            this.CBV_DataTab = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.commandToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.ExportClipboardFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.PickInvokeTimer = new System.Windows.Forms.Timer(this.components);
            this.label_EnvType = new System.Windows.Forms.Label();
            this.EnvironmentTypeTextBox = new System.Windows.Forms.TextBox();
            this.trayContextMenuStrip.SuspendLayout();
            this.Invoker_MainMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TabControl_InvokerMain.SuspendLayout();
            this.MainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_DashBoardMain)).BeginInit();
            this.SC_DashBoardMain.Panel1.SuspendLayout();
            this.SC_DashBoardMain.Panel2.SuspendLayout();
            this.SC_DashBoardMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.GroupBox_Tools.SuspendLayout();
            this.ClipboardTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_ClipBoardViewerMain)).BeginInit();
            this.SC_ClipBoardViewerMain.Panel1.SuspendLayout();
            this.SC_ClipBoardViewerMain.Panel2.SuspendLayout();
            this.SC_ClipBoardViewerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_CipboardViewSplitter)).BeginInit();
            this.SC_CipboardViewSplitter.Panel1.SuspendLayout();
            this.SC_CipboardViewSplitter.Panel2.SuspendLayout();
            this.SC_CipboardViewSplitter.SuspendLayout();
            this.ClipboardViewerTabControl.SuspendLayout();
            this.CBV_TextTab.SuspendLayout();
            this.CBV_ImageTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClipViewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusStrip_InvokerMain
            // 
            this.StatusStrip_InvokerMain.Location = new System.Drawing.Point(0, 473);
            this.StatusStrip_InvokerMain.Name = "StatusStrip_InvokerMain";
            this.StatusStrip_InvokerMain.Size = new System.Drawing.Size(701, 22);
            this.StatusStrip_InvokerMain.TabIndex = 1;
            this.StatusStrip_InvokerMain.Text = "statusStrip1";
            // 
            // MainNotifyIcon
            // 
            this.MainNotifyIcon.BalloonTipText = "Click on this notification icon to minimize invoker";
            this.MainNotifyIcon.ContextMenuStrip = this.trayContextMenuStrip;
            this.MainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MainNotifyIcon.Icon")));
            this.MainNotifyIcon.Text = "Invoker";
            this.MainNotifyIcon.Visible = true;
            this.MainNotifyIcon.DoubleClick += new System.EventHandler(this.MainNotifyIconDoubleClick);
            // 
            // trayContextMenuStrip
            // 
            this.trayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrayToggleHotKeyMenuItem,
            this.toggleShowToolStripMenuItem,
            this.clipboardCaptureToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.trayEnvSelectorComboBox});
            this.trayContextMenuStrip.Name = "trayContextMenuStrip";
            this.trayContextMenuStrip.Size = new System.Drawing.Size(210, 119);
            // 
            // TrayToggleHotKeyMenuItem
            // 
            this.TrayToggleHotKeyMenuItem.Name = "TrayToggleHotKeyMenuItem";
            this.TrayToggleHotKeyMenuItem.Size = new System.Drawing.Size(209, 22);
            this.TrayToggleHotKeyMenuItem.Text = "Enable HotKeys";
            this.TrayToggleHotKeyMenuItem.Click += new System.EventHandler(this.ToggleHotKeysButtonClick);
            // 
            // toggleShowToolStripMenuItem
            // 
            this.toggleShowToolStripMenuItem.Name = "toggleShowToolStripMenuItem";
            this.toggleShowToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.toggleShowToolStripMenuItem.Text = "Toggle Show";
            this.toggleShowToolStripMenuItem.Click += new System.EventHandler(this.ToggleShowToolStripMenuItemClick);
            // 
            // clipboardCaptureToolStripMenuItem
            // 
            this.clipboardCaptureToolStripMenuItem.Name = "clipboardCaptureToolStripMenuItem";
            this.clipboardCaptureToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.clipboardCaptureToolStripMenuItem.Text = "Enable Clipboard Capture";
            this.clipboardCaptureToolStripMenuItem.Click += new System.EventHandler(this.ClipboardCaptureToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // trayEnvSelectorComboBox
            // 
            this.trayEnvSelectorComboBox.Name = "trayEnvSelectorComboBox";
            this.trayEnvSelectorComboBox.Size = new System.Drawing.Size(121, 23);
            this.trayEnvSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.TrayEnvSelectorComboBoxSelectedIndexChanged);
            // 
            // Invoker_MainMenuStrip
            // 
            this.Invoker_MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.Invoker_MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.Invoker_MainMenuStrip.Name = "Invoker_MainMenuStrip";
            this.Invoker_MainMenuStrip.Size = new System.Drawing.Size(701, 24);
            this.Invoker_MainMenuStrip.TabIndex = 3;
            this.Invoker_MainMenuStrip.Text = "Invoker Main Menu Strip";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TabControl_InvokerMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 449);
            this.panel1.TabIndex = 4;
            // 
            // TabControl_InvokerMain
            // 
            this.TabControl_InvokerMain.Controls.Add(this.MainTab);
            this.TabControl_InvokerMain.Controls.Add(this.ClipboardTab);
            this.TabControl_InvokerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl_InvokerMain.Location = new System.Drawing.Point(0, 0);
            this.TabControl_InvokerMain.Name = "TabControl_InvokerMain";
            this.TabControl_InvokerMain.SelectedIndex = 0;
            this.TabControl_InvokerMain.Size = new System.Drawing.Size(701, 449);
            this.TabControl_InvokerMain.TabIndex = 2;
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.SC_DashBoardMain);
            this.MainTab.Location = new System.Drawing.Point(4, 22);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(693, 423);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Main";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // SC_DashBoardMain
            // 
            this.SC_DashBoardMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_DashBoardMain.Location = new System.Drawing.Point(3, 3);
            this.SC_DashBoardMain.Name = "SC_DashBoardMain";
            this.SC_DashBoardMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SC_DashBoardMain.Panel1
            // 
            this.SC_DashBoardMain.Panel1.Controls.Add(this.EnvironmentTypeTextBox);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.label_EnvType);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.HideInvokerWindowButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.ToggleHotKeysButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.SaveEnvSettingsButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.SaveSettingsButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.Remove_EnvironmentButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.Add_EnvironmentButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.ReloadSettingsButton);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.EnvironmentSelectionComboBox);
            this.SC_DashBoardMain.Panel1.Controls.Add(this.label_EnvSelect);
            // 
            // SC_DashBoardMain.Panel2
            // 
            this.SC_DashBoardMain.Panel2.Controls.Add(this.splitContainer1);
            this.SC_DashBoardMain.Size = new System.Drawing.Size(687, 417);
            this.SC_DashBoardMain.SplitterDistance = 91;
            this.SC_DashBoardMain.TabIndex = 0;
            // 
            // HideInvokerWindowButton
            // 
            this.HideInvokerWindowButton.Location = new System.Drawing.Point(125, 56);
            this.HideInvokerWindowButton.Name = "HideInvokerWindowButton";
            this.HideInvokerWindowButton.Size = new System.Drawing.Size(113, 23);
            this.HideInvokerWindowButton.TabIndex = 8;
            this.HideInvokerWindowButton.Text = "Hide window";
            this.HideInvokerWindowButton.UseVisualStyleBackColor = true;
            this.HideInvokerWindowButton.Click += new System.EventHandler(this.HideInvokerWindowButtonClick);
            // 
            // ToggleHotKeysButton
            // 
            this.ToggleHotKeysButton.Location = new System.Drawing.Point(9, 56);
            this.ToggleHotKeysButton.Name = "ToggleHotKeysButton";
            this.ToggleHotKeysButton.Size = new System.Drawing.Size(113, 23);
            this.ToggleHotKeysButton.TabIndex = 7;
            this.ToggleHotKeysButton.Text = "Enable Hotkeys";
            this.ToggleHotKeysButton.UseVisualStyleBackColor = true;
            this.ToggleHotKeysButton.Click += new System.EventHandler(this.ToggleHotKeysButtonClick);
            // 
            // SaveEnvSettingsButton
            // 
            this.SaveEnvSettingsButton.Location = new System.Drawing.Point(244, 56);
            this.SaveEnvSettingsButton.Name = "SaveEnvSettingsButton";
            this.SaveEnvSettingsButton.Size = new System.Drawing.Size(122, 23);
            this.SaveEnvSettingsButton.TabIndex = 6;
            this.SaveEnvSettingsButton.Text = "Save Env Settings";
            this.SaveEnvSettingsButton.UseVisualStyleBackColor = true;
            this.SaveEnvSettingsButton.Click += new System.EventHandler(this.SaveEnvSettingsButtonClick);
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(493, 56);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(122, 23);
            this.SaveSettingsButton.TabIndex = 5;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Visible = false;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButtonClick);
            // 
            // Remove_EnvironmentButton
            // 
            this.Remove_EnvironmentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remove_EnvironmentButton.Location = new System.Drawing.Point(516, 8);
            this.Remove_EnvironmentButton.Name = "Remove_EnvironmentButton";
            this.Remove_EnvironmentButton.Size = new System.Drawing.Size(21, 23);
            this.Remove_EnvironmentButton.TabIndex = 4;
            this.Remove_EnvironmentButton.Text = "-";
            this.Remove_EnvironmentButton.UseVisualStyleBackColor = true;
            this.Remove_EnvironmentButton.Visible = false;
            this.Remove_EnvironmentButton.Click += new System.EventHandler(this.Remove_EnvironmentButtonClick);
            // 
            // Add_EnvironmentButton
            // 
            this.Add_EnvironmentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_EnvironmentButton.Location = new System.Drawing.Point(489, 8);
            this.Add_EnvironmentButton.Name = "Add_EnvironmentButton";
            this.Add_EnvironmentButton.Size = new System.Drawing.Size(21, 23);
            this.Add_EnvironmentButton.TabIndex = 3;
            this.Add_EnvironmentButton.Text = "+";
            this.Add_EnvironmentButton.UseVisualStyleBackColor = true;
            this.Add_EnvironmentButton.Click += new System.EventHandler(this.Add_EnvironmentButtonClick);
            // 
            // ReloadSettingsButton
            // 
            this.ReloadSettingsButton.Location = new System.Drawing.Point(372, 56);
            this.ReloadSettingsButton.Name = "ReloadSettingsButton";
            this.ReloadSettingsButton.Size = new System.Drawing.Size(115, 23);
            this.ReloadSettingsButton.TabIndex = 2;
            this.ReloadSettingsButton.Text = "Reload Settings";
            this.ReloadSettingsButton.UseVisualStyleBackColor = true;
            this.ReloadSettingsButton.Click += new System.EventHandler(this.ReloadSettingsButtonClick);
            // 
            // EnvironmentSelectionComboBox
            // 
            this.EnvironmentSelectionComboBox.FormattingEnabled = true;
            this.EnvironmentSelectionComboBox.Location = new System.Drawing.Point(84, 8);
            this.EnvironmentSelectionComboBox.Name = "EnvironmentSelectionComboBox";
            this.EnvironmentSelectionComboBox.Size = new System.Drawing.Size(393, 21);
            this.EnvironmentSelectionComboBox.TabIndex = 1;
            this.EnvironmentSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.EnvironmentSelectionComboBoxSelectedIndexChanged);
            // 
            // label_EnvSelect
            // 
            this.label_EnvSelect.Location = new System.Drawing.Point(5, 11);
            this.label_EnvSelect.Name = "label_EnvSelect";
            this.label_EnvSelect.Size = new System.Drawing.Size(73, 23);
            this.label_EnvSelect.TabIndex = 0;
            this.label_EnvSelect.Text = "Environment:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GroupBox_Tools);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.commandsFlowLayoutPanel);
            this.splitContainer1.Size = new System.Drawing.Size(687, 322);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 46;
            // 
            // GroupBox_Tools
            // 
            this.GroupBox_Tools.Controls.Add(this.label1);
            this.GroupBox_Tools.Controls.Add(this.LaunchCustomCommandButton);
            this.GroupBox_Tools.Controls.Add(this.commandsComboBox);
            this.GroupBox_Tools.Controls.Add(this.Remove_PropertyButton);
            this.GroupBox_Tools.Controls.Add(this.Add_PropertyButton);
            this.GroupBox_Tools.Controls.Add(this.PropertiesComboBox);
            this.GroupBox_Tools.Controls.Add(this.PropertyCopyButton);
            this.GroupBox_Tools.Controls.Add(this.label_Properties);
            this.GroupBox_Tools.Controls.Add(this.propertyValueTextBox);
            this.GroupBox_Tools.Location = new System.Drawing.Point(4, 3);
            this.GroupBox_Tools.Name = "GroupBox_Tools";
            this.GroupBox_Tools.Size = new System.Drawing.Size(683, 149);
            this.GroupBox_Tools.TabIndex = 44;
            this.GroupBox_Tools.TabStop = false;
            this.GroupBox_Tools.Text = "Tools";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 23);
            this.label1.TabIndex = 80;
            this.label1.Text = "Commands:";
            // 
            // LaunchCustomCommandButton
            // 
            this.LaunchCustomCommandButton.Location = new System.Drawing.Point(267, 95);
            this.LaunchCustomCommandButton.Name = "LaunchCustomCommandButton";
            this.LaunchCustomCommandButton.Size = new System.Drawing.Size(56, 20);
            this.LaunchCustomCommandButton.TabIndex = 79;
            this.LaunchCustomCommandButton.Text = "Launch";
            this.LaunchCustomCommandButton.UseVisualStyleBackColor = true;
            this.LaunchCustomCommandButton.Click += new System.EventHandler(this.LaunchCustomCommandButtonClick);
            // 
            // commandsComboBox
            // 
            this.commandsComboBox.FormattingEnabled = true;
            this.commandsComboBox.Location = new System.Drawing.Point(88, 94);
            this.commandsComboBox.Name = "commandsComboBox";
            this.commandsComboBox.Size = new System.Drawing.Size(173, 21);
            this.commandsComboBox.TabIndex = 78;
            this.commandsComboBox.SelectedIndexChanged += new System.EventHandler(this.CommandsComboBoxSelectedIndexChanged);
            // 
            // Remove_PropertyButton
            // 
            this.Remove_PropertyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remove_PropertyButton.Location = new System.Drawing.Point(240, 46);
            this.Remove_PropertyButton.Name = "Remove_PropertyButton";
            this.Remove_PropertyButton.Size = new System.Drawing.Size(21, 23);
            this.Remove_PropertyButton.TabIndex = 14;
            this.Remove_PropertyButton.Text = "-";
            this.Remove_PropertyButton.UseVisualStyleBackColor = true;
            this.Remove_PropertyButton.Click += new System.EventHandler(this.Remove_PropertyButton_Click);
            // 
            // Add_PropertyButton
            // 
            this.Add_PropertyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add_PropertyButton.Location = new System.Drawing.Point(213, 46);
            this.Add_PropertyButton.Name = "Add_PropertyButton";
            this.Add_PropertyButton.Size = new System.Drawing.Size(21, 23);
            this.Add_PropertyButton.TabIndex = 13;
            this.Add_PropertyButton.Text = "+";
            this.Add_PropertyButton.UseVisualStyleBackColor = true;
            this.Add_PropertyButton.Click += new System.EventHandler(this.Add_PropertyButtonClick);
            // 
            // PropertiesComboBox
            // 
            this.PropertiesComboBox.FormattingEnabled = true;
            this.PropertiesComboBox.Location = new System.Drawing.Point(88, 19);
            this.PropertiesComboBox.Name = "PropertiesComboBox";
            this.PropertiesComboBox.Size = new System.Drawing.Size(173, 21);
            this.PropertiesComboBox.TabIndex = 12;
            this.PropertiesComboBox.SelectedIndexChanged += new System.EventHandler(this.PropertiesComboBoxSelectedIndexChanged);
            // 
            // PropertyCopyButton
            // 
            this.PropertyCopyButton.Location = new System.Drawing.Point(630, 22);
            this.PropertyCopyButton.Name = "PropertyCopyButton";
            this.PropertyCopyButton.Size = new System.Drawing.Size(44, 23);
            this.PropertyCopyButton.TabIndex = 9;
            this.PropertyCopyButton.Text = "Copy";
            this.PropertyCopyButton.UseVisualStyleBackColor = true;
            this.PropertyCopyButton.Click += new System.EventHandler(this.PropertyCopyButton_Click);
            // 
            // label_Properties
            // 
            this.label_Properties.Location = new System.Drawing.Point(11, 22);
            this.label_Properties.Name = "label_Properties";
            this.label_Properties.Size = new System.Drawing.Size(71, 23);
            this.label_Properties.TabIndex = 11;
            this.label_Properties.Text = "Properties";
            // 
            // propertyValueTextBox
            // 
            this.propertyValueTextBox.Location = new System.Drawing.Point(267, 19);
            this.propertyValueTextBox.Multiline = true;
            this.propertyValueTextBox.Name = "propertyValueTextBox";
            this.propertyValueTextBox.Size = new System.Drawing.Size(357, 69);
            this.propertyValueTextBox.TabIndex = 10;
            // 
            // commandsFlowLayoutPanel
            // 
            this.commandsFlowLayoutPanel.AutoScroll = true;
            this.commandsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.commandsFlowLayoutPanel.Name = "commandsFlowLayoutPanel";
            this.commandsFlowLayoutPanel.Size = new System.Drawing.Size(687, 170);
            this.commandsFlowLayoutPanel.TabIndex = 45;
            // 
            // ClipboardTab
            // 
            this.ClipboardTab.Controls.Add(this.SC_ClipBoardViewerMain);
            this.ClipboardTab.Location = new System.Drawing.Point(4, 22);
            this.ClipboardTab.Name = "ClipboardTab";
            this.ClipboardTab.Padding = new System.Windows.Forms.Padding(3);
            this.ClipboardTab.Size = new System.Drawing.Size(693, 423);
            this.ClipboardTab.TabIndex = 1;
            this.ClipboardTab.Text = "Clipboard++";
            this.ClipboardTab.UseVisualStyleBackColor = true;
            // 
            // SC_ClipBoardViewerMain
            // 
            this.SC_ClipBoardViewerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_ClipBoardViewerMain.Location = new System.Drawing.Point(3, 3);
            this.SC_ClipBoardViewerMain.Name = "SC_ClipBoardViewerMain";
            this.SC_ClipBoardViewerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SC_ClipBoardViewerMain.Panel1
            // 
            this.SC_ClipBoardViewerMain.Panel1.Controls.Add(this.ExportClipBoardButton);
            this.SC_ClipBoardViewerMain.Panel1.Controls.Add(this.PasteFromClipBoardButton);
            this.SC_ClipBoardViewerMain.Panel1.Controls.Add(this.ClearClipBoardButton);
            this.SC_ClipBoardViewerMain.Panel1.Controls.Add(this.CopyToClipBoardButton);
            this.SC_ClipBoardViewerMain.Panel1.Controls.Add(this.EnableClipboardCaptureButton);
            // 
            // SC_ClipBoardViewerMain.Panel2
            // 
            this.SC_ClipBoardViewerMain.Panel2.Controls.Add(this.SC_CipboardViewSplitter);
            this.SC_ClipBoardViewerMain.Size = new System.Drawing.Size(687, 417);
            this.SC_ClipBoardViewerMain.SplitterDistance = 34;
            this.SC_ClipBoardViewerMain.TabIndex = 2;
            // 
            // ExportClipBoardButton
            // 
            this.ExportClipBoardButton.Location = new System.Drawing.Point(407, 3);
            this.ExportClipBoardButton.Name = "ExportClipBoardButton";
            this.ExportClipBoardButton.Size = new System.Drawing.Size(71, 23);
            this.ExportClipBoardButton.TabIndex = 12;
            this.ExportClipBoardButton.Text = "Export";
            this.ExportClipBoardButton.UseVisualStyleBackColor = true;
            this.ExportClipBoardButton.Click += new System.EventHandler(this.ExportClipBoardButtonClick);
            // 
            // PasteFromClipBoardButton
            // 
            this.PasteFromClipBoardButton.Location = new System.Drawing.Point(253, 3);
            this.PasteFromClipBoardButton.Name = "PasteFromClipBoardButton";
            this.PasteFromClipBoardButton.Size = new System.Drawing.Size(71, 23);
            this.PasteFromClipBoardButton.TabIndex = 11;
            this.PasteFromClipBoardButton.Text = "Paste";
            this.PasteFromClipBoardButton.UseVisualStyleBackColor = true;
            this.PasteFromClipBoardButton.Click += new System.EventHandler(this.PasteFromClipBoardButtonClick);
            // 
            // ClearClipBoardButton
            // 
            this.ClearClipBoardButton.Location = new System.Drawing.Point(330, 3);
            this.ClearClipBoardButton.Name = "ClearClipBoardButton";
            this.ClearClipBoardButton.Size = new System.Drawing.Size(71, 23);
            this.ClearClipBoardButton.TabIndex = 10;
            this.ClearClipBoardButton.Text = "Clear";
            this.ClearClipBoardButton.UseVisualStyleBackColor = true;
            this.ClearClipBoardButton.Click += new System.EventHandler(this.ClearClipBoardButtonClick);
            // 
            // CopyToClipBoardButton
            // 
            this.CopyToClipBoardButton.Location = new System.Drawing.Point(176, 3);
            this.CopyToClipBoardButton.Name = "CopyToClipBoardButton";
            this.CopyToClipBoardButton.Size = new System.Drawing.Size(71, 23);
            this.CopyToClipBoardButton.TabIndex = 9;
            this.CopyToClipBoardButton.Text = "Copy";
            this.CopyToClipBoardButton.UseVisualStyleBackColor = true;
            this.CopyToClipBoardButton.Click += new System.EventHandler(this.CopyToClipBoardButtonClick);
            // 
            // EnableClipboardCaptureButton
            // 
            this.EnableClipboardCaptureButton.Location = new System.Drawing.Point(5, 3);
            this.EnableClipboardCaptureButton.Name = "EnableClipboardCaptureButton";
            this.EnableClipboardCaptureButton.Size = new System.Drawing.Size(165, 23);
            this.EnableClipboardCaptureButton.TabIndex = 8;
            this.EnableClipboardCaptureButton.Text = "Enable ClipBoard Capture";
            this.EnableClipboardCaptureButton.UseVisualStyleBackColor = true;
            this.EnableClipboardCaptureButton.Click += new System.EventHandler(this.EnableClipboardCaptureButtonClick);
            // 
            // SC_CipboardViewSplitter
            // 
            this.SC_CipboardViewSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_CipboardViewSplitter.Location = new System.Drawing.Point(0, 0);
            this.SC_CipboardViewSplitter.Name = "SC_CipboardViewSplitter";
            // 
            // SC_CipboardViewSplitter.Panel1
            // 
            this.SC_CipboardViewSplitter.Panel1.Controls.Add(this.ClipboardListBox);
            // 
            // SC_CipboardViewSplitter.Panel2
            // 
            this.SC_CipboardViewSplitter.Panel2.Controls.Add(this.ClipboardViewerTabControl);
            this.SC_CipboardViewSplitter.Size = new System.Drawing.Size(687, 379);
            this.SC_CipboardViewSplitter.SplitterDistance = 229;
            this.SC_CipboardViewSplitter.TabIndex = 1;
            // 
            // ClipboardListBox
            // 
            this.ClipboardListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipboardListBox.FormattingEnabled = true;
            this.ClipboardListBox.HorizontalScrollbar = true;
            this.ClipboardListBox.Location = new System.Drawing.Point(0, 0);
            this.ClipboardListBox.Name = "ClipboardListBox";
            this.ClipboardListBox.Size = new System.Drawing.Size(229, 379);
            this.ClipboardListBox.TabIndex = 0;
            this.ClipboardListBox.SelectedIndexChanged += new System.EventHandler(this.ClipboardListBoxSelectedIndexChanged);
            this.ClipboardListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClipboardListBoxKeyDown);
            this.ClipboardListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClipboardListBoxMouseDoubleClick);
            // 
            // ClipboardViewerTabControl
            // 
            this.ClipboardViewerTabControl.Controls.Add(this.CBV_TextTab);
            this.ClipboardViewerTabControl.Controls.Add(this.CBV_ImageTab);
            this.ClipboardViewerTabControl.Controls.Add(this.CBV_DataTab);
            this.ClipboardViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipboardViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.ClipboardViewerTabControl.Name = "ClipboardViewerTabControl";
            this.ClipboardViewerTabControl.SelectedIndex = 0;
            this.ClipboardViewerTabControl.Size = new System.Drawing.Size(454, 379);
            this.ClipboardViewerTabControl.TabIndex = 0;
            // 
            // CBV_TextTab
            // 
            this.CBV_TextTab.Controls.Add(this.ClipViewRTB);
            this.CBV_TextTab.Location = new System.Drawing.Point(4, 22);
            this.CBV_TextTab.Name = "CBV_TextTab";
            this.CBV_TextTab.Padding = new System.Windows.Forms.Padding(3);
            this.CBV_TextTab.Size = new System.Drawing.Size(446, 353);
            this.CBV_TextTab.TabIndex = 0;
            this.CBV_TextTab.Text = "Text";
            this.CBV_TextTab.UseVisualStyleBackColor = true;
            // 
            // ClipViewRTB
            // 
            this.ClipViewRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipViewRTB.Location = new System.Drawing.Point(3, 3);
            this.ClipViewRTB.Name = "ClipViewRTB";
            this.ClipViewRTB.ReadOnly = true;
            this.ClipViewRTB.Size = new System.Drawing.Size(440, 347);
            this.ClipViewRTB.TabIndex = 0;
            this.ClipViewRTB.Text = "";
            // 
            // CBV_ImageTab
            // 
            this.CBV_ImageTab.Controls.Add(this.ClipViewPictureBox);
            this.CBV_ImageTab.Location = new System.Drawing.Point(4, 22);
            this.CBV_ImageTab.Name = "CBV_ImageTab";
            this.CBV_ImageTab.Padding = new System.Windows.Forms.Padding(3);
            this.CBV_ImageTab.Size = new System.Drawing.Size(446, 353);
            this.CBV_ImageTab.TabIndex = 1;
            this.CBV_ImageTab.Text = "Image";
            this.CBV_ImageTab.UseVisualStyleBackColor = true;
            // 
            // ClipViewPictureBox
            // 
            this.ClipViewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClipViewPictureBox.Location = new System.Drawing.Point(3, 3);
            this.ClipViewPictureBox.Name = "ClipViewPictureBox";
            this.ClipViewPictureBox.Size = new System.Drawing.Size(440, 347);
            this.ClipViewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ClipViewPictureBox.TabIndex = 0;
            this.ClipViewPictureBox.TabStop = false;
            // 
            // CBV_DataTab
            // 
            this.CBV_DataTab.Location = new System.Drawing.Point(4, 22);
            this.CBV_DataTab.Name = "CBV_DataTab";
            this.CBV_DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.CBV_DataTab.Size = new System.Drawing.Size(446, 353);
            this.CBV_DataTab.TabIndex = 2;
            this.CBV_DataTab.Text = "Data";
            this.CBV_DataTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(741, 555);
            this.propertyGrid1.TabIndex = 0;
            // 
            // PickInvokeTimer
            // 
            this.PickInvokeTimer.Enabled = true;
            this.PickInvokeTimer.Tick += new System.EventHandler(this.PickInvokeTimerTick);
            // 
            // label_EnvType
            // 
            this.label_EnvType.Location = new System.Drawing.Point(39, 34);
            this.label_EnvType.Name = "label_EnvType";
            this.label_EnvType.Size = new System.Drawing.Size(39, 23);
            this.label_EnvType.TabIndex = 9;
            this.label_EnvType.Text = "Type:";
            // 
            // EnvironmentTypeTextBox
            // 
            this.EnvironmentTypeTextBox.Location = new System.Drawing.Point(84, 32);
            this.EnvironmentTypeTextBox.Name = "EnvironmentTypeTextBox";
            this.EnvironmentTypeTextBox.ReadOnly = true;
            this.EnvironmentTypeTextBox.Size = new System.Drawing.Size(282, 20);
            this.EnvironmentTypeTextBox.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 495);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StatusStrip_InvokerMain);
            this.Controls.Add(this.Invoker_MainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Invoker_MainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Invoker";
            this.ClipboardChangedToText += new System.EventHandler<Invoker.ClipboardChangedTextEventArgs>(this.MainFormClipboardChangedToText);
            this.ClipboardChangedToImage += new System.EventHandler<Invoker.ClipboardChangedImageEventArgs>(this.MainFormClipboardChangedToImage);
            this.ClipboardChangedToFileList += new System.EventHandler<Invoker.ClipboardChangedFileListEventArgs>(this.MainFormClipboardChangedToFileList);
            this.ClipboardChangedToAudioStream += new System.EventHandler<Invoker.ClipboardChangedAudioStreamEventArgs>(this.MainFormClipboardChangedToAudioStream);
            this.ClipboardChangedToData += new System.EventHandler<Invoker.ClipboardChangedDataEventArgs>(this.MainFormClipboardChangedToData);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.trayContextMenuStrip.ResumeLayout(false);
            this.Invoker_MainMenuStrip.ResumeLayout(false);
            this.Invoker_MainMenuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.TabControl_InvokerMain.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.SC_DashBoardMain.Panel1.ResumeLayout(false);
            this.SC_DashBoardMain.Panel1.PerformLayout();
            this.SC_DashBoardMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_DashBoardMain)).EndInit();
            this.SC_DashBoardMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.GroupBox_Tools.ResumeLayout(false);
            this.GroupBox_Tools.PerformLayout();
            this.ClipboardTab.ResumeLayout(false);
            this.SC_ClipBoardViewerMain.Panel1.ResumeLayout(false);
            this.SC_ClipBoardViewerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_ClipBoardViewerMain)).EndInit();
            this.SC_ClipBoardViewerMain.ResumeLayout(false);
            this.SC_CipboardViewSplitter.Panel1.ResumeLayout(false);
            this.SC_CipboardViewSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_CipboardViewSplitter)).EndInit();
            this.SC_CipboardViewSplitter.ResumeLayout(false);
            this.ClipboardViewerTabControl.ResumeLayout(false);
            this.CBV_TextTab.ResumeLayout(false);
            this.CBV_ImageTab.ResumeLayout(false);
            this.CBV_ImageTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClipViewPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Timer PickInvokeTimer;
        private System.Windows.Forms.TextBox EnvironmentTypeTextBox;
        private System.Windows.Forms.Label label_EnvType;
    }
}
