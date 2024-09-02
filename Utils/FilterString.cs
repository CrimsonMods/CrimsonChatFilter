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
            string pattern = CreateRegexPattern(word);
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
            string pattern = CreateRegexPattern(word);
            if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase)) return true;
        }

        return false;
    }

    private static string CreateRegexPattern(string word)
    {
        string escapedWord = Regex.Escape(word).Replace(@"\*", ".*");
        return $@"\b{escapedWord}\b";
    }
}