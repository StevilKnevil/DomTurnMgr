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
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("WORKING....");
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dom5InspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.listView1 = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.fadingStatusText1 = new DomTurnMgr.FadingStatusLabel();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.timeRemainingLbl = new System.Windows.Forms.ToolStripStatusLabel();
      this.label1 = new System.Windows.Forms.Label();
      this.lblTurnNumber = new System.Windows.Forms.Label();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lvRaceStatus = new System.Windows.Forms.ListView();
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.editToolStripMenuItem,
            this.dom5InspectorToolStripMenuItem,
            this.aboutToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.ShowItemToolTips = true;
      this.menuStrip1.Size = new System.Drawing.Size(748, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMenuItem2.Image = global::DomTurnMgr.Properties.Resources.newFile;
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(28, 20);
      this.toolStripMenuItem2.Text = "Add Game";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.newGame_Click);
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
      this.button2.Location = new System.Drawing.Point(661, 75);
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
      this.button3.Location = new System.Drawing.Point(661, 131);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 50);
      this.button3.TabIndex = 2;
      this.button3.Text = "Send Completed Turn";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.btnSend2h_Click);
      // 
      // listView1
      // 
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.FullRowSelect = true;
      listViewGroup1.Header = "Pending Turns";
      listViewGroup1.Name = "pendingGroup";
      listViewGroup2.Header = "Completed Turns";
      listViewGroup2.Name = "completeGroup";
      this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
      this.listView1.HideSelection = false;
      this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.MultiSelect = false;
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(438, 406);
      this.listView1.TabIndex = 4;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Turn";
      this.columnHeader1.Width = 94;
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
      this.statusStrip1.Location = new System.Drawing.Point(0, 436);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(748, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // fadingStatusText1
      // 
      this.fadingStatusText1.Duration = 3F;
      this.fadingStatusText1.Name = "fadingStatusText1";
      this.fadingStatusText1.Size = new System.Drawing.Size(0, 17);
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(650, 17);
      this.toolStripStatusLabel2.Spring = true;
      // 
      // timeRemainingLbl
      // 
      this.timeRemainingLbl.Name = "timeRemainingLbl";
      this.timeRemainingLbl.Size = new System.Drawing.Size(83, 17);
      this.timeRemainingLbl.Text = "Next Turn Due";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(661, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 18);
      this.label1.TabIndex = 6;
      this.label1.Text = "Current turn:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblTurnNumber
      // 
      this.lblTurnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblTurnNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTurnNumber.Location = new System.Drawing.Point(661, 45);
      this.lblTurnNumber.Name = "lblTurnNumber";
      this.lblTurnNumber.Size = new System.Drawing.Size(75, 27);
      this.lblTurnNumber.TabIndex = 7;
      this.lblTurnNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.BalloonTipTitle = "Dominions Turn Manager";
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Text = "Dominions Turn Manager";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.DoubleClick += new System.EventHandler(this.onRestoreForm);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
      // 
      // showToolStripMenuItem
      // 
      this.showToolStripMenuItem.Name = "showToolStripMenuItem";
      this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.showToolStripMenuItem.Text = "Show";
      this.showToolStripMenuItem.Click += new System.EventHandler(this.onRestoreForm);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // lvRaceStatus
      // 
      this.lvRaceStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
      this.lvRaceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvRaceStatus.FullRowSelect = true;
      this.lvRaceStatus.HideSelection = false;
      this.lvRaceStatus.Location = new System.Drawing.Point(0, 0);
      this.lvRaceStatus.MultiSelect = false;
      this.lvRaceStatus.Name = "lvRaceStatus";
      this.lvRaceStatus.Size = new System.Drawing.Size(201, 406);
      this.lvRaceStatus.TabIndex = 8;
      this.lvRaceStatus.UseCompatibleStateImageBehavior = false;
      this.lvRaceStatus.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Race";
      this.columnHeader3.Width = 94;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Status";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(12, 27);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.listView1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.lvRaceStatus);
      this.splitContainer1.Size = new System.Drawing.Size(643, 406);
      this.splitContainer1.SplitterDistance = 438;
      this.splitContainer1.TabIndex = 9;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(748, 458);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.lblTurnNumber);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.menuStrip1);
      this.Icon = global::DomTurnMgr.Properties.Resources.icon_grey;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Dominions Turn Manager";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private DomTurnMgr.FadingStatusLabel fadingStatusText1;
    private System.Windows.Forms.ToolStripMenuItem dom5InspectorToolStripMenuItem;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel timeRemainingLbl;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblTurnNumber;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ListView lvRaceStatus;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.SplitContainer splitContainer1;
  }
}

