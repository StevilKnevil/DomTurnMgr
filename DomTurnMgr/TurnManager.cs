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

      public GameFile(string gameName, string filePath, int turnNumber)
      {
        GameName = gameName;
        RaceName = Path.GetFileNameWithoutExtension(filePath);
        Type = FromExtension(Path.GetExtension(filePath));
        TurnNumber = turnNumber;
      }

      public string GameName{ get; }
      public string RaceName { get; }
      public FileType Type { get; }
      public int TurnNumber { get; }

      public string Extension => ToExtension(Type);

      public static FileType FromExtension(string extn)
      {
        if (String.Compare(extn, ".trn", true) == 0) return FileType.Turn;
        if (String.Compare(extn, ".2h", true) == 0) return FileType.Result;
        return FileType.Unknown;
      }

      public static string ToExtension(FileType t)
      {
        switch(t)
        {
          case FileType.Turn: return ".trn";
          case FileType.Result: return ".2h";
        }
        return "Unknown";
      }

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
      return GameNames.Contains(gameName);
    }

    public bool AddGame(string gameName)
    {
      return Directory.CreateDirectory(Path.Combine(LibraryDir, gameName)).Exists;
    }

    private string GetPathForFile(GameFile file)
    {
      string filename = file.RaceName + "_" + file.TurnNumber + "." + file.Extension;
      return Path.Combine(new string[] { LibraryDir, file.GameName , filename });
    }

    public GameFile Import(string gameName, string turnPath, int turnNumber)
    {
      if (!GameExists(gameName))
      {
        AddGame(gameName);
      }

      GameFile file = new GameFile(gameName, turnPath, turnNumber);

      // todo: compare files
      string destFilePath = GetPathForFile(file);
      if (File.Exists(destFilePath))
      {
        File.Delete(destFilePath);
      }
      File.Copy(turnPath, destFilePath);

      // Call the event handlers
      if (FileAdded!= null)
        FileAdded(this, file);

      return file;
    }

    public string Export(GameFile file, string destDir)
    {
      string destFile = Path.Combine(
        destDir,
        file.RaceName + file.Extension);
      File.Copy(GetPathForFile(file), destFile);
      return destFile;
    }

  }
}
