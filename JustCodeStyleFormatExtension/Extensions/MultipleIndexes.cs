namespace JustCodeStyleFormatExtension.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MultipleIndexes
    {
        static int i = 0;

        public static int[] MultipleIndex(this string stringValue, char chChar)
        {
            var indexs = from rgChar in stringValue
                         where rgChar == chChar && i != stringValue.IndexOf(rgChar, i + 1)
                         select new { Index = stringValue.IndexOf(rgChar, i + 1), Increament = (i = i + stringValue.IndexOf(rgChar)) };
            i = 0;
            return indexs.Select(p => p.Index).ToArray<int>();
        }

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
    }
}
