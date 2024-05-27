using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrimsonChatFilter.Utils;

internal static class FilterString
{
    public static List<string> FilteredWords;

    private static string addressFilter = @".[0-9]+[0-9][0-9].[0-9]+[0-9][0-9].[0-9]+[0-9][0-9].[0-9]+[0-9][0-9]*";
    private static string domainFilter = @"\bhttp:\/\/([^\/]*)\/([^\s]*)";

    public static string Filter(this string input)
    {
        if (Plugin.Settings.GetActiveOption(Structs.Settings.Options.FilterUrl))
        {
            input = Regex.Replace(input, addressFilter, "****", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, domainFilter, "****", RegexOptions.IgnoreCase);
        }

        foreach (var word in FilteredWords)
        { 
            string pattern = $@"\b{Regex.Escape(word)}\b";
            input = Regex.Replace(input, pattern, "****", RegexOptions.IgnoreCase);
        }
        return input;
    }

    public static bool ContainsFiltered(this string input)
    {
        if (Plugin.Settings.GetActiveOption(Structs.Settings.Options.FilterUrl))
        {
            if (Regex.IsMatch(input, addressFilter)) return true;
            if (Regex.IsMatch(input, domainFilter)) return true;
        }

        foreach (var word in FilteredWords)
        { 
            if(input.Contains(word)) return true;
        }

        return false;
    }
}
