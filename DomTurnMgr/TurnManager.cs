using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class TurnManager
  {
    public TurnManager(string libraryDir)
    {
      LibraryDir = libraryDir;
      Directory.CreateDirectory(LibraryDir);
    }

    private readonly string LibraryDir;

    public class GameTurn
    {
      public string GameName;
      public string RaceName;
      public int TurnNumber;

      public GameTurn(string gameName, string raceName, int turnNumber)
      {
        GameName = gameName;
        RaceName = raceName;
        TurnNumber = turnNumber;
      }

      public string ToPath() => Path.Combine(new string[] {
        GameName,
        RaceName,
        TurnNumber.ToString()
      });

      /*
      public static GameTurn CreateFromPath()
      {
        return new GameTurn();
      }
      */
    }

    //public event EventHandler<GameFile> FileAdded;

    public IEnumerable<string> GetGameNames()
    {
      List<string> dirs = new List<string>();
      var paths = Directory.EnumerateDirectories(LibraryDir);
      foreach (var path in paths)
      {
        dirs.Add(Path.GetFileName(path));
      }
      return dirs;
    }

    public IEnumerable<string> GetRaceNames(string gameName)
    {
      List<string> dirs = new List<string>();
      var paths = Directory.EnumerateDirectories(Path.Combine(LibraryDir, gameName));
      foreach (var path in paths)
      {
        dirs.Add(Path.GetFileName(path));
      }
      return dirs;
    }

    public IEnumerable<int> GetTurnNumbers(string gameName, string raceName)
    {
      List<int> dirs = new List<int>();
      var paths = Directory.EnumerateDirectories(Path.Combine(new string[] {LibraryDir, gameName, raceName}));
      foreach (var path in paths)
      {
        string name = Path.GetFileName(path);
        int num = 0;
        if (int.TryParse(name, out num))
        {
          dirs.Add(num);
        }
      }
      return dirs;
    }

    public void Import(string sourceFilePath, GameTurn gameTurn)
    {
      if (Path.GetExtension(sourceFilePath) != ".trn" && Path.GetExtension(sourceFilePath) != ".2h")
        throw new ArgumentException("Expected .trn or .2h");

      // Ensure the correct directory is created
      string destDir = Path.Combine(LibraryDir, gameTurn.ToPath());
      Directory.CreateDirectory(destDir);

      // todo: compare files timestamps etc
      string destFilePath = Path.Combine(destDir, Path.GetFileName(sourceFilePath));
      if (File.Exists(destFilePath))
      {
        File.Delete(destFilePath);
      }

      File.Copy(sourceFilePath, destFilePath);

      // Call the event handlers
      //if (FileAdded!= null)
      //  FileAdded(this, file);
    }

    public void Export(GameTurn gameTurn, string destDir)
    {
      // Export all files from library
      string sourceDir = Path.Combine(LibraryDir, gameTurn.ToPath());

      string[] fileList = Directory.GetFiles(sourceDir, "*.*");

      // Copy picture files.
      foreach (string f in fileList)
      {
        File.Copy(f, Path.Combine(destDir, Path.GetFileName(f)));
      }
    }

  }
}
