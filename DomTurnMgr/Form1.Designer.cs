namespace DomTurnMgr
{
  partial class Form1
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Pending Turns", System.Windows.Forms.HorizontalAlignment.Left);
      System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Completed Turns", System.Windows.Forms.HorizontalAlignment.Left);
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.listView1 = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.timeRemainingLbl = new System.Windows.Forms.ToolStripStatusLabel();
      this.updateTimer = new System.Windows.Forms.Timer(this.components);
      this.fadingStatusText1 = new DomTurnMgr.FadingStatusLabel();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dom5InspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem,
            this.dom5InspectorToolStripMenuItem,
            this.aboutToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.ShowItemToolTips = true;
      this.menuStrip1.Size = new System.Drawing.Size(714, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(627, 83);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 50);
      this.button2.TabIndex = 1;
      this.button2.Text = "Launch Dominions";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.btnStartDominions_Click);
      // 
      // button3
      // 
      this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button3.Location = new System.Drawing.Point(627, 139);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 50);
      this.button3.TabIndex = 2;
      this.button3.Text = "Send Completed Turn";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.btnSend2h_Click);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(627, 27);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 50);
      this.button1.TabIndex = 0;
      this.button1.Text = "Get Turn";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.btnGetTrn_Click);
      // 
      // listView1
      // 
      this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.listView1.FullRowSelect = true;
      listViewGroup1.Header = "Pending Turns";
      listViewGroup1.Name = "pendingGroup";
      listViewGroup2.Header = "Completed Turns";
      listViewGroup2.Name = "completeGroup";
      this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
      this.listView1.HideSelection = false;
      this.listView1.Location = new System.Drawing.Point(12, 27);
      this.listView1.MultiSelect = false;
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(609, 400);
      this.listView1.TabIndex = 4;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Turn";
      this.columnHeader1.Width = 34;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Status";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fadingStatusText1,
            this.toolStripStatusLabel2,
            this.timeRemainingLbl});
      this.statusStrip1.Location = new System.Drawing.Point(0, 430);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(714, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(616, 17);
      this.toolStripStatusLabel2.Spring = true;
      // 
      // timeRemainingLbl
      // 
      this.timeRemainingLbl.Name = "timeRemainingLbl";
      this.timeRemainingLbl.Size = new System.Drawing.Size(83, 17);
      this.timeRemainingLbl.Text = "Next Turn Due";
      // 
      // updateTimer
      // 
      this.updateTimer.Interval = 60000;
      this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
      // 
      // fadingStatusText1
      // 
      this.fadingStatusText1.Duration = 3F;
      this.fadingStatusText1.Name = "fadingStatusText1";
      this.fadingStatusText1.Size = new System.Drawing.Size(0, 17);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMenuItem1.Image = global::DomTurnMgr.Properties.Resources.refresh;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 20);
      this.toolStripMenuItem1.Text = "Refresh";
      this.toolStripMenuItem1.ToolTipText = "Refresh";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.refresh_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.editToolStripMenuItem.Image = global::DomTurnMgr.Properties.Resources.settings;
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
      this.editToolStripMenuItem.Text = "Preferences";
      this.editToolStripMenuItem.ToolTipText = "Settings";
      this.editToolStripMenuItem.Click += new System.EventHandler(this.showPrefs_Click);
      // 
      // dom5InspectorToolStripMenuItem
      // 
      this.dom5InspectorToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.dom5InspectorToolStripMenuItem.Image = global::DomTurnMgr.Properties.Resources.magnifier;
      this.dom5InspectorToolStripMenuItem.Name = "dom5InspectorToolStripMenuItem";
      this.dom5InspectorToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
      this.dom5InspectorToolStripMenuItem.Text = "Dom 5 Inspector";
      this.dom5InspectorToolStripMenuItem.ToolTipText = "Dom 5 Inspector";
      this.dom5InspectorToolStripMenuItem.Click += new System.EventHandler(this.dom5InspectorToolStripMenuItem_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(714, 452);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.listView1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(Properties.Resources.icon_grey));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Dominions Turn Manager";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private DomTurnMgr.FadingStatusLabel fadingStatusText1;
    private System.Windows.Forms.ToolStripMenuItem dom5InspectorToolStripMenuItem;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel timeRemainingLbl;
    private System.Windows.Forms.Timer updateTimer;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
  }
}

