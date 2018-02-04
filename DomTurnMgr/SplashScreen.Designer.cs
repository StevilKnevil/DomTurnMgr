namespace DomTurnMgr
{
  partial class SplashScreen
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.lblGameName = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 66);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(260, 23);
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 0;
      this.progressBar1.UseWaitCursor = true;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(259, 23);
      this.label1.TabIndex = 1;
      this.label1.Text = "Downloading Message Headers for:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.label1.UseWaitCursor = true;
      // 
      // lblGameName
      // 
      this.lblGameName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblGameName.Location = new System.Drawing.Point(13, 36);
      this.lblGameName.Name = "lblGameName";
      this.lblGameName.Size = new System.Drawing.Size(259, 23);
      this.lblGameName.TabIndex = 2;
      this.lblGameName.Text = "GameName";
      this.lblGameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblGameName.UseWaitCursor = true;
      // 
      // SplashScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 101);
      this.ControlBox = false;
      this.Controls.Add(this.lblGameName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.progressBar1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "SplashScreen";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Dominions Turn Manager";
      this.UseWaitCursor = true;
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label label1;
    internal System.Windows.Forms.ProgressBar progressBar1;
    public System.Windows.Forms.Label lblGameName;
  }
}