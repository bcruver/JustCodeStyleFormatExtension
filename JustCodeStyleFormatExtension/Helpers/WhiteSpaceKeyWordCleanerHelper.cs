namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JustCodeStyleFormatExtension.Extensions;

    public class WhiteSpaceKeyWordCleanerHelper
    {
        private readonly WhiteSpaceKeyWordWarningHelper keywordWarning = new WhiteSpaceKeyWordWarningHelper();

        public string RemoveAddExtraWhiteSpaceBeforeKeyword(string s, string keywordCheck)
        {
            string returnString = s;
           
            returnString = AddSingleWhiteSpaceBeforeKeyword(keywordCheck, returnString);
            return returnString;
        }        

        public string RemoveAddSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            var warningCheck = keywordWarning.NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, keywordCheck);
            if (warningCheck == true)
            {
                returnString = this.AddSingleWhiteSpaceAfterKeyword(keywordCheck, returnString);
            }

            return returnString;
        }

        private string AddSingleWhiteSpaceBeforeKeyword(string keywordCheck, string returnString)
        {
            var startPointIndex = returnString.IndexesOf(keywordCheck);

            foreach (var startPoint in startPointIndex)
            {
                if (startPoint - 1 > 0)
                {
                    returnString = returnString.Insert(startPoint - 1, " ");
                }
            }
            return returnString;
        }

        private string AddSingleWhiteSpaceAfterKeyword(string keywordCheck, string returnString)
        {
            int index2 = returnString.IndexOf(keywordCheck);
            var exceptionCheck = returnString.Substring(index2, 1);
            if (exceptionCheck != "new[")
            {
                returnString = returnString.Insert(index2 + keywordCheck.Length, " ");
            }
            return returnString;
        }

        public string RemoveAllDoubleSpacesOnString(string s)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            return regex.Replace(s, @" ");
        }
    }
}