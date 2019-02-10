namespace DomTurnMgr
{
  partial class MailServerConfigForm
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
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.imapAddressText = new System.Windows.Forms.TextBox();
      this.imapPortText = new System.Windows.Forms.TextBox();
      this.usernameText = new System.Windows.Forms.TextBox();
      this.passwordText = new System.Windows.Forms.TextBox();
      this.configNameText = new System.Windows.Forms.TextBox();
      this.enableIMAPLabel = new System.Windows.Forms.LinkLabel();
      this.createAppPasswordLabel = new System.Windows.Forms.LinkLabel();
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
      this.label2.Size = new System.Drawing.Size(26, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Port";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 120);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "User Name";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 146);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(108, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Application Password";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(15, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(100, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Configuration Name";
      // 
      // hostnameText
      // 
      this.imapAddressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.imapAddressText.Location = new System.Drawing.Point(126, 63);
      this.imapAddressText.Name = "hostnameText";
      this.imapAddressText.Size = new System.Drawing.Size(323, 20);
      this.imapAddressText.TabIndex = 5;
      this.imapAddressText.Text = "imap.gmail.com";
      this.imapAddressText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // portText
      // 
      this.imapPortText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.imapPortText.Location = new System.Drawing.Point(126, 91);
      this.imapPortText.Name = "portText";
      this.imapPortText.Size = new System.Drawing.Size(323, 20);
      this.imapPortText.TabIndex = 6;
      this.imapPortText.Text = "993";
      this.imapPortText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // usernameText
      // 
      this.usernameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.usernameText.Location = new System.Drawing.Point(126, 117);
      this.usernameText.Name = "usernameText";
      this.usernameText.Size = new System.Drawing.Size(323, 20);
      this.usernameText.TabIndex = 7;
      this.usernameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // passwordText
      // 
      this.passwordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.passwordText.Location = new System.Drawing.Point(126, 143);
      this.passwordText.Name = "passwordText";
      this.passwordText.Size = new System.Drawing.Size(323, 20);
      this.passwordText.TabIndex = 8;
      this.passwordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // configNameText
      // 
      this.configNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.configNameText.Location = new System.Drawing.Point(126, 12);
      this.configNameText.Name = "configNameText";
      this.configNameText.Size = new System.Drawing.Size(323, 20);
      this.configNameText.TabIndex = 9;
      this.configNameText.Text = "Gmail";
      this.configNameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // enableIMAPLabel
      // 
      this.enableIMAPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.enableIMAPLabel.AutoSize = true;
      this.enableIMAPLabel.Location = new System.Drawing.Point(340, 175);
      this.enableIMAPLabel.Name = "enableIMAPLabel";
      this.enableIMAPLabel.Size = new System.Drawing.Size(109, 13);
      this.enableIMAPLabel.TabIndex = 10;
      this.enableIMAPLabel.TabStop = true;
      this.enableIMAPLabel.Text = "Enable IMAP in Gmail";
      this.enableIMAPLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.enableIMAPLabel_LinkClicked);
      // 
      // createAppPasswordLabel
      // 
      this.createAppPasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.createAppPasswordLabel.AutoSize = true;
      this.createAppPasswordLabel.Location = new System.Drawing.Point(269, 188);
      this.createAppPasswordLabel.Name = "createAppPasswordLabel";
      this.createAppPasswordLabel.Size = new System.Drawing.Size(180, 13);
      this.createAppPasswordLabel.TabIndex = 11;
      this.createAppPasswordLabel.TabStop = true;
      this.createAppPasswordLabel.Text = "Create a Gmail Application Password";
      this.createAppPasswordLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createAppPasswordLabel_LinkClicked);
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
      // MailServerConfigForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(461, 249);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.createAppPasswordLabel);
      this.Controls.Add(this.enableIMAPLabel);
      this.Controls.Add(this.configNameText);
      this.Controls.Add(this.passwordText);
      this.Controls.Add(this.usernameText);
      this.Controls.Add(this.imapPortText);
      this.Controls.Add(this.imapAddressText);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MailServerConfigForm";
      this.Text = "MailServerConfigForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox imapAddressText;
    private System.Windows.Forms.TextBox imapPortText;
    private System.Windows.Forms.TextBox usernameText;
    private System.Windows.Forms.TextBox passwordText;
    private System.Windows.Forms.TextBox configNameText;
    private System.Windows.Forms.LinkLabel enableIMAPLabel;
    private System.Windows.Forms.LinkLabel createAppPasswordLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
  }
}