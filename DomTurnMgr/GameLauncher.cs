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
    private string tempGameName { get; }
    public string tempGameDir => Path.Combine(
          Program.SettingsManager.SaveGameDirectory,
          tempGameName);

    public GameLauncher()
    {
      tempGameName = Guid.NewGuid().ToString();
      Directory.CreateDirectory(tempGameDir);
    }

    ~GameLauncher()
    {
      if (Directory.Exists(tempGameDir))
        Directory.Delete(tempGameDir, true);
    }

    public Task<string> LaunchAsync()
    {
      return Task.Run<string>(() =>
      {
        Process process = new Process();
        // Configure the process using the StartInfo properties.
        process.StartInfo.FileName = Program.SettingsManager.GameExePath;
        process.StartInfo.Arguments = $"{tempGameName} --window";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

        process.Start();
        process.WaitForExit();

        string resultFilePath = null;
        if (process.ExitCode == 0)
        {
          var files = Directory.EnumerateFiles(tempGameDir, "*.2h");
          if (files.Count() == 1)
          {
            resultFilePath = files.First();
          }
          else if (files.Count() > 1)
          {
            throw new Exception();
          }
        }

        return resultFilePath;
      });
    }

  }


}
