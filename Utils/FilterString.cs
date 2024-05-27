using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrimsonChatFilter.Utils;

internal static class FilterString
{
    public static List<string> FilteredWords;

    public static string Filter(this string input)
    {
        foreach (var word in FilteredWords)
        { 
            string pattern = $@"\b{Regex.Escape(word)}\b";
            input = Regex.Replace(input, pattern, "****", RegexOptions.IgnoreCase);
        }
        return input;
    }

    public static bool ContainsFiltered(this string input)
    {
        foreach (var word in FilteredWords)
        { 
            if(input.Contains(word)) return true;
        }

        return false;
    }
}
