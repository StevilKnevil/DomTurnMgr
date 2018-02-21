namespace DomTurnMgr
{
  partial class AddGameDialog
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
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.listView1 = new System.Windows.Forms.ListView();
      this.rbEA = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbMA = new System.Windows.Forms.RadioButton();
      this.rbLA = new System.Windows.Forms.RadioButton();
      this.cmbRace = new System.Windows.Forms.ComboBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(167, 335);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(86, 335);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.listView1_ItemActivate);
      // 
      // listView1
      // 
      this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listView1.FullRowSelect = true;
      this.listView1.Location = new System.Drawing.Point(13, 13);
      this.listView1.MultiSelect = false;
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(229, 193);
      this.listView1.TabIndex = 2;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.List;
      this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
      this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
      // 
      // rbEA
      // 
      this.rbEA.AutoSize = true;
      this.rbEA.Location = new System.Drawing.Point(7, 47);
      this.rbEA.Name = "rbEA";
      this.rbEA.Size = new System.Drawing.Size(70, 17);
      this.rbEA.TabIndex = 3;
      this.rbEA.Text = "Early Age";
      this.rbEA.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.cmbRace);
      this.groupBox1.Controls.Add(this.rbLA);
      this.groupBox1.Controls.Add(this.rbMA);
      this.groupBox1.Controls.Add(this.rbEA);
      this.groupBox1.Location = new System.Drawing.Point(13, 212);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(229, 117);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Race + Age";
      // 
      // rbMA
      // 
      this.rbMA.AutoSize = true;
      this.rbMA.Checked = true;
      this.rbMA.Location = new System.Drawing.Point(7, 70);
      this.rbMA.Name = "rbMA";
      this.rbMA.Size = new System.Drawing.Size(78, 17);
      this.rbMA.TabIndex = 4;
      this.rbMA.TabStop = true;
      this.rbMA.Text = "Middle Age";
      this.rbMA.UseVisualStyleBackColor = true;
      // 
      // rbLA
      // 
      this.rbLA.AutoSize = true;
      this.rbLA.Location = new System.Drawing.Point(7, 93);
      this.rbLA.Name = "rbLA";
      this.rbLA.Size = new System.Drawing.Size(68, 17);
      this.rbLA.TabIndex = 5;
      this.rbLA.Text = "Late Age";
      this.rbLA.UseVisualStyleBackColor = true;
      // 
      // cmbRace
      // 
      this.cmbRace.FormattingEnabled = true;
      this.cmbRace.Location = new System.Drawing.Point(7, 20);
      this.cmbRace.Name = "cmbRace";
      this.cmbRace.Size = new System.Drawing.Size(216, 21);
      this.cmbRace.TabIndex = 6;
      // 
      // AddGameDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(254, 370);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.listView1);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Name = "AddGameDialog";
      this.Text = "AddGameDialog";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.RadioButton rbEA;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cmbRace;
    private System.Windows.Forms.RadioButton rbLA;
    private System.Windows.Forms.RadioButton rbMA;
  }
}