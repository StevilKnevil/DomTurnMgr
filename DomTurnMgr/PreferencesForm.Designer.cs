namespace DomTurnMgr
{
  partial class PreferencesForm
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
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.tbServerAddress = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbDominionsLocation = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.tbSavegamesLoction = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "exe";
      this.openFileDialog1.Filter = "Executable Files|*.exe";
      // 
      // tbServerAddress
      // 
      this.tbServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbServerAddress.Location = new System.Drawing.Point(126, 12);
      this.tbServerAddress.Name = "tbServerAddress";
      this.tbServerAddress.Size = new System.Drawing.Size(282, 20);
      this.tbServerAddress.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 12);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Server";
      // 
      // tbDominionsLocation
      // 
      this.tbDominionsLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbDominionsLocation.Enabled = false;
      this.tbDominionsLocation.Location = new System.Drawing.Point(126, 38);
      this.tbDominionsLocation.Name = "tbDominionsLocation";
      this.tbDominionsLocation.Size = new System.Drawing.Size(282, 20);
      this.tbDominionsLocation.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(13, 41);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Dominions Location";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.Location = new System.Drawing.Point(333, 94);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 6;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // tbSavegamesLoction
      // 
      this.tbSavegamesLoction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSavegamesLoction.Enabled = false;
      this.tbSavegamesLoction.Location = new System.Drawing.Point(126, 64);
      this.tbSavegamesLoction.Name = "tbSavegamesLoction";
      this.tbSavegamesLoction.Size = new System.Drawing.Size(282, 20);
      this.tbSavegamesLoction.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 67);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(107, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Savegames Location";
      // 
      // PreferencesForm
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(420, 129);
      this.Controls.Add(this.tbSavegamesLoction);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.tbDominionsLocation);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.tbServerAddress);
      this.Controls.Add(this.label2);
      this.Name = "PreferencesForm";
      this.Text = "Preferences";
      this.Shown += new System.EventHandler(this.PreferencesForm_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TextBox tbServerAddress;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbDominionsLocation;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox tbSavegamesLoction;
    private System.Windows.Forms.Label label4;
  }
}