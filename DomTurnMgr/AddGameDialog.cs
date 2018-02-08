using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class AddGameDialog : Form
  {
    public AddGameDialog()
    {
      InitializeComponent();
      foreach (string v in System.IO.Directory.EnumerateDirectories(Program.SettingsManager.SaveGameDirectory))
        listView1.Items.Add(System.IO.Path.GetFileName(v));
    }

    public string GameName { get; private set; }

    private void btnAddGame_Click(object sender, EventArgs e)
    {
      // Add the name to the list and select it.
      // TODO: Check to see if that name exists on the server
    }

    private void listView1_ItemActivate(object sender, EventArgs e)
    {
      this.GameName = listView1.SelectedItems[0].Text;
      this.DialogResult = DialogResult.OK;
      this.Hide();
    }
  }
}
