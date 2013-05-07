namespace StyleFormatEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Library
    {        
        public List<string> Exceptions { get; set; }

        public List<string> Keywords { get; set; }

        public List<string> CommentKeys { get; set; }

        public Library()
        {
            this.Exceptions = new List<string>() { "new[ ", "///" };

            this.Keywords = new List<string>() { "catch", "for", "foreach", "in", "new", "switch", "("};

            this.CommentKeys = new List<string>() {"//", "/*", "'" };

        }
    }
}
