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
    public string GameRace { get; private set; }
    public string GameEra { get; private set; }

    private void btnAddGame_Click(object sender, EventArgs e)
    {
      // Add the name to the list and select it.
      // TODO: Check to see if that name exists on the server
    }

    private void listView1_ItemActivate(object sender, EventArgs e)
    {
      if (cmbRace.Text == string.Empty)
      {
        MessageBox.Show("Please enter or select a valid name for the race.", "Error");
      }
      else
      {
        this.GameName = listView1.SelectedItems[0].Text;
        this.GameRace = cmbRace.SelectedItem.ToString();
        this.GameEra = rbEA.Checked ? "Early" : rbMA.Checked ? "Mid" : rbLA.Checked ? "Late" : "<<ERROR>>";
        this.DialogResult = DialogResult.OK;
        this.Hide();
      }
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      cmbRace.Items.Clear();
      cmbRace.Text = "";
      if (listView1.SelectedItems.Count == 1)
      {
        Cursor.Current = Cursors.WaitCursor;
        // Update the race combo box with info from the server
        string data = ServerWatcher.GetServerData(listView1.SelectedItems[0].Text);
        Dictionary<string, bool> races = ServerWatcher.ExtractRaceInfo(data);
        cmbRace.Items.AddRange(races.Keys.ToArray());
        if (cmbRace.Items.Count > 0)
          cmbRace.SelectedIndex = 0;
        Cursor.Current = Cursors.Default;
      }
    }
  }
}
