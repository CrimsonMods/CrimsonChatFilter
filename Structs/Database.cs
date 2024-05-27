using CrimsonChatFilter.Utils;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CrimsonChatFilter.Structs;

public class Database
{
    private static Database INSTANCE;
    public string FilteredPath { get; set; }

    public Database(string _path, string _db)
    { 
        FilteredPath = Path.Combine(_path, $"{_db}.json");
    }

    public static void InitFiltered(string db)
    {
        var _path = Path.Combine(BepInEx.Paths.ConfigPath, "CrimsonChatFilter");
        INSTANCE = new Database(_path, db);

        if (File.Exists(INSTANCE.FilteredPath))
        {
            string _json = File.ReadAllText(INSTANCE.FilteredPath);
            FilterString.FilteredWords = JsonSerializer.Deserialize<List<string>>(_json);
        }
    }
}
