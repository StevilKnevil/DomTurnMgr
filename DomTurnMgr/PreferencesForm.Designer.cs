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
      this.label1 = new System.Windows.Forms.Label();
      this.tbGameName = new System.Windows.Forms.TextBox();
      this.tbServerAddress = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbDominionsLocation = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.tbSavegamesLoction = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "exe";
      this.openFileDialog1.Filter = "Executable Files|*.exe";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Game Name";
      // 
      // tbGameName
      // 
      this.tbGameName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbGameName.Location = new System.Drawing.Point(126, 13);
      this.tbGameName.Name = "tbGameName";
      this.tbGameName.Size = new System.Drawing.Size(282, 20);
      this.tbGameName.TabIndex = 1;
      // 
      // tbServerAddress
      // 
      this.tbServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbServerAddress.Location = new System.Drawing.Point(126, 39);
      this.tbServerAddress.Name = "tbServerAddress";
      this.tbServerAddress.Size = new System.Drawing.Size(282, 20);
      this.tbServerAddress.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 39);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Server";
      // 
      // tbDominionsLocation
      // 
      this.tbDominionsLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbDominionsLocation.Location = new System.Drawing.Point(126, 65);
      this.tbDominionsLocation.Name = "tbDominionsLocation";
      this.tbDominionsLocation.Size = new System.Drawing.Size(262, 20);
      this.tbDominionsLocation.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(13, 68);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Dominions Location";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.Location = new System.Drawing.Point(333, 121);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 6;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.BackColor = System.Drawing.SystemColors.Control;
      this.button2.Location = new System.Drawing.Point(384, 65);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(23, 20);
      this.button2.TabIndex = 7;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button3.BackColor = System.Drawing.SystemColors.Control;
      this.button3.Location = new System.Drawing.Point(384, 91);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(23, 20);
      this.button3.TabIndex = 10;
      this.button3.Text = "...";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // tbSavegamesLoction
      // 
      this.tbSavegamesLoction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSavegamesLoction.Location = new System.Drawing.Point(126, 91);
      this.tbSavegamesLoction.Name = "tbSavegamesLoction";
      this.tbSavegamesLoction.Size = new System.Drawing.Size(262, 20);
      this.tbSavegamesLoction.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 94);
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
      this.ClientSize = new System.Drawing.Size(420, 156);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.tbSavegamesLoction);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.tbDominionsLocation);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.tbServerAddress);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbGameName);
      this.Controls.Add(this.label1);
      this.Name = "PreferencesForm";
      this.Text = "Preferences";
      this.Shown += new System.EventHandler(this.PreferencesForm_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbGameName;
    private System.Windows.Forms.TextBox tbServerAddress;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbDominionsLocation;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TextBox tbSavegamesLoction;
    private System.Windows.Forms.Label label4;
  }
}