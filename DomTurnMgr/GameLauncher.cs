using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class GameLauncher
  {
    private Process process;
    private TurnManager turnManager;
    private string gameName;
    private TurnManager.GameFile sourceFile;

    private string tempGameName => System.Windows.Forms.Application.ProductName;
    private string tempDestDir => Path.Combine(
          Program.SettingsManager.SaveGameDirectory,
          tempGameName);

    public GameLauncher(string gn, TurnManager tm, TurnManager.GameFile turnFile)
    {
      turnManager = tm;
      gameName = gn;
      sourceFile = turnFile;

      // Empty all files from that directory
      Directory.CreateDirectory(tempDestDir);
      foreach (string path in Directory.EnumerateFiles(tempDestDir, "*.trn"))
      {
        File.Delete(path);
      }
      // copy the save file to the save game directory (and rename appropriately)
      tm.Export(turnFile, tempDestDir);

      process = new Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Program.SettingsManager.GameExePath;
      process.StartInfo.Arguments = tempGameName;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

      // Watch for when process finishes
      process.EnableRaisingEvents = true;
      process.Exited += process_Exited;

      process.Start();
    }


    private void process_Exited(object sender, System.EventArgs e)
    {
      if(process.ExitCode == 0)
      {
        // Success
        string currentResult = Path.Combine(new string[] {
          tempDestDir,
          sourceFile.RaceName + "." + TurnManager.GameFile.ToExtension(TurnManager.GameFile.FileType.Result)
        });

        // Import the result into the turn manager
        if (File.Exists(currentResult))
        {
          turnManager.Import(gameName, currentResult, sourceFile.TurnNumber);
        }
      }

      process.Exited -= process_Exited;
    }
  }


}
