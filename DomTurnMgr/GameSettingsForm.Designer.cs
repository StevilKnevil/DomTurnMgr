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
      this.mailServerConfig.Location = new System.Drawing.Point(126, 63);
      this.mailServerConfig.Name = "mailServerConfig";
      this.mailServerConfig.Size = new System.Drawing.Size(323, 20);
      this.mailServerConfig.TabIndex = 5;
      this.mailServerConfig.Text = "Gmail";
      this.mailServerConfig.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // querySubjectText
      // 
      this.querySubjectText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.querySubjectText.Location = new System.Drawing.Point(126, 91);
      this.querySubjectText.Name = "querySubjectText";
      this.querySubjectText.Size = new System.Drawing.Size(323, 20);
      this.querySubjectText.TabIndex = 6;
      this.querySubjectText.Text = "New turn file: XXXX";
      this.querySubjectText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // querySenderText
      // 
      this.querySenderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.querySenderText.Location = new System.Drawing.Point(126, 117);
      this.querySenderText.Name = "querySenderText";
      this.querySenderText.Size = new System.Drawing.Size(323, 20);
      this.querySenderText.TabIndex = 7;
      this.querySenderText.Text = "turns@llamaserver.net";
      this.querySenderText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // gameNameText
      // 
      this.gameNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gameNameText.Location = new System.Drawing.Point(126, 12);
      this.gameNameText.Name = "gameNameText";
      this.gameNameText.Size = new System.Drawing.Size(323, 20);
      this.gameNameText.TabIndex = 9;
      this.gameNameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(374, 214);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 12;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(293, 214);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 0;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // GameSettingsForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(461, 249);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.gameNameText);
      this.Controls.Add(this.querySenderText);
      this.Controls.Add(this.querySubjectText);
      this.Controls.Add(this.mailServerConfig);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label3);
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
  }
}