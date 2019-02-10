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
      this.smtpPortText = new System.Windows.Forms.TextBox();
      this.smtpAddressText = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 66);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "IMAP Server";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 94);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(55, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "IMAP Port";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 174);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "User Name";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 200);
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
      // imapAddressText
      // 
      this.imapAddressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.imapAddressText.Location = new System.Drawing.Point(126, 63);
      this.imapAddressText.Name = "imapAddressText";
      this.imapAddressText.Size = new System.Drawing.Size(247, 20);
      this.imapAddressText.TabIndex = 5;
      this.imapAddressText.Text = "imap.gmail.com";
      this.imapAddressText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // imapPortText
      // 
      this.imapPortText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.imapPortText.Location = new System.Drawing.Point(126, 91);
      this.imapPortText.Name = "imapPortText";
      this.imapPortText.Size = new System.Drawing.Size(247, 20);
      this.imapPortText.TabIndex = 6;
      this.imapPortText.Text = "993";
      this.imapPortText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // usernameText
      // 
      this.usernameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.usernameText.Location = new System.Drawing.Point(126, 171);
      this.usernameText.Name = "usernameText";
      this.usernameText.Size = new System.Drawing.Size(247, 20);
      this.usernameText.TabIndex = 7;
      this.usernameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // passwordText
      // 
      this.passwordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.passwordText.Location = new System.Drawing.Point(126, 197);
      this.passwordText.Name = "passwordText";
      this.passwordText.Size = new System.Drawing.Size(247, 20);
      this.passwordText.TabIndex = 8;
      this.passwordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // configNameText
      // 
      this.configNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.configNameText.Location = new System.Drawing.Point(126, 12);
      this.configNameText.Name = "configNameText";
      this.configNameText.Size = new System.Drawing.Size(247, 20);
      this.configNameText.TabIndex = 9;
      this.configNameText.Text = "Gmail";
      this.configNameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // enableIMAPLabel
      // 
      this.enableIMAPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.enableIMAPLabel.AutoSize = true;
      this.enableIMAPLabel.Location = new System.Drawing.Point(264, 225);
      this.enableIMAPLabel.Name = "enableIMAPLabel";
      this.enableIMAPLabel.Size = new System.Drawing.Size(109, 13);
      this.enableIMAPLabel.TabIndex = 10;
      this.enableIMAPLabel.TabStop = true;
      this.enableIMAPLabel.Text = "Enable IMAP in Gmail";
      this.enableIMAPLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.enableIMAPLabel_LinkClicked);
      // 
      // createAppPasswordLabel
      // 
      this.createAppPasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.createAppPasswordLabel.AutoSize = true;
      this.createAppPasswordLabel.Location = new System.Drawing.Point(193, 238);
      this.createAppPasswordLabel.Name = "createAppPasswordLabel";
      this.createAppPasswordLabel.Size = new System.Drawing.Size(180, 13);
      this.createAppPasswordLabel.TabIndex = 11;
      this.createAppPasswordLabel.TabStop = true;
      this.createAppPasswordLabel.Text = "Create a Gmail Application Password";
      this.createAppPasswordLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createAppPasswordLabel_LinkClicked);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(298, 264);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 12;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(217, 264);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 0;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // smtpPortText
      // 
      this.smtpPortText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.smtpPortText.Location = new System.Drawing.Point(126, 145);
      this.smtpPortText.Name = "smtpPortText";
      this.smtpPortText.Size = new System.Drawing.Size(247, 20);
      this.smtpPortText.TabIndex = 16;
      this.smtpPortText.Text = "587";
      this.smtpPortText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // smtpAddressText
      // 
      this.smtpAddressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.smtpAddressText.Location = new System.Drawing.Point(126, 117);
      this.smtpAddressText.Name = "smtpAddressText";
      this.smtpAddressText.Size = new System.Drawing.Size(247, 20);
      this.smtpAddressText.TabIndex = 15;
      this.smtpAddressText.Text = "smtp.gmail.com";
      this.smtpAddressText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 148);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(59, 13);
      this.label6.TabIndex = 14;
      this.label6.Text = "SMTP Port";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 120);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(71, 13);
      this.label7.TabIndex = 13;
      this.label7.Text = "SMTP Server";
      // 
      // MailServerConfigForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(385, 299);
      this.Controls.Add(this.smtpPortText);
      this.Controls.Add(this.smtpAddressText);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label7);
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
    private System.Windows.Forms.TextBox smtpPortText;
    private System.Windows.Forms.TextBox smtpAddressText;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
  }
}