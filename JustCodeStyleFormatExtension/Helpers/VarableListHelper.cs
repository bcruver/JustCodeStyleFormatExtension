namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VarableListHelper
    {
        public List<string> GetKeyWordList()
        {
            List<string> keyWords = new List<string>
            {
                "catch", "fixed", "for", "foreach", "from", "group", "if", "in", "into", "join",
                "let", "lock", "orderby", "return", "select", "stackalloc", "switch", "throw", "using",
                "where", "while", "yield", "=", ","
            };
            return keyWords;
        }
    }
}
