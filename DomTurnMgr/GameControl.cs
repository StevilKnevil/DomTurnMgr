﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace DomTurnMgr
{
  public partial class GameControl : UserControl
  {
    public GameControl()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process process = new System.Diagnostics.Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Program.SettingsManager.GameExePath;
      // Extract the game name from the text on the parent tab page
      process.StartInfo.Arguments = this.Parent.Text;
      process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
      process.Start();
    }
  }
}
