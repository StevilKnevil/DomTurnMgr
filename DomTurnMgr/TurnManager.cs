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

    public enum FileType
    {
      Unknown,
      Turn,
      Result,
    }

    public class FileChangedEventArgs : EventArgs
    {
      public FileChangedEventArgs(FileType type, string gameName, string raceName, int turnNumber)
      {
        Type = type;
        GameName = gameName;
        RaceName = raceName;
        TurnNumber = turnNumber;
      }

      public FileType Type { get; private set; }
      public string GameName { get; private set; }
      public string RaceName { get; private set; }
      public int TurnNumber { get; private set; }
    }

    public event EventHandler<FileChangedEventArgs> FileAdded;

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

      string extension = Path.GetExtension(turnPath);
      string raceName = Path.GetFileNameWithoutExtension(turnPath);
      string destFilename = raceName + "_" + turnNumber + "." + extension;
      string destPath = Path.Combine(LibraryDir, destFilename);
      File.Copy(turnPath, destPath);

      FileType type = FileType.Unknown;
      if (extension == "trn")
      {
        type = FileType.Turn;
      }
      if (extension == "2h")
      {
        type = FileType.Result;
      }
      FileChangedEventArgs args = new FileChangedEventArgs(type, gameName, raceName, turnNumber);

      FileAdded(this, args);

      return true;
    }

  }
}
