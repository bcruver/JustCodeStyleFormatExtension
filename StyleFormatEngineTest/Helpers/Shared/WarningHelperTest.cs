namespace StyleFormatEngineTest.Helpers.Shared
{
    using System;
    using System.Collections.Generic;
    using StyleFormatEngine.Helpers.Spacing.Shared;
    using Xunit;
    using Xunit.Extensions;

    // True means Warning is displayed, false means no warning
    public class WarningHelperTest
    {
        public WarningHelper WarningHelper { get; set; }

        public WarningHelperTest()
        {
            this.WarningHelper = new WarningHelper();
        }

        #region comment test

        [Fact]
        public void CommentSingleThreeSlashNoWarningNeeedTest()
        {
            // this exception is due to Telerik seeing this as a comment in JavaScript
            string s = "/// <reference path=\"jquery-ui-1.10.2.js\" />";
            List<int> startIndexes = new List<int>();
            startIndexes.Add(0);
            string keywordCheck = "//";

            Assert.False(this.WarningHelper.IsWarningNeededAfter(s, startIndexes, keywordCheck));            
        }

        [Fact]
        public void CommentCSharpDoubleSlashNoWarningNeeedTest()
        {
            string s = "// lakjdkfjasdf";
            List<int> startIndexes = new List<int>(){0};
            string keywordCheck = "//";

            Assert.False(this.WarningHelper.IsWarningNeededAfter(s, startIndexes, keywordCheck));
        }

        [Fact]
        public void CommentCSharpDoubleSlashWarningNeeedTest()
        {
            string s = "//lakjdkfjasdf";
            List<int> startIndexes = new List<int>(){0};
            string keywordCheck = "//";

            Assert.True(this.WarningHelper.IsWarningNeededAfter(s, startIndexes, keywordCheck));
        }

        [Fact]
        public void CommentCSharpDoubleSlashMultiSpaceWarningNeeedTest()
        {
            string s = "//    lakjdkfjasdf";
            List<int> startIndexes = new List<int>() { 0 };
            string keywordCheck = "//";

            Assert.False(this.WarningHelper.IsWarningNeededAfter(s, startIndexes, keywordCheck));
        }

        #endregion

        #region Keyword
       // [Fact]
        // public void CommentParenthiseWarningNeeedTest()
        //{
        //    string s = "//incorrect\r\n \r\n for(int i = 0; i < 10; i++)\r\n {\r\n\t\t\tList<string> keywordSearch = new List<string> { \"for\", \"foreach\", \"in\" };\r\n var test = i;\r\n }";
        //    List<int> startIndexes = new List<int>(){42};
        //    string keywordCheck = "(";

        //    Assert.False(this.WarningHelper.IsWarningNeededAfterForNoSpace(s, startIndexes, keywordCheck));
        //}


        #endregion


    }
}
