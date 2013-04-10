namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Linq;
    using JustCodeStyleFormatExtension.Helpers.Interface;

    public class WhiteSpaceHelper : ISpacingHelper
    {
        private readonly WhiteSpaceKeyWordWarningHelper keywordWarning = new WhiteSpaceKeyWordWarningHelper();
        private readonly WhiteSpaceKeyWordCleanerHelper keywordCleaner = new WhiteSpaceKeyWordCleanerHelper();

        public bool CheckWhiteSpaceAroundKeyword(string s, string keywordCheck)
        {
            var warningCheck = keywordWarning.NeedWarningWhiteSpaceBeforeKeyword(s, keywordCheck);
            if (warningCheck != true)
            {
                warningCheck = keywordWarning.NeedWarningForSingleWhiteSpaceAfterKeyword(s, keywordCheck);
            }

            return warningCheck;
        }

        public string RemoveWhiteSpaceAroundKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            returnString = keywordCleaner.RemoveAllDoubleSpacesOnString(s);
            var warningCheck = keywordWarning.NeedWarningWhiteSpaceBeforeKeyword(returnString, keywordCheck);
            if (warningCheck == true)
            {
                returnString = keywordCleaner.RemoveAddExtraWhiteSpaceBeforeKeyword(returnString, keywordCheck);
            }

            warningCheck = keywordWarning.NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, keywordCheck);

            if (warningCheck == true)
            {
                returnString = keywordCleaner.RemoveAddSingleWhiteSpaceAfterKeyword(s, keywordCheck);
            }
            return returnString;
        }
    }
}
