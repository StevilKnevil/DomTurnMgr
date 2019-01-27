using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class GameLauncher
  {
    void Launch(string gameName, TurnManager.GameFile saveFile)
    {
      // copy the save file to the save game directory (and rename appropriately)
      string destFile = Path.Combine(
        Program.SettingsManager.SaveGameDirectory, 
        saveFile.RaceName + "." + saveFile.Extension);
      File.Copy(saveFile.FilePath, destFile);

      System.Diagnostics.Process process = new System.Diagnostics.Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Program.SettingsManager.GameExePath;
      // Extract the game name from the text on the parent tab page
      process.StartInfo.Arguments = gameName;
      process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
      process.Start();
    }
  }


}
