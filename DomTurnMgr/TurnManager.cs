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

    public class GameFile
    {
      public enum FileType
      {
        Unknown,
        Turn,
        Result,
      }

      public GameFile(string filePath, int turnNumber)
      {
        RaceName = Path.GetFileNameWithoutExtension(filePath);
        Type = (FileType)Enum.Parse(
          typeof(FileType), 
          Path.GetExtension(filePath), 
          false);
        TurnNumber = turnNumber;
      }

      //public string FilePath{ get; }
      public string RaceName { get; }
      public FileType Type { get; }
      public int TurnNumber { get; }

      public string Extension => Type.ToString();
    }

    public event EventHandler<GameFile> FileAdded;

    public IEnumerable<string> GameNames
    {
      get {
        List<string> dirs = new List<string>();
        var paths = Directory.EnumerateDirectories(LibraryDir);
        foreach (var path in paths)
        {
          dirs.Add(Path.GetFileName(path));
        }
        return dirs;
      }
    } 

    public bool GameExists(string gameName)
    {
      return GameNames.Contains(Path.Combine(LibraryDir, gameName));
    }

    public bool AddGame(string gameName)
    {
      return Directory.CreateDirectory(Path.Combine(LibraryDir, gameName)).Exists;
    }

    public bool AddTurn(string gameName, string turnPath, int turnNumber)
    {
      if (!GameExists(gameName))
      {
        AddGame(gameName);
      }

      GameFile file = new GameFile(turnPath, turnNumber);
      string destFilename = file.RaceName + "_" + file.TurnNumber + "." + file.Extension;
      string destPath = Path.Combine(LibraryDir, destFilename);
      File.Copy(turnPath, destPath);

      FileAdded(this, file);

      return true;
    }

  }
}
