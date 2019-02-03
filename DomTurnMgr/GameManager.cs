using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class GameManager
  {
    private readonly string LibraryDir;
    public string GameName => Path.GetFileName(LibraryDir);

    public GameManager(string libraryDir)
    {
      LibraryDir = libraryDir;
      Directory.CreateDirectory(LibraryDir);
    }

    public class GameTurn
    {
      public string RaceName;
      public int TurnNumber;

      public GameTurn(string raceName, int turnNumber)
      {
        RaceName = raceName;
        TurnNumber = turnNumber;
      }

      public GameTurn(string filePath)
      {
        using (var fs = new FileStream(filePath, FileMode.Open))
        using (var reader = new BinaryReader(fs))
        {
          RaceName = GetNationName(reader);
          TurnNumber = GetTurnNumber(reader);
        }
      }

      public GameTurn(BinaryReader reader)
      {
        RaceName = GetNationName(reader);
        TurnNumber = GetTurnNumber(reader);
      }

      public string ToPath() => Path.Combine(new string[] {
        RaceName,
        TurnNumber.ToString()
      });

      private static string GetNationName(BinaryReader reader)
      {
        // Instead get offset 0x1a to get the race ID
        byte[] test = new byte[1];
        reader.BaseStream.Seek(0x1A, SeekOrigin.Begin);
        reader.Read(test, 0, 1);
        int nationID = test[0];
        return NationDetails.Get(nationID).Filename;
      }

      private static int GetTurnNumber(BinaryReader reader)
      {
        byte[] test = new byte[1];
        reader.BaseStream.Seek(0xE, SeekOrigin.Begin);
        reader.Read(test, 0, 1);
        int turnNum = test[0];
        return turnNum;
      }

    }

    public event EventHandler TurnsChanged;

    public IEnumerable<string> GetRaceNames()
    {
      List<string> dirs = new List<string>();
      var paths = Directory.EnumerateDirectories(LibraryDir);
      foreach (var path in paths)
      {
        dirs.Add(Path.GetFileName(path));
      }
      return dirs;
    }

    public IEnumerable<int> GetTurnNumbers(string raceName)
    {
      List<int> dirs = new List<int>();
      var paths = Directory.EnumerateDirectories(Path.Combine(LibraryDir, raceName));
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
      string destFilePath = Path.Combine(destDir, gameTurn.RaceName + Path.GetExtension(sourceFilePath));
      if (File.Exists(destFilePath))
      {
        File.Delete(destFilePath);
      }

      File.Copy(sourceFilePath, destFilePath);

      // Call the event handlers
      TurnsChanged?.Invoke(this, new EventArgs());
    }

    public void Import(MemoryStream sourceStream, string extension)
    {
      if (extension != ".trn" && extension != ".2h")
        throw new ArgumentException("Expected .trn or .2h");

      using (BinaryReader reader = new BinaryReader(sourceStream))
      {
        var gt = new GameTurn(reader);
        // Ensure the correct directory is created
        string destDir = Path.Combine(LibraryDir, gt.ToPath());
        Directory.CreateDirectory(destDir);

        // todo: compare files timestamps etc
        string destFilePath = Path.Combine(destDir, gt.RaceName + extension);
        if (File.Exists(destFilePath))
        {
          File.Delete(destFilePath);
        }

        // Write the file to the stream
        sourceStream.WriteTo(File.Create(destFilePath));
      }

      // Call the event handlers
      TurnsChanged?.Invoke(this, new EventArgs());
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
