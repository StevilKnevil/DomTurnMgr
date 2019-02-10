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
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.submitTurnButton = new System.Windows.Forms.Button();
      this.browseFilesButton = new System.Windows.Forms.Button();
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
      this.comboBox1.Size = new System.Drawing.Size(684, 21);
      this.comboBox1.TabIndex = 1;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // launchDomsButton
      // 
      this.launchDomsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.launchDomsButton.Location = new System.Drawing.Point(734, 3);
      this.launchDomsButton.Name = "launchDomsButton";
      this.launchDomsButton.Size = new System.Drawing.Size(75, 50);
      this.launchDomsButton.TabIndex = 4;
      this.launchDomsButton.Text = "Launch Dominions";
      this.launchDomsButton.UseVisualStyleBackColor = true;
      this.launchDomsButton.Click += new System.EventHandler(this.launchDomsButton_Click);
      // 
      // webBrowser1
      // 
      this.webBrowser1.AllowNavigation = false;
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.Location = new System.Drawing.Point(3, 59);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(726, 520);
      this.webBrowser1.TabIndex = 5;
      // 
      // comboBox2
      // 
      this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new System.Drawing.Point(44, 32);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new System.Drawing.Size(684, 21);
      this.comboBox2.TabIndex = 6;
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
      this.submitTurnButton.Location = new System.Drawing.Point(734, 59);
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
      this.browseFilesButton.Location = new System.Drawing.Point(734, 529);
      this.browseFilesButton.Name = "browseFilesButton";
      this.browseFilesButton.Size = new System.Drawing.Size(75, 50);
      this.browseFilesButton.TabIndex = 9;
      this.browseFilesButton.Text = "Browse Local Files";
      this.browseFilesButton.UseVisualStyleBackColor = true;
      this.browseFilesButton.Click += new System.EventHandler(this.browseFilesButton_Click);
      // 
      // GameControl
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.browseFilesButton);
      this.Controls.Add(this.submitTurnButton);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.comboBox2);
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.launchDomsButton);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label1);
      this.Name = "GameControl";
      this.Size = new System.Drawing.Size(812, 582);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GameControl_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GameControl_DragEnter);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button launchDomsButton;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button submitTurnButton;
    private System.Windows.Forms.Button browseFilesButton;
  }
}
