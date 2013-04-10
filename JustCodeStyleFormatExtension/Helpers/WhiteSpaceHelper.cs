namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Linq;
    using JustCodeStyleFormatExtension.Helpers.Interface;

    public class WhiteSpaceHelper : WhiteSpaceKeyWordCleanerHelper, ISpacingHelper
    {
        public bool CheckWhiteSpaceAroundKeyword(string s, string itemCheck)
        {
            var warningCheck = NeedWarningWhiteSpaceBeforeKeyword(s, itemCheck);
            if (warningCheck != true)
            {
                warningCheck = NeedWarningForSingleWhiteSpaceAfterKeyword(s, itemCheck);
            }

            return warningCheck;
        }

        public bool CheckWhiteSpaceAroundComment(string s, string itemCheck)
        {
            var warningCheck = NeedWarningWhiteSpaceBeforeComment(s, itemCheck);
            if (warningCheck != true)
            {
               warningCheck = NeedWarningForSingleWhiteSpaceAfterComment(s, itemCheck);
            }

            return warningCheck;
        }

        public string RemoveWhiteSpaceAroundKeyword(string s, string itemCheck)
        {
            string returnString = s;
            returnString = RemoveAllDoubleSpacesOnString(s);
            var warningCheck = NeedWarningWhiteSpaceBeforeKeyword(returnString, itemCheck);
            if (warningCheck == true)
            {
                returnString = RemoveAddExtraWhiteSpaceBeforeKeyword(returnString, itemCheck);
            }

            warningCheck = NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, itemCheck);

            if (warningCheck == true)
            {
                returnString = RemoveAddSingleWhiteSpaceAfterKeyword(s, itemCheck);
            }
            return returnString;
        }

        public string RemoveWhiteSpaceAroundComment(string s, string itemCheck)
        {
            throw new NotImplementedException();
        }
    }
}
