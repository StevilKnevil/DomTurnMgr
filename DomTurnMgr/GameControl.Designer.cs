namespace DomTurnMgr
{
  partial class GameControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.launchDomsButton = new System.Windows.Forms.Button();
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.submitTurnButton = new System.Windows.Forms.Button();
      this.browseFilesButton = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.adminOptionsButton = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.gameInfoBrowser = new System.Windows.Forms.WebBrowser();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.staleDataBrowser = new System.Windows.Forms.WebBrowser();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(33, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Race";
      // 
      // comboBox1
      // 
      this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(44, 3);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(508, 21);
      this.comboBox1.TabIndex = 1;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // launchDomsButton
      // 
      this.launchDomsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.launchDomsButton.Location = new System.Drawing.Point(558, 3);
      this.launchDomsButton.Name = "launchDomsButton";
      this.launchDomsButton.Size = new System.Drawing.Size(75, 50);
      this.launchDomsButton.TabIndex = 4;
      this.launchDomsButton.Text = "Launch Dominions";
      this.launchDomsButton.UseVisualStyleBackColor = true;
      this.launchDomsButton.Click += new System.EventHandler(this.launchDomsButton_Click);
      // 
      // comboBox2
      // 
      this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new System.Drawing.Point(44, 32);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new System.Drawing.Size(508, 21);
      this.comboBox2.TabIndex = 6;
      this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(5, 35);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Turn";
      // 
      // submitTurnButton
      // 
      this.submitTurnButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.submitTurnButton.Location = new System.Drawing.Point(558, 59);
      this.submitTurnButton.Name = "submitTurnButton";
      this.submitTurnButton.Size = new System.Drawing.Size(75, 50);
      this.submitTurnButton.TabIndex = 8;
      this.submitTurnButton.Text = "Submit Turn";
      this.submitTurnButton.UseVisualStyleBackColor = true;
      this.submitTurnButton.Click += new System.EventHandler(this.submitTurnButton_Click);
      // 
      // browseFilesButton
      // 
      this.browseFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.browseFilesButton.Location = new System.Drawing.Point(558, 322);
      this.browseFilesButton.Name = "browseFilesButton";
      this.browseFilesButton.Size = new System.Drawing.Size(75, 50);
      this.browseFilesButton.TabIndex = 9;
      this.browseFilesButton.Text = "Browse Local Files";
      this.browseFilesButton.UseVisualStyleBackColor = true;
      this.browseFilesButton.Click += new System.EventHandler(this.browseFilesButton_Click);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button1.Location = new System.Drawing.Point(3, 348);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(102, 23);
      this.button1.TabIndex = 10;
      this.button1.Text = "Open In Browser";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // adminOptionsButton
      // 
      this.adminOptionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.adminOptionsButton.Location = new System.Drawing.Point(111, 348);
      this.adminOptionsButton.Name = "adminOptionsButton";
      this.adminOptionsButton.Size = new System.Drawing.Size(102, 23);
      this.adminOptionsButton.TabIndex = 11;
      this.adminOptionsButton.Text = "Admin Options";
      this.adminOptionsButton.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(0, 59);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(552, 283);
      this.tabControl1.TabIndex = 12;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.gameInfoBrowser);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(544, 257);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Game Details";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // gameInfoBrowser
      // 
      this.gameInfoBrowser.AllowNavigation = false;
      this.gameInfoBrowser.AllowWebBrowserDrop = false;
      this.gameInfoBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gameInfoBrowser.IsWebBrowserContextMenuEnabled = false;
      this.gameInfoBrowser.Location = new System.Drawing.Point(3, 3);
      this.gameInfoBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.gameInfoBrowser.Name = "gameInfoBrowser";
      this.gameInfoBrowser.Size = new System.Drawing.Size(538, 251);
      this.gameInfoBrowser.TabIndex = 6;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.staleDataBrowser);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(544, 257);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Stale Turn Data";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // staleDataBrowser
      // 
      this.staleDataBrowser.AllowNavigation = false;
      this.staleDataBrowser.AllowWebBrowserDrop = false;
      this.staleDataBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.staleDataBrowser.IsWebBrowserContextMenuEnabled = false;
      this.staleDataBrowser.Location = new System.Drawing.Point(3, 3);
      this.staleDataBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.staleDataBrowser.Name = "staleDataBrowser";
      this.staleDataBrowser.Size = new System.Drawing.Size(538, 251);
      this.staleDataBrowser.TabIndex = 6;
      // 
      // GameControl
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.adminOptionsButton);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.browseFilesButton);
      this.Controls.Add(this.submitTurnButton);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.comboBox2);
      this.Controls.Add(this.launchDomsButton);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label1);
      this.Name = "GameControl";
      this.Size = new System.Drawing.Size(636, 375);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GameControl_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GameControl_DragEnter);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button launchDomsButton;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button submitTurnButton;
    private System.Windows.Forms.Button browseFilesButton;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button adminOptionsButton;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.WebBrowser gameInfoBrowser;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.WebBrowser staleDataBrowser;
  }
}
