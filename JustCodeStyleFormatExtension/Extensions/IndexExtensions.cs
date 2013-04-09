namespace JustCodeStyleFormatExtension.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class IndexExtensions
    {
        public static IEnumerable<int> IndexesOf(this string haystack, string needle)
        {
            int lastIndex = 0;
            while (true)
            {
                int index = haystack.IndexOf(needle, lastIndex);
                if (index == -1)
                {
                    yield break;
                }
                yield return index;
                lastIndex = index + needle.Length;
            }
        }

        public static int WholeWordIndexOf(this string source, string key, bool ignoreCase = true)
        {
            string testValue = @"\b(" + key + @"\b)";

            var regex = new Regex(testValue, ignoreCase ?
                   RegexOptions.IgnoreCase :
                   RegexOptions.None);

            var match = regex.Match(source);
            return match.Captures.Count == 0 ? -1 : match.Groups[0].Index;
        }

        public static IEnumerable<int> WholeWordIndexesOf(this string source, string key, bool ignoreCase = true)
        {
            List<int> returnList = new List<int>();

            string testValue = @"\b(" + key + @"\b)";

            var regex = new Regex(testValue, ignoreCase ?
                   RegexOptions.IgnoreCase :
                   RegexOptions.None);

            var match = regex.Match(source);

            if(match.Captures.Count == 0)
            {
                returnList.Add(-1);
                return returnList;
            }

            for (int i = 0; i < match.Captures.Count; i++)
            {
                returnList.Add(match.Groups[i].Index);
            }

            return returnList;
        }
    }
}
