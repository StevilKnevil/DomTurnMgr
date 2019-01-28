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
    private string turnFilePath;
    private int turnNumber;

    private string tempGameName => System.Windows.Forms.Application.ProductName;
    public string tempDestDir => Path.Combine(
          Program.SettingsManager.SaveGameDirectory,
          tempGameName);

    public GameLauncher()
    {
      // TODO: This should be re-entrant - GUID for the game name? Wipe out in Dispose?
      // Empty all files from that directory
      Directory.CreateDirectory(tempDestDir);
      foreach (string path in Directory.EnumerateFiles(tempDestDir, "*.trn"))
      {
        File.Delete(path);
      }
    }

    public async Task<string> LaunchAsync(string turnFilePath)
    {
      //return await new Task<string>(() =>
      {
        if (Path.GetDirectoryName(turnFilePath) == tempGameName)
          throw new ArgumentException($"Turn file: ${turnFilePath} is not in game directory ${tempGameName}");

        process = new Process();
        // Configure the process using the StartInfo properties.
        process.StartInfo.FileName = Program.SettingsManager.GameExePath;
        process.StartInfo.Arguments = tempGameName;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

        // Watch for when process finishes
        process.EnableRaisingEvents = true;
        //      process.Exited += process_Exited;

        process.Start();
        process.WaitForExit();

        string resultFilePath = null;
        if (process.ExitCode == 0)
        {
          resultFilePath = Path.ChangeExtension(turnFilePath, ".2h");

          // Import the result into the turn manager
          if (!File.Exists(resultFilePath))
          {
            resultFilePath = null;
          }
        }

        return resultFilePath;
      }
      //});
    }

  }


}
