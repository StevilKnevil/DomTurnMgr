namespace DomTurnMgr
{
  partial class GameSettingsForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.mailServerConfig = new System.Windows.Forms.TextBox();
      this.querySubjectText = new System.Windows.Forms.TextBox();
      this.querySenderText = new System.Windows.Forms.TextBox();
      this.gameNameText = new System.Windows.Forms.TextBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.serverInfoURL = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.serverAdminPassword = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.mailServerConfigResult = new System.Windows.Forms.Label();
      this.querySubjectTextResult = new System.Windows.Forms.Label();
      this.querySenderTextResult = new System.Windows.Forms.Label();
      this.serverInfoURLResult = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 66);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Server";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 94);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Mail Subject Text";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 120);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Mail Sender Name";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(15, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(66, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Game Name";
      // 
      // mailServerConfig
      // 
      this.mailServerConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.mailServerConfig.Location = new System.Drawing.Point(137, 63);
      this.mailServerConfig.Name = "mailServerConfig";
      this.mailServerConfig.Size = new System.Drawing.Size(218, 20);
      this.mailServerConfig.TabIndex = 1;
      this.mailServerConfig.Text = "Gmail";
      this.mailServerConfig.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.mailServerConfig.TextChanged += new System.EventHandler(this.onTextChanged);
      // 
      // querySubjectText
      // 
      this.querySubjectText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.querySubjectText.Location = new System.Drawing.Point(137, 91);
      this.querySubjectText.Name = "querySubjectText";
      this.querySubjectText.Size = new System.Drawing.Size(218, 20);
      this.querySubjectText.TabIndex = 2;
      this.querySubjectText.Text = "New turn file: {GAMENAME}";
      this.querySubjectText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.querySubjectText.TextChanged += new System.EventHandler(this.onTextChanged);
      // 
      // querySenderText
      // 
      this.querySenderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.querySenderText.Location = new System.Drawing.Point(137, 117);
      this.querySenderText.Name = "querySenderText";
      this.querySenderText.Size = new System.Drawing.Size(218, 20);
      this.querySenderText.TabIndex = 3;
      this.querySenderText.Text = "turns@llamaserver.net";
      this.querySenderText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.querySenderText.TextChanged += new System.EventHandler(this.onTextChanged);
      // 
      // gameNameText
      // 
      this.gameNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gameNameText.Location = new System.Drawing.Point(87, 12);
      this.gameNameText.Name = "gameNameText";
      this.gameNameText.Size = new System.Drawing.Size(540, 20);
      this.gameNameText.TabIndex = 0;
      this.gameNameText.Text = "<<Enter Game Name>>";
      this.gameNameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.gameNameText.TextChanged += new System.EventHandler(this.onTextChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(552, 213);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(471, 213);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // serverInfoURL
      // 
      this.serverInfoURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.serverInfoURL.Location = new System.Drawing.Point(137, 143);
      this.serverInfoURL.Name = "serverInfoURL";
      this.serverInfoURL.Size = new System.Drawing.Size(218, 20);
      this.serverInfoURL.TabIndex = 4;
      this.serverInfoURL.Text = "http://www.llamaserver.net/gameinfo.cgi?game={GAMENAME}";
      this.serverInfoURL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.serverInfoURL.TextChanged += new System.EventHandler(this.onTextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 146);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(87, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "Server Info Page";
      // 
      // serverAdminPassword
      // 
      this.serverAdminPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.serverAdminPassword.Location = new System.Drawing.Point(137, 169);
      this.serverAdminPassword.Name = "serverAdminPassword";
      this.serverAdminPassword.PasswordChar = '*';
      this.serverAdminPassword.Size = new System.Drawing.Size(218, 20);
      this.serverAdminPassword.TabIndex = 5;
      this.serverAdminPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 172);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(119, 13);
      this.label6.TabIndex = 15;
      this.label6.Text = "Server Admin Password";
      // 
      // mailServerConfigResult
      // 
      this.mailServerConfigResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.mailServerConfigResult.AutoSize = true;
      this.mailServerConfigResult.Location = new System.Drawing.Point(361, 66);
      this.mailServerConfigResult.Name = "mailServerConfigResult";
      this.mailServerConfigResult.Size = new System.Drawing.Size(116, 13);
      this.mailServerConfigResult.TabIndex = 0;
      this.mailServerConfigResult.Text = "mailServerConfigResult";
      // 
      // querySubjectTextResult
      // 
      this.querySubjectTextResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.querySubjectTextResult.AutoSize = true;
      this.querySubjectTextResult.Location = new System.Drawing.Point(361, 94);
      this.querySubjectTextResult.Name = "querySubjectTextResult";
      this.querySubjectTextResult.Size = new System.Drawing.Size(120, 13);
      this.querySubjectTextResult.TabIndex = 1;
      this.querySubjectTextResult.Text = "querySubjectTextResult";
      // 
      // querySenderTextResult
      // 
      this.querySenderTextResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.querySenderTextResult.AutoSize = true;
      this.querySenderTextResult.Location = new System.Drawing.Point(361, 120);
      this.querySenderTextResult.Name = "querySenderTextResult";
      this.querySenderTextResult.Size = new System.Drawing.Size(118, 13);
      this.querySenderTextResult.TabIndex = 2;
      this.querySenderTextResult.Text = "querySenderTextResult";
      // 
      // serverInfoURLResult
      // 
      this.serverInfoURLResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.serverInfoURLResult.AutoSize = true;
      this.serverInfoURLResult.Location = new System.Drawing.Point(361, 146);
      this.serverInfoURLResult.Name = "serverInfoURLResult";
      this.serverInfoURLResult.Size = new System.Drawing.Size(106, 13);
      this.serverInfoURLResult.TabIndex = 13;
      this.serverInfoURLResult.Text = "serverInfoURLResult";
      // 
      // GameSettingsForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(639, 248);
      this.Controls.Add(this.serverAdminPassword);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.serverInfoURLResult);
      this.Controls.Add(this.serverInfoURL);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.gameNameText);
      this.Controls.Add(this.querySenderText);
      this.Controls.Add(this.querySubjectText);
      this.Controls.Add(this.mailServerConfig);
      this.Controls.Add(this.querySenderTextResult);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.querySubjectTextResult);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.mailServerConfigResult);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GameSettingsForm";
      this.Text = "MailServerConfigForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox mailServerConfig;
    private System.Windows.Forms.TextBox querySubjectText;
    private System.Windows.Forms.TextBox querySenderText;
    private System.Windows.Forms.TextBox gameNameText;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox serverInfoURL;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox serverAdminPassword;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label mailServerConfigResult;
    private System.Windows.Forms.Label querySubjectTextResult;
    private System.Windows.Forms.Label querySenderTextResult;
    private System.Windows.Forms.Label serverInfoURLResult;
  }
}