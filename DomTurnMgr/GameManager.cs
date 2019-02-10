using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class GameManager : IDisposable
  {
    public static IDictionary<string, GameManager> GameManagers = new Dictionary<string, GameManager>();
    public static EventHandler<GameManager> GameManagersChanged;

    // Flag: Has Dispose already been called?
    private bool disposed = false;

    public string LibraryDir { get; }
    private GameSettings gameSettings;
    private IMAPMailWatcher mailWatcher;
    public string GameName => gameSettings.Name;
    public string UserMailAccount => gameSettings.MailServerConfig.Username;
    public string ServerMailAccount => gameSettings.MailConfig.ServerMailAccount;
    public string ServerUrl => gameSettings.GameServerCfg.GameServerAddress;
    public MailServerConfig MailServerConfig => gameSettings.MailServerConfig;

    public GameManager(GameSettings gs, string libraryDir)
    {
      gameSettings = gs;

      LibraryDir = libraryDir;
      Directory.CreateDirectory(LibraryDir);

      var query =
        MailKit.Search.SearchQuery.SubjectContains(gs.MailConfig.SubjectSearchString).And(
          MailKit.Search.SearchQuery.FromContains(gs.MailConfig.ServerMailAccount));

      var mailConfig = MailServerConfig.MailServerConfigs[gameSettings.MailServerConfigName];
      mailWatcher = new IMAPMailWatcher(mailConfig, query);
      mailWatcher.AttachmentsAvailable += MailWatcher_AttachmentsAvailable;

      GameManagers[this.GameName] = this;
      GameManagersChanged?.Invoke(this, this);
    }

    ~GameManager()
    {
      Dispose(false);
    }

    #region IDisposable
    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
        return;

      if (disposing)
      {
        mailWatcher.Dispose();
      }

      // Free any unmanaged objects here.
      //
      disposed = true;
    }
    #endregion IDisposable

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
      string nationPath = Path.Combine(LibraryDir, raceName);
      if (Directory.Exists(nationPath))
      {
        var paths = Directory.EnumerateDirectories(nationPath);
        foreach (var path in paths)
        {
          string name = Path.GetFileName(path);
          int num = 0;
          if (int.TryParse(name, out num))
          {
            dirs.Add(num);
          }
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
        using (var outStream = File.Create(destFilePath))
        {
          sourceStream.WriteTo(outStream);
        }
      }

      // Call the event handlers
      TurnsChanged?.Invoke(this, new EventArgs());
    }

    public void Export(GameTurn gameTurn, string destDir)
    {
      // Export all files from library
      string sourceDir = Path.Combine(LibraryDir, gameTurn.ToPath());

      string[] fileList = Directory.GetFiles(sourceDir, "*.*");

      // Copy files.
      foreach (string f in fileList)
      {
        File.Copy(f, Path.Combine(destDir, Path.GetFileName(f)));
      }
    }

    private int GetTurnNumberFromSubject(string subject)
    {
      int turnNumber = 0;

      string turnIndexString = System.Text.RegularExpressions.Regex.Match(subject, @"\d+$").Value;
      if (!int.TryParse(turnIndexString, out turnNumber))
      {
        // perhaps this is the first turn
        if (System.Text.RegularExpressions.Regex.Match(subject, @"First turn attached$").Success)
        {
          turnNumber = 1;
        }
      }
      return turnNumber;
    }

    public string[] GetFilesForTurn(GameTurn gameTurn, string searchString = "*")
    {
      string[] retval = { };
      if (GetTurnNumbers(gameTurn.RaceName).Contains(gameTurn.TurnNumber))
      {
        string sourceDir = Path.Combine(LibraryDir, gameTurn.ToPath());
        return Directory.GetFiles(sourceDir, searchString);
      }
      return retval;
    }

    private bool FileExistsInLibrary(IMAPMailWatcher.MessageAttachment ma)
    {
      int turnNumber = GetTurnNumberFromSubject(ma.Subject);
      string raceName = Path.GetFileNameWithoutExtension(ma.Filename);
      var gameTurn = new GameTurn(raceName, turnNumber);
      var files = GetFilesForTurn(gameTurn);
      if (files.Contains(ma.Filename))
      {
        return true;
      }
      return false;
    }


    private void MailWatcher_AttachmentsAvailable(object sender, IMAPMailWatcher.MessageAttachment ma)
    {
      var ext = Path.GetExtension(ma.Filename);
      if (String.Compare(ext, ".trn", true) == 0 ||
        String.Compare(ext, ".2h", true) == 0)
      {
        if (!FileExistsInLibrary(ma))
        {
          // This is an attachment of interest
          using (MemoryStream s = ma.CreateMemoryStream())
          {
            Import(s, ext);
          }
        }
      }
    }

  }
}
