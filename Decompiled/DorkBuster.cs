using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows.Forms;
using Costura;
using DorkBuster.Forms;
using Guna.UI2.WinForms;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("DorkBuster")]
[assembly: AssemblyDescription("A Google dork + GitHub dork generator which is suitable for pentesting purposes and available on Windows.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Kap0ne Tech Solutions B.V.")]
[assembly: AssemblyProduct("DorkBuster")]
[assembly: AssemblyCopyright("Copyright © 2025 Kap0ne Tech Solutions B.V.")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("da5f7c6d-0710-43b1-b767-c93ab173ca58")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyVersion("1.0.0.0")]
internal class <Module>
{
	static <Module>()
	{
		AssemblyLoader.Attach(subscribe: true);
	}
}
namespace DorkBuster
{
	public class MainForm : Form
	{
		private string currentGoogleOutput = "";

		private string currentGitHubOutput = "";

		private IContainer components;

		private Guna2BorderlessForm BorderlessMainForm;

		private Label TitleLabel;

		private Guna2PictureBox Logo;

		private Guna2Button ExitButton;

		private Guna2Button MinimizeButton;

		private Guna2TabControl MainFormTabControl;

		private TabPage GoogleDorksTab;

		private TabPage GitHubDorksTab;

		private Guna2VSeparator GoogleDorksTabSeparator;

		private Guna2VSeparator GitHubDorksTabSeparator;

		private Guna2TextBox GoogleTargetTextBox;

		private Label GoogleTargetLabel;

		private Guna2Button GenerateGoogleDorksButton;

		private Guna2Button GoogleOutputCopyButton;

		private Guna2TextBox GoogleDorksOutputTextBox;

		private Label GoogleOutputLabel;

		private Guna2Button GoogleOutputSaveButton;

		private Guna2Button GenerateGitHubDorksButton;

		private Guna2TextBox GitHubTargetTextBox;

		private Label GitHubTargetLabel;

		private Guna2Button GitHubOutputSaveButton;

		private Label GitHubOutputLabel;

		private Guna2Button GitHubOutputCopyButton;

		private Guna2TextBox GitHubDorksOutputTextBox;

		private Label CreditsLabel;

		private Label label1;

		public MainForm()
		{
			InitializeComponent();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void MinimizeButton_Click(object sender, EventArgs e)
		{
			((Form)this).WindowState = (FormWindowState)1;
		}

		private void GenerateGoogleDorksButton_Click(object sender, EventArgs e)
		{
			GenerateDorks("GoogleDorks.txt", ((Control)GoogleTargetTextBox).Text, "Google", GoogleDorksOutputTextBox);
		}

		private void GenerateGitHubDorksButton_Click(object sender, EventArgs e)
		{
			GenerateDorks("GitHubDorks.txt", ((Control)GitHubTargetTextBox).Text, "GitHub", GitHubDorksOutputTextBox);
		}

		private void GenerateDorks(string resourceFile, string target, string dorkType, Guna2TextBox outputBox)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrWhiteSpace(target))
			{
				MessageBox.Show("Please enter a target domain/URL!", "Error:", (MessageBoxButtons)0, (MessageBoxIcon)16);
				return;
			}
			target = target.Replace("http://", "").Replace("https://", "").Trim();
			try
			{
				string text = (((Control)outputBox).Text = ReadEmbeddedResource(resourceFile).Replace("{target}", target));
				if (dorkType == "Google")
				{
					currentGoogleOutput = text;
				}
				else if (dorkType == "GitHub")
				{
					currentGitHubOutput = text;
				}
				MessageBox.Show(dorkType + " dorks generated successfully!", "Success:", (MessageBoxButtons)0, (MessageBoxIcon)64);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message ?? "", "Error:", (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}

		private string ReadEmbeddedResource(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetName().Name + ".DorkTemplates." + resourceName;
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			if (stream == null)
			{
				throw new FileNotFoundException("Resource '" + text + "' not found. Available: " + string.Join(", ", executingAssembly.GetManifestResourceNames()));
			}
			using StreamReader streamReader = new StreamReader(stream);
			return streamReader.ReadToEnd();
		}

		private void GoogleOutputCopyButton_Click(object sender, EventArgs e)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			if (!string.IsNullOrEmpty(currentGoogleOutput))
			{
				Clipboard.SetText(currentGoogleOutput);
				MessageBox.Show("Google dorks successfully copied to clipboard!", "Success:", (MessageBoxButtons)0, (MessageBoxIcon)64);
			}
			else
			{
				MessageBox.Show("Generate dorks first before trying to copy!", "Error:", (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}

		private void GoogleOutputSaveButton_Click(object sender, EventArgs e)
		{
			SaveToFile(currentGoogleOutput, "Google");
		}

		private void GitHubOutputCopyButton_Click(object sender, EventArgs e)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			if (!string.IsNullOrEmpty(currentGitHubOutput))
			{
				Clipboard.SetText(currentGitHubOutput);
				MessageBox.Show("GitHub dorks successfully copied to clipboard!", "Success:", (MessageBoxButtons)0, (MessageBoxIcon)64);
			}
			else
			{
				MessageBox.Show("Generate dorks first before trying to copy!", "Error:", (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}

		private void GitHubOutputSaveButton_Click(object sender, EventArgs e)
		{
			SaveToFile(currentGitHubOutput, "GitHub");
		}

		private void SaveToFile(string content, string dorkType)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Invalid comparison between Unknown and I4
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(content))
			{
				MessageBox.Show("Generate dorks first before trying to save!", "Error:", (MessageBoxButtons)0, (MessageBoxIcon)16);
				return;
			}
			SaveFileDialog val = new SaveFileDialog();
			try
			{
				((FileDialog)val).Filter = "Text Files|*.txt";
				((FileDialog)val).FileName = "Generated " + dorkType + " Dorks.txt";
				if ((int)((CommonDialog)val).ShowDialog() == 1)
				{
					File.WriteAllText(((FileDialog)val).FileName, content);
					MessageBox.Show(dorkType + " dorks saved successfully!", "Success:", (MessageBoxButtons)0, (MessageBoxIcon)64);
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((Form)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Expected O, but got Unknown
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Expected O, but got Unknown
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Expected O, but got Unknown
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Expected O, but got Unknown
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Expected O, but got Unknown
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Expected O, but got Unknown
			//IL_029d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Expected O, but got Unknown
			//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d7: Expected O, but got Unknown
			//IL_0459: Unknown result type (might be due to invalid IL or missing references)
			//IL_0463: Expected O, but got Unknown
			//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Expected O, but got Unknown
			//IL_0651: Unknown result type (might be due to invalid IL or missing references)
			//IL_065b: Expected O, but got Unknown
			//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ee: Expected O, but got Unknown
			//IL_0875: Unknown result type (might be due to invalid IL or missing references)
			//IL_087f: Expected O, but got Unknown
			//IL_0900: Unknown result type (might be due to invalid IL or missing references)
			//IL_090a: Expected O, but got Unknown
			//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7f: Expected O, but got Unknown
			//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0b: Expected O, but got Unknown
			//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce2: Expected O, but got Unknown
			//IL_0df3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfd: Expected O, but got Unknown
			//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e89: Expected O, but got Unknown
			//IL_108c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1096: Expected O, but got Unknown
			//IL_1295: Unknown result type (might be due to invalid IL or missing references)
			//IL_129f: Expected O, but got Unknown
			//IL_1321: Unknown result type (might be due to invalid IL or missing references)
			//IL_132b: Expected O, but got Unknown
			//IL_152e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1538: Expected O, but got Unknown
			//IL_167e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1688: Expected O, but got Unknown
			//IL_182c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1926: Unknown result type (might be due to invalid IL or missing references)
			//IL_1930: Expected O, but got Unknown
			//IL_19b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_19bc: Expected O, but got Unknown
			//IL_1a8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a94: Expected O, but got Unknown
			//IL_1ba6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bb0: Expected O, but got Unknown
			//IL_1c32: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c3c: Expected O, but got Unknown
			//IL_1e4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e59: Expected O, but got Unknown
			//IL_2059: Unknown result type (might be due to invalid IL or missing references)
			//IL_2063: Expected O, but got Unknown
			//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_20ef: Expected O, but got Unknown
			//IL_22f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_22fc: Expected O, but got Unknown
			//IL_2442: Unknown result type (might be due to invalid IL or missing references)
			//IL_244c: Expected O, but got Unknown
			//IL_251c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2526: Expected O, but got Unknown
			//IL_25ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_25b6: Expected O, but got Unknown
			//IL_26e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_26ef: Expected O, but got Unknown
			//IL_271c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2726: Expected O, but got Unknown
			//IL_2728: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			BorderlessMainForm = new Guna2BorderlessForm(components);
			TitleLabel = new Label();
			Logo = new Guna2PictureBox();
			ExitButton = new Guna2Button();
			MinimizeButton = new Guna2Button();
			MainFormTabControl = new Guna2TabControl();
			GoogleDorksTab = new TabPage();
			GoogleOutputSaveButton = new Guna2Button();
			GoogleOutputLabel = new Label();
			GoogleOutputCopyButton = new Guna2Button();
			GoogleDorksOutputTextBox = new Guna2TextBox();
			GenerateGoogleDorksButton = new Guna2Button();
			GoogleTargetTextBox = new Guna2TextBox();
			GoogleTargetLabel = new Label();
			GoogleDorksTabSeparator = new Guna2VSeparator();
			GitHubDorksTab = new TabPage();
			GitHubOutputSaveButton = new Guna2Button();
			GitHubOutputLabel = new Label();
			GitHubOutputCopyButton = new Guna2Button();
			GitHubDorksOutputTextBox = new Guna2TextBox();
			GenerateGitHubDorksButton = new Guna2Button();
			GitHubTargetTextBox = new Guna2TextBox();
			GitHubTargetLabel = new Label();
			GitHubDorksTabSeparator = new Guna2VSeparator();
			CreditsLabel = new Label();
			label1 = new Label();
			((ISupportInitialize)Logo).BeginInit();
			((Control)MainFormTabControl).SuspendLayout();
			((Control)GoogleDorksTab).SuspendLayout();
			((Control)GitHubDorksTab).SuspendLayout();
			((Control)this).SuspendLayout();
			BorderlessMainForm.AnimateWindow = true;
			BorderlessMainForm.AnimationInterval = 200;
			BorderlessMainForm.AnimationType = (AnimateWindowType)16;
			BorderlessMainForm.BorderRadius = 20;
			BorderlessMainForm.ContainerControl = (ContainerControl)(object)this;
			BorderlessMainForm.DockIndicatorTransparencyValue = 0.6;
			BorderlessMainForm.DragStartTransparencyValue = 1.0;
			BorderlessMainForm.ResizeForm = false;
			BorderlessMainForm.ShadowColor = Color.FromArgb(147, 76, 245);
			BorderlessMainForm.TransparentWhileDrag = true;
			((Control)TitleLabel).AutoSize = true;
			((Control)TitleLabel).Font = new Font("Segoe UI Semilight", 15f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)TitleLabel).Location = new Point(63, 20);
			((Control)TitleLabel).Name = "TitleLabel";
			((Control)TitleLabel).Size = new Size(106, 28);
			((Control)TitleLabel).TabIndex = 0;
			((Control)TitleLabel).Text = "DorkBuster";
			((PictureBox)Logo).Image = (Image)componentResourceManager.GetObject("Logo.Image");
			Logo.ImageRotate = 0f;
			((Control)Logo).Location = new Point(12, 12);
			((Control)Logo).Name = "Logo";
			((Control)Logo).Size = new Size(45, 42);
			((PictureBox)Logo).SizeMode = (PictureBoxSizeMode)4;
			((PictureBox)Logo).TabIndex = 1;
			((PictureBox)Logo).TabStop = false;
			ExitButton.Animated = true;
			ExitButton.BorderColor = Color.Transparent;
			ExitButton.BorderRadius = 5;
			ExitButton.DisabledState.BorderColor = Color.Transparent;
			ExitButton.DisabledState.CustomBorderColor = Color.Transparent;
			ExitButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			ExitButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			ExitButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)ExitButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)ExitButton).ForeColor = Color.FromArgb(224, 224, 224);
			ExitButton.HoverState.BorderColor = Color.Transparent;
			ExitButton.HoverState.CustomBorderColor = Color.Transparent;
			ExitButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			ExitButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			ExitButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)ExitButton).Location = new Point(613, 12);
			((Control)ExitButton).Name = "ExitButton";
			ExitButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)ExitButton).Size = new Size(45, 42);
			((Control)ExitButton).TabIndex = 2;
			((Control)ExitButton).Text = "X";
			((Control)ExitButton).Click += ExitButton_Click;
			MinimizeButton.Animated = true;
			MinimizeButton.BorderColor = Color.Transparent;
			MinimizeButton.BorderRadius = 5;
			MinimizeButton.DisabledState.BorderColor = Color.Transparent;
			MinimizeButton.DisabledState.CustomBorderColor = Color.Transparent;
			MinimizeButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			MinimizeButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			MinimizeButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)MinimizeButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)MinimizeButton).ForeColor = Color.FromArgb(224, 224, 224);
			MinimizeButton.HoverState.BorderColor = Color.Transparent;
			MinimizeButton.HoverState.CustomBorderColor = Color.Transparent;
			MinimizeButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			MinimizeButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			MinimizeButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)MinimizeButton).Location = new Point(562, 12);
			((Control)MinimizeButton).Name = "MinimizeButton";
			MinimizeButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)MinimizeButton).Size = new Size(45, 42);
			((Control)MinimizeButton).TabIndex = 3;
			((Control)MinimizeButton).Text = "-";
			((Control)MinimizeButton).Click += MinimizeButton_Click;
			MainFormTabControl.Alignment = (TabAlignment)2;
			((Control)MainFormTabControl).Controls.Add((Control)(object)GoogleDorksTab);
			((Control)MainFormTabControl).Controls.Add((Control)(object)GitHubDorksTab);
			MainFormTabControl.ItemSize = new Size(180, 40);
			((Control)MainFormTabControl).Location = new Point(12, 79);
			((Control)MainFormTabControl).Name = "MainFormTabControl";
			((TabControl)MainFormTabControl).SelectedIndex = 0;
			((Control)MainFormTabControl).Size = new Size(646, 341);
			MainFormTabControl.TabButtonHoverState.BorderColor = Color.Empty;
			MainFormTabControl.TabButtonHoverState.FillColor = Color.FromArgb(20, 20, 20);
			MainFormTabControl.TabButtonHoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
			MainFormTabControl.TabButtonHoverState.ForeColor = Color.FromArgb(224, 224, 224);
			MainFormTabControl.TabButtonHoverState.InnerColor = Color.FromArgb(20, 20, 20);
			MainFormTabControl.TabButtonIdleState.BorderColor = Color.Empty;
			MainFormTabControl.TabButtonIdleState.FillColor = Color.FromArgb(20, 20, 20);
			MainFormTabControl.TabButtonIdleState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			MainFormTabControl.TabButtonIdleState.ForeColor = Color.FromArgb(224, 224, 224);
			MainFormTabControl.TabButtonIdleState.InnerColor = Color.Transparent;
			MainFormTabControl.TabButtonSelectedState.BorderColor = Color.Empty;
			MainFormTabControl.TabButtonSelectedState.FillColor = Color.FromArgb(20, 20, 20);
			MainFormTabControl.TabButtonSelectedState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
			MainFormTabControl.TabButtonSelectedState.ForeColor = Color.FromArgb(224, 224, 224);
			MainFormTabControl.TabButtonSelectedState.InnerColor = Color.FromArgb(147, 76, 245);
			MainFormTabControl.TabButtonSize = new Size(180, 40);
			((Control)MainFormTabControl).TabIndex = 4;
			MainFormTabControl.TabMenuBackColor = Color.FromArgb(20, 20, 20);
			((Control)GoogleDorksTab).BackColor = Color.FromArgb(20, 20, 20);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleOutputSaveButton);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleOutputLabel);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleOutputCopyButton);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleDorksOutputTextBox);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GenerateGoogleDorksButton);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleTargetTextBox);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleTargetLabel);
			((Control)GoogleDorksTab).Controls.Add((Control)(object)GoogleDorksTabSeparator);
			GoogleDorksTab.Location = new Point(184, 4);
			((Control)GoogleDorksTab).Name = "GoogleDorksTab";
			((Control)GoogleDorksTab).Padding = new Padding(3);
			((Control)GoogleDorksTab).Size = new Size(458, 333);
			GoogleDorksTab.TabIndex = 0;
			((Control)GoogleDorksTab).Text = "Google Dorks";
			GoogleOutputSaveButton.Animated = true;
			GoogleOutputSaveButton.BorderColor = Color.Transparent;
			GoogleOutputSaveButton.BorderRadius = 5;
			GoogleOutputSaveButton.DisabledState.BorderColor = Color.Transparent;
			GoogleOutputSaveButton.DisabledState.CustomBorderColor = Color.Transparent;
			GoogleOutputSaveButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GoogleOutputSaveButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleOutputSaveButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GoogleOutputSaveButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GoogleOutputSaveButton).ForeColor = Color.FromArgb(224, 224, 224);
			GoogleOutputSaveButton.HoverState.BorderColor = Color.Transparent;
			GoogleOutputSaveButton.HoverState.CustomBorderColor = Color.Transparent;
			GoogleOutputSaveButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GoogleOutputSaveButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GoogleOutputSaveButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GoogleOutputSaveButton).Location = new Point(235, 290);
			((Control)GoogleOutputSaveButton).Name = "GoogleOutputSaveButton";
			GoogleOutputSaveButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GoogleOutputSaveButton).Size = new Size(206, 37);
			((Control)GoogleOutputSaveButton).TabIndex = 8;
			((Control)GoogleOutputSaveButton).Text = "Save To File";
			((Control)GoogleOutputSaveButton).Click += GoogleOutputSaveButton_Click;
			((Control)GoogleOutputLabel).AutoSize = true;
			((Control)GoogleOutputLabel).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GoogleOutputLabel).Location = new Point(16, 128);
			((Control)GoogleOutputLabel).Name = "GoogleOutputLabel";
			((Control)GoogleOutputLabel).Size = new Size(55, 19);
			((Control)GoogleOutputLabel).TabIndex = 7;
			((Control)GoogleOutputLabel).Text = "Output:";
			GoogleOutputCopyButton.Animated = true;
			GoogleOutputCopyButton.BorderColor = Color.Transparent;
			GoogleOutputCopyButton.BorderRadius = 5;
			GoogleOutputCopyButton.DisabledState.BorderColor = Color.Transparent;
			GoogleOutputCopyButton.DisabledState.CustomBorderColor = Color.Transparent;
			GoogleOutputCopyButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GoogleOutputCopyButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleOutputCopyButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GoogleOutputCopyButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GoogleOutputCopyButton).ForeColor = Color.FromArgb(224, 224, 224);
			GoogleOutputCopyButton.HoverState.BorderColor = Color.Transparent;
			GoogleOutputCopyButton.HoverState.CustomBorderColor = Color.Transparent;
			GoogleOutputCopyButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GoogleOutputCopyButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GoogleOutputCopyButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GoogleOutputCopyButton).Location = new Point(22, 290);
			((Control)GoogleOutputCopyButton).Name = "GoogleOutputCopyButton";
			GoogleOutputCopyButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GoogleOutputCopyButton).Size = new Size(206, 37);
			((Control)GoogleOutputCopyButton).TabIndex = 6;
			((Control)GoogleOutputCopyButton).Text = "Copy";
			((Control)GoogleOutputCopyButton).Click += GoogleOutputCopyButton_Click;
			GoogleDorksOutputTextBox.Animated = true;
			GoogleDorksOutputTextBox.BorderColor = Color.FromArgb(40, 40, 40);
			GoogleDorksOutputTextBox.BorderRadius = 5;
			((Control)GoogleDorksOutputTextBox).Cursor = Cursors.IBeam;
			GoogleDorksOutputTextBox.DefaultText = "";
			GoogleDorksOutputTextBox.DisabledState.BorderColor = Color.FromArgb(40, 40, 40);
			GoogleDorksOutputTextBox.DisabledState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleDorksOutputTextBox.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleDorksOutputTextBox.DisabledState.PlaceholderForeColor = Color.DimGray;
			GoogleDorksOutputTextBox.FillColor = Color.FromArgb(20, 20, 20);
			GoogleDorksOutputTextBox.FocusedState.BorderColor = Color.FromArgb(147, 76, 245);
			GoogleDorksOutputTextBox.FocusedState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleDorksOutputTextBox.FocusedState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleDorksOutputTextBox.FocusedState.PlaceholderForeColor = Color.DimGray;
			((Control)GoogleDorksOutputTextBox).Font = new Font("Segoe UI Semilight", 10f);
			((Control)GoogleDorksOutputTextBox).ForeColor = Color.FromArgb(224, 224, 224);
			GoogleDorksOutputTextBox.HoverState.BorderColor = Color.FromArgb(147, 76, 245);
			GoogleDorksOutputTextBox.HoverState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleDorksOutputTextBox.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleDorksOutputTextBox.HoverState.PlaceholderForeColor = Color.DimGray;
			((Control)GoogleDorksOutputTextBox).Location = new Point(16, 152);
			GoogleDorksOutputTextBox.MaxLength = 1000000000;
			GoogleDorksOutputTextBox.Multiline = true;
			((Control)GoogleDorksOutputTextBox).Name = "GoogleDorksOutputTextBox";
			GoogleDorksOutputTextBox.PlaceholderForeColor = Color.DimGray;
			GoogleDorksOutputTextBox.PlaceholderText = "";
			GoogleDorksOutputTextBox.ReadOnly = true;
			GoogleDorksOutputTextBox.SelectedText = "";
			((Control)GoogleDorksOutputTextBox).Size = new Size(432, 132);
			((Control)GoogleDorksOutputTextBox).TabIndex = 5;
			GoogleDorksOutputTextBox.WordWrap = false;
			GenerateGoogleDorksButton.Animated = true;
			GenerateGoogleDorksButton.BorderColor = Color.Transparent;
			GenerateGoogleDorksButton.BorderRadius = 5;
			GenerateGoogleDorksButton.DisabledState.BorderColor = Color.Transparent;
			GenerateGoogleDorksButton.DisabledState.CustomBorderColor = Color.Transparent;
			GenerateGoogleDorksButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GenerateGoogleDorksButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GenerateGoogleDorksButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GenerateGoogleDorksButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GenerateGoogleDorksButton).ForeColor = Color.FromArgb(224, 224, 224);
			GenerateGoogleDorksButton.HoverState.BorderColor = Color.Transparent;
			GenerateGoogleDorksButton.HoverState.CustomBorderColor = Color.Transparent;
			GenerateGoogleDorksButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GenerateGoogleDorksButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GenerateGoogleDorksButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GenerateGoogleDorksButton).Location = new Point(16, 71);
			((Control)GenerateGoogleDorksButton).Name = "GenerateGoogleDorksButton";
			GenerateGoogleDorksButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GenerateGoogleDorksButton).Size = new Size(432, 37);
			((Control)GenerateGoogleDorksButton).TabIndex = 4;
			((Control)GenerateGoogleDorksButton).Text = "Generate Google Dorks";
			((Control)GenerateGoogleDorksButton).Click += GenerateGoogleDorksButton_Click;
			GoogleTargetTextBox.Animated = true;
			GoogleTargetTextBox.BorderColor = Color.FromArgb(40, 40, 40);
			GoogleTargetTextBox.BorderRadius = 5;
			((Control)GoogleTargetTextBox).Cursor = Cursors.IBeam;
			GoogleTargetTextBox.DefaultText = "";
			GoogleTargetTextBox.DisabledState.BorderColor = Color.FromArgb(40, 40, 40);
			GoogleTargetTextBox.DisabledState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleTargetTextBox.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleTargetTextBox.DisabledState.PlaceholderForeColor = Color.DimGray;
			GoogleTargetTextBox.FillColor = Color.FromArgb(20, 20, 20);
			GoogleTargetTextBox.FocusedState.BorderColor = Color.FromArgb(147, 76, 245);
			GoogleTargetTextBox.FocusedState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleTargetTextBox.FocusedState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleTargetTextBox.FocusedState.PlaceholderForeColor = Color.DimGray;
			((Control)GoogleTargetTextBox).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GoogleTargetTextBox).ForeColor = Color.FromArgb(224, 224, 224);
			GoogleTargetTextBox.HoverState.BorderColor = Color.FromArgb(147, 76, 245);
			GoogleTargetTextBox.HoverState.FillColor = Color.FromArgb(20, 20, 20);
			GoogleTargetTextBox.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			GoogleTargetTextBox.HoverState.PlaceholderForeColor = Color.DimGray;
			((Control)GoogleTargetTextBox).Location = new Point(16, 29);
			GoogleTargetTextBox.MaxLength = 1000;
			((Control)GoogleTargetTextBox).Name = "GoogleTargetTextBox";
			GoogleTargetTextBox.PlaceholderForeColor = Color.DimGray;
			GoogleTargetTextBox.PlaceholderText = "Example: example.com";
			GoogleTargetTextBox.SelectedText = "";
			((Control)GoogleTargetTextBox).Size = new Size(432, 37);
			((Control)GoogleTargetTextBox).TabIndex = 2;
			GoogleTargetTextBox.WordWrap = false;
			((Control)GoogleTargetLabel).AutoSize = true;
			((Control)GoogleTargetLabel).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GoogleTargetLabel).Location = new Point(16, 5);
			((Control)GoogleTargetLabel).Name = "GoogleTargetLabel";
			((Control)GoogleTargetLabel).Size = new Size(130, 19);
			((Control)GoogleTargetLabel).TabIndex = 1;
			((Control)GoogleTargetLabel).Text = "Target Domain/URL:";
			((Guna2Separator)GoogleDorksTabSeparator).FillColor = Color.FromArgb(40, 40, 40);
			((Control)GoogleDorksTabSeparator).Location = new Point(0, 0);
			((Control)GoogleDorksTabSeparator).Name = "GoogleDorksTabSeparator";
			((Control)GoogleDorksTabSeparator).Size = new Size(10, 333);
			((Control)GoogleDorksTabSeparator).TabIndex = 0;
			((Control)GitHubDorksTab).BackColor = Color.FromArgb(20, 20, 20);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubOutputSaveButton);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubOutputLabel);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubOutputCopyButton);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubDorksOutputTextBox);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GenerateGitHubDorksButton);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubTargetTextBox);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubTargetLabel);
			((Control)GitHubDorksTab).Controls.Add((Control)(object)GitHubDorksTabSeparator);
			GitHubDorksTab.Location = new Point(184, 4);
			((Control)GitHubDorksTab).Name = "GitHubDorksTab";
			((Control)GitHubDorksTab).Padding = new Padding(3);
			((Control)GitHubDorksTab).Size = new Size(458, 333);
			GitHubDorksTab.TabIndex = 1;
			((Control)GitHubDorksTab).Text = "GitHub Dorks";
			GitHubOutputSaveButton.Animated = true;
			GitHubOutputSaveButton.BorderColor = Color.Transparent;
			GitHubOutputSaveButton.BorderRadius = 5;
			GitHubOutputSaveButton.DisabledState.BorderColor = Color.Transparent;
			GitHubOutputSaveButton.DisabledState.CustomBorderColor = Color.Transparent;
			GitHubOutputSaveButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GitHubOutputSaveButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubOutputSaveButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GitHubOutputSaveButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubOutputSaveButton).ForeColor = Color.FromArgb(224, 224, 224);
			GitHubOutputSaveButton.HoverState.BorderColor = Color.Transparent;
			GitHubOutputSaveButton.HoverState.CustomBorderColor = Color.Transparent;
			GitHubOutputSaveButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GitHubOutputSaveButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GitHubOutputSaveButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GitHubOutputSaveButton).Location = new Point(235, 290);
			((Control)GitHubOutputSaveButton).Name = "GitHubOutputSaveButton";
			GitHubOutputSaveButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GitHubOutputSaveButton).Size = new Size(206, 37);
			((Control)GitHubOutputSaveButton).TabIndex = 12;
			((Control)GitHubOutputSaveButton).Text = "Save To File";
			((Control)GitHubOutputSaveButton).Click += GitHubOutputSaveButton_Click;
			((Control)GitHubOutputLabel).AutoSize = true;
			((Control)GitHubOutputLabel).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubOutputLabel).Location = new Point(16, 128);
			((Control)GitHubOutputLabel).Name = "GitHubOutputLabel";
			((Control)GitHubOutputLabel).Size = new Size(55, 19);
			((Control)GitHubOutputLabel).TabIndex = 11;
			((Control)GitHubOutputLabel).Text = "Output:";
			GitHubOutputCopyButton.Animated = true;
			GitHubOutputCopyButton.BorderColor = Color.Transparent;
			GitHubOutputCopyButton.BorderRadius = 5;
			GitHubOutputCopyButton.DisabledState.BorderColor = Color.Transparent;
			GitHubOutputCopyButton.DisabledState.CustomBorderColor = Color.Transparent;
			GitHubOutputCopyButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GitHubOutputCopyButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubOutputCopyButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GitHubOutputCopyButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubOutputCopyButton).ForeColor = Color.FromArgb(224, 224, 224);
			GitHubOutputCopyButton.HoverState.BorderColor = Color.Transparent;
			GitHubOutputCopyButton.HoverState.CustomBorderColor = Color.Transparent;
			GitHubOutputCopyButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GitHubOutputCopyButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GitHubOutputCopyButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GitHubOutputCopyButton).Location = new Point(22, 290);
			((Control)GitHubOutputCopyButton).Name = "GitHubOutputCopyButton";
			GitHubOutputCopyButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GitHubOutputCopyButton).Size = new Size(206, 37);
			((Control)GitHubOutputCopyButton).TabIndex = 10;
			((Control)GitHubOutputCopyButton).Text = "Copy";
			((Control)GitHubOutputCopyButton).Click += GitHubOutputCopyButton_Click;
			GitHubDorksOutputTextBox.Animated = true;
			((ScrollableControl)GitHubDorksOutputTextBox).AutoScroll = true;
			GitHubDorksOutputTextBox.BorderColor = Color.FromArgb(40, 40, 40);
			GitHubDorksOutputTextBox.BorderRadius = 5;
			((Control)GitHubDorksOutputTextBox).Cursor = Cursors.IBeam;
			GitHubDorksOutputTextBox.DefaultText = "";
			GitHubDorksOutputTextBox.DisabledState.BorderColor = Color.FromArgb(40, 40, 40);
			GitHubDorksOutputTextBox.DisabledState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubDorksOutputTextBox.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubDorksOutputTextBox.DisabledState.PlaceholderForeColor = Color.DimGray;
			GitHubDorksOutputTextBox.FillColor = Color.FromArgb(20, 20, 20);
			GitHubDorksOutputTextBox.FocusedState.BorderColor = Color.FromArgb(147, 76, 245);
			GitHubDorksOutputTextBox.FocusedState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubDorksOutputTextBox.FocusedState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubDorksOutputTextBox.FocusedState.PlaceholderForeColor = Color.DimGray;
			((Control)GitHubDorksOutputTextBox).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubDorksOutputTextBox).ForeColor = Color.FromArgb(224, 224, 224);
			GitHubDorksOutputTextBox.HoverState.BorderColor = Color.FromArgb(147, 76, 245);
			GitHubDorksOutputTextBox.HoverState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubDorksOutputTextBox.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubDorksOutputTextBox.HoverState.PlaceholderForeColor = Color.DimGray;
			((Control)GitHubDorksOutputTextBox).Location = new Point(16, 152);
			GitHubDorksOutputTextBox.MaxLength = 1000000000;
			GitHubDorksOutputTextBox.Multiline = true;
			((Control)GitHubDorksOutputTextBox).Name = "GitHubDorksOutputTextBox";
			GitHubDorksOutputTextBox.PlaceholderForeColor = Color.DimGray;
			GitHubDorksOutputTextBox.PlaceholderText = "";
			GitHubDorksOutputTextBox.ReadOnly = true;
			GitHubDorksOutputTextBox.SelectedText = "";
			((Control)GitHubDorksOutputTextBox).Size = new Size(432, 132);
			((Control)GitHubDorksOutputTextBox).TabIndex = 9;
			GitHubDorksOutputTextBox.WordWrap = false;
			GenerateGitHubDorksButton.Animated = true;
			GenerateGitHubDorksButton.BorderColor = Color.Transparent;
			GenerateGitHubDorksButton.BorderRadius = 5;
			GenerateGitHubDorksButton.DisabledState.BorderColor = Color.Transparent;
			GenerateGitHubDorksButton.DisabledState.CustomBorderColor = Color.Transparent;
			GenerateGitHubDorksButton.DisabledState.FillColor = Color.FromArgb(40, 40, 40);
			GenerateGitHubDorksButton.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GenerateGitHubDorksButton.FillColor = Color.FromArgb(40, 40, 40);
			((Control)GenerateGitHubDorksButton).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GenerateGitHubDorksButton).ForeColor = Color.FromArgb(224, 224, 224);
			GenerateGitHubDorksButton.HoverState.BorderColor = Color.Transparent;
			GenerateGitHubDorksButton.HoverState.CustomBorderColor = Color.Transparent;
			GenerateGitHubDorksButton.HoverState.FillColor = Color.FromArgb(147, 76, 245);
			GenerateGitHubDorksButton.HoverState.Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			GenerateGitHubDorksButton.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			((Control)GenerateGitHubDorksButton).Location = new Point(16, 71);
			((Control)GenerateGitHubDorksButton).Name = "GenerateGitHubDorksButton";
			GenerateGitHubDorksButton.PressedColor = Color.FromArgb(20, 20, 20);
			((Control)GenerateGitHubDorksButton).Size = new Size(432, 37);
			((Control)GenerateGitHubDorksButton).TabIndex = 7;
			((Control)GenerateGitHubDorksButton).Text = "Generate GitHub Dorks";
			((Control)GenerateGitHubDorksButton).Click += GenerateGitHubDorksButton_Click;
			GitHubTargetTextBox.Animated = true;
			GitHubTargetTextBox.BorderColor = Color.FromArgb(40, 40, 40);
			GitHubTargetTextBox.BorderRadius = 5;
			((Control)GitHubTargetTextBox).Cursor = Cursors.IBeam;
			GitHubTargetTextBox.DefaultText = "";
			GitHubTargetTextBox.DisabledState.BorderColor = Color.FromArgb(40, 40, 40);
			GitHubTargetTextBox.DisabledState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubTargetTextBox.DisabledState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubTargetTextBox.DisabledState.PlaceholderForeColor = Color.DimGray;
			GitHubTargetTextBox.FillColor = Color.FromArgb(20, 20, 20);
			GitHubTargetTextBox.FocusedState.BorderColor = Color.FromArgb(147, 76, 245);
			GitHubTargetTextBox.FocusedState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubTargetTextBox.FocusedState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubTargetTextBox.FocusedState.PlaceholderForeColor = Color.DimGray;
			((Control)GitHubTargetTextBox).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubTargetTextBox).ForeColor = Color.FromArgb(224, 224, 224);
			GitHubTargetTextBox.HoverState.BorderColor = Color.FromArgb(147, 76, 245);
			GitHubTargetTextBox.HoverState.FillColor = Color.FromArgb(20, 20, 20);
			GitHubTargetTextBox.HoverState.ForeColor = Color.FromArgb(224, 224, 224);
			GitHubTargetTextBox.HoverState.PlaceholderForeColor = Color.DimGray;
			((Control)GitHubTargetTextBox).Location = new Point(16, 29);
			GitHubTargetTextBox.MaxLength = 1000;
			((Control)GitHubTargetTextBox).Name = "GitHubTargetTextBox";
			GitHubTargetTextBox.PlaceholderForeColor = Color.DimGray;
			GitHubTargetTextBox.PlaceholderText = "Example: example.com";
			GitHubTargetTextBox.SelectedText = "";
			((Control)GitHubTargetTextBox).Size = new Size(432, 37);
			((Control)GitHubTargetTextBox).TabIndex = 6;
			GitHubTargetTextBox.WordWrap = false;
			((Control)GitHubTargetLabel).AutoSize = true;
			((Control)GitHubTargetLabel).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)GitHubTargetLabel).Location = new Point(16, 5);
			((Control)GitHubTargetLabel).Name = "GitHubTargetLabel";
			((Control)GitHubTargetLabel).Size = new Size(130, 19);
			((Control)GitHubTargetLabel).TabIndex = 5;
			((Control)GitHubTargetLabel).Text = "Target Domain/URL:";
			((Guna2Separator)GitHubDorksTabSeparator).FillColor = Color.FromArgb(40, 40, 40);
			((Control)GitHubDorksTabSeparator).Location = new Point(0, 0);
			((Control)GitHubDorksTabSeparator).Name = "GitHubDorksTabSeparator";
			((Control)GitHubDorksTabSeparator).Size = new Size(10, 333);
			((Control)GitHubDorksTabSeparator).TabIndex = 1;
			((Control)CreditsLabel).AutoSize = true;
			((Control)CreditsLabel).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)CreditsLabel).ForeColor = Color.DimGray;
			((Control)CreditsLabel).Location = new Point(43, 383);
			((Control)CreditsLabel).Name = "CreditsLabel";
			((Control)CreditsLabel).Size = new Size(114, 19);
			((Control)CreditsLabel).TabIndex = 8;
			((Control)CreditsLabel).Text = "Made By: Kap0ne";
			((Control)label1).AutoSize = true;
			((Control)label1).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)label1).ForeColor = Color.DimGray;
			((Control)label1).Location = new Point(58, 361);
			((Control)label1).Name = "label1";
			((Control)label1).Size = new Size(83, 19);
			((Control)label1).TabIndex = 9;
			((Control)label1).Text = "Version: v1.0";
			((ContainerControl)this).AutoScaleDimensions = new SizeF(7f, 17f);
			((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
			((Control)this).BackColor = Color.FromArgb(20, 20, 20);
			((Form)this).ClientSize = new Size(670, 432);
			((Control)this).Controls.Add((Control)(object)label1);
			((Control)this).Controls.Add((Control)(object)CreditsLabel);
			((Control)this).Controls.Add((Control)(object)MainFormTabControl);
			((Control)this).Controls.Add((Control)(object)MinimizeButton);
			((Control)this).Controls.Add((Control)(object)ExitButton);
			((Control)this).Controls.Add((Control)(object)Logo);
			((Control)this).Controls.Add((Control)(object)TitleLabel);
			((Control)this).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Control)this).ForeColor = Color.FromArgb(224, 224, 224);
			((Form)this).FormBorderStyle = (FormBorderStyle)0;
			((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			((Form)this).Margin = new Padding(4);
			((Form)this).MaximizeBox = false;
			((Form)this).MinimizeBox = false;
			((Control)this).Name = "MainForm";
			((Form)this).StartPosition = (FormStartPosition)1;
			((Control)this).Text = "DorkBuster";
			((ISupportInitialize)Logo).EndInit();
			((Control)MainFormTabControl).ResumeLayout(false);
			((Control)GoogleDorksTab).ResumeLayout(false);
			((Control)GoogleDorksTab).PerformLayout();
			((Control)GitHubDorksTab).ResumeLayout(false);
			((Control)GitHubDorksTab).PerformLayout();
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			LoadingForm loadingForm = new LoadingForm();
			try
			{
				if ((int)((Form)loadingForm).ShowDialog() == 1)
				{
					Application.Run((Form)(object)new MainForm());
				}
			}
			finally
			{
				((IDisposable)loadingForm)?.Dispose();
			}
		}
	}
}
namespace DorkBuster.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("DorkBuster.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal Resources()
		{
		}
	}
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)(object)SettingsBase.Synchronized((SettingsBase)(object)new Settings());

		public static Settings Default => defaultInstance;
	}
}
namespace DorkBuster.Forms
{
	public class LoadingForm : Form
	{
		private Timer loadingTimer;

		private int progressValue;

		private Timer fadeTimer;

		private double opacityIncrement = 0.05;

		private IContainer components;

		private Guna2BorderlessForm BorderlessLoadingForm;

		private Guna2PictureBox Logo;

		public LoadingForm()
		{
			InitializeComponent();
			SetupControls();
			StartLoadingAnimation();
		}

		private void SetupControls()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Expected O, but got Unknown
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Expected O, but got Unknown
			//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f8: Expected O, but got Unknown
			//IL_0274: Unknown result type (might be due to invalid IL or missing references)
			//IL_027e: Expected O, but got Unknown
			Guna2Panel val = new Guna2Panel
			{
				Dock = (DockStyle)5,
				BorderRadius = 15,
				FillColor = Color.FromArgb(20, 20, 20),
				BorderColor = Color.FromArgb(40, 40, 40),
				BorderThickness = 1
			};
			Label val2 = new Label
			{
				Text = "DorkBuster",
				Font = new Font("Segoe UI", 15f),
				ForeColor = Color.FromArgb(224, 224, 224),
				AutoSize = false,
				TextAlign = (ContentAlignment)32,
				Size = new Size(350, 40),
				Location = new Point(25, 120)
			};
			Label val3 = new Label
			{
				Text = "Loading...",
				Font = new Font("Segoe UI", 10f),
				ForeColor = Color.DimGray,
				AutoSize = false,
				TextAlign = (ContentAlignment)32,
				Size = new Size(350, 30),
				Location = new Point(25, 150)
			};
			Guna2ProgressBar progressBar = new Guna2ProgressBar
			{
				Size = new Size(350, 10),
				Location = new Point(25, 185),
				FillColor = Color.FromArgb(40, 40, 40),
				ProgressColor = Color.FromArgb(147, 76, 245),
				ProgressColor2 = Color.FromArgb(147, 76, 245),
				BorderRadius = 5,
				Minimum = 0,
				Maximum = 100,
				Value = 0
			};
			Label val4 = new Label
			{
				Text = "v1.0",
				Font = new Font("Segoe UI", 10f),
				ForeColor = Color.DimGray,
				AutoSize = true,
				Location = new Point(350, 220)
			};
			((Control)val).Controls.Add((Control)(object)Logo);
			((Control)Logo).Location = new Point(160, 40);
			((Control)val).Controls.Add((Control)(object)val2);
			((Control)val).Controls.Add((Control)(object)val3);
			((Control)val).Controls.Add((Control)(object)progressBar);
			((Control)val).Controls.Add((Control)(object)val4);
			((Control)this).Controls.Add((Control)(object)val);
			((Control)val).SendToBack();
			((Control)Logo).BringToFront();
			loadingTimer = new Timer();
			loadingTimer.Interval = 20;
			loadingTimer.Tick += delegate
			{
				progressValue++;
				progressBar.Value = progressValue;
				if (progressValue >= 100)
				{
					loadingTimer.Stop();
					StartFadeOut();
				}
			};
		}

		private void StartLoadingAnimation()
		{
			loadingTimer.Start();
		}

		private void StartFadeOut()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			fadeTimer = new Timer();
			fadeTimer.Interval = 20;
			fadeTimer.Tick += delegate
			{
				if (((Form)this).Opacity > 0.0)
				{
					((Form)this).Opacity = ((Form)this).Opacity - opacityIncrement;
				}
				else
				{
					fadeTimer.Stop();
					((Form)this).DialogResult = (DialogResult)1;
					((Form)this).Close();
				}
			};
			fadeTimer.Start();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((Form)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Expected O, but got Unknown
			//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Expected O, but got Unknown
			//IL_0211: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Expected O, but got Unknown
			//IL_021d: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LoadingForm));
			BorderlessLoadingForm = new Guna2BorderlessForm(components);
			Logo = new Guna2PictureBox();
			((ISupportInitialize)Logo).BeginInit();
			((Control)this).SuspendLayout();
			BorderlessLoadingForm.AnimateWindow = true;
			BorderlessLoadingForm.AnimationInterval = 200;
			BorderlessLoadingForm.AnimationType = (AnimateWindowType)16;
			BorderlessLoadingForm.BorderRadius = 20;
			BorderlessLoadingForm.ContainerControl = (ContainerControl)(object)this;
			BorderlessLoadingForm.DockIndicatorTransparencyValue = 0.6;
			BorderlessLoadingForm.DragStartTransparencyValue = 1.0;
			BorderlessLoadingForm.ResizeForm = false;
			BorderlessLoadingForm.ShadowColor = Color.FromArgb(147, 76, 245);
			BorderlessLoadingForm.TransparentWhileDrag = true;
			((Control)Logo).BackColor = Color.Transparent;
			((PictureBox)Logo).Image = (Image)componentResourceManager.GetObject("Logo.Image");
			Logo.ImageRotate = 0f;
			((Control)Logo).Location = new Point(144, 77);
			((Control)Logo).Name = "Logo";
			((Control)Logo).Size = new Size(80, 80);
			((PictureBox)Logo).SizeMode = (PictureBoxSizeMode)4;
			((PictureBox)Logo).TabIndex = 2;
			((PictureBox)Logo).TabStop = false;
			Logo.UseTransparentBackground = true;
			((ContainerControl)this).AutoScaleDimensions = new SizeF(7f, 17f);
			((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
			((Control)this).BackColor = Color.FromArgb(20, 20, 20);
			((Form)this).ClientSize = new Size(400, 250);
			((Form)this).ControlBox = false;
			((Control)this).Controls.Add((Control)(object)Logo);
			((Control)this).Font = new Font("Segoe UI Semilight", 10f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
			((Form)this).FormBorderStyle = (FormBorderStyle)0;
			((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			((Form)this).Margin = new Padding(4);
			((Form)this).MaximizeBox = false;
			((Form)this).MinimizeBox = false;
			((Control)this).Name = "LoadingForm";
			((Form)this).StartPosition = (FormStartPosition)1;
			((Control)this).Text = "DorkBuster";
			((ISupportInitialize)Logo).EndInit();
			((Control)this).ResumeLayout(false);
		}
	}
}
namespace Costura
{
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		private static object nullCacheLock = new object();

		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		private static int isAttached;

		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return string.Empty;
			}
			return culture.Name;
		}

		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			Assembly[] array = assemblies;
			foreach (Assembly assembly in array)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(CultureToString(name2.CultureInfo), CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		private static string GetAssemblyResourceName(AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = (CultureToString(requestedAssemblyName.CultureInfo) + "." + text).ToLowerInvariant();
			}
			return text;
		}

		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream stream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using DeflateStream source = new DeflateStream(stream, CompressionMode.Decompress);
					MemoryStream memoryStream = new MemoryStream();
					CopyTo(source, memoryStream);
					memoryStream.Position = 0L;
					return memoryStream;
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			if (resourceNames.TryGetValue(name, out var value))
			{
				return LoadStream(value);
			}
			return null;
		}

		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string assemblyResourceName = GetAssemblyResourceName(requestedAssemblyName);
			byte[] rawAssembly;
			using (Stream stream = LoadStream(assemblyNames, assemblyResourceName))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = ReadStream(stream);
			}
			using (Stream stream2 = LoadStream(symbolNames, assemblyResourceName))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			string name = e.Name;
			AssemblyName assemblyName = new AssemblyName(name);
			lock (nullCacheLock)
			{
				if (nullCache.ContainsKey(name))
				{
					return null;
				}
			}
			Assembly assembly = ReadExistingAssembly(assemblyName);
			if ((object)assembly != null)
			{
				return assembly;
			}
			assembly = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
			if ((object)assembly == null)
			{
				lock (nullCacheLock)
				{
					nullCache[name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		static AssemblyLoader()
		{
			assemblyNames.Add("costura", "costura.costura.dll.compressed");
			symbolNames.Add("costura", "costura.costura.pdb.compressed");
			assemblyNames.Add("guna.ui2", "costura.guna.ui2.dll.compressed");
		}

		public static void Attach(bool subscribe)
		{
			if (Interlocked.Exchange(ref isAttached, 1) == 1 || !subscribe)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs e)
			{
				string name = e.Name;
				AssemblyName assemblyName = new AssemblyName(name);
				lock (nullCacheLock)
				{
					if (nullCache.ContainsKey(name))
					{
						return (Assembly)null;
					}
				}
				Assembly assembly = ReadExistingAssembly(assemblyName);
				if ((object)assembly != null)
				{
					return assembly;
				}
				assembly = ReadFromEmbeddedResources(assemblyNames, symbolNames, assemblyName);
				if ((object)assembly == null)
				{
					lock (nullCacheLock)
					{
						nullCache[name] = true;
					}
					if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
					{
						assembly = Assembly.Load(assemblyName);
					}
				}
				return assembly;
			};
		}
	}
}
internal class DorkBuster_ProcessedByFody
{
	internal const string FodyVersion = "6.8.2.0";

	internal const string Costura = "6.0.0";
}
