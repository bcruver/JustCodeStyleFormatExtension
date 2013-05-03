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

        public bool CheckWhiteSpaceAroundCharacter(string s, string itemCheck)
        {
            var warningCheck = NeedWarningWhiteSpaceBeforeCharacter(s, itemCheck);
            if (warningCheck != true)
            {
               warningCheck = NeedWarningForSingleWhiteSpaceAfterCharacter(s, itemCheck);
            }

            return warningCheck;
        }

        public bool CheckNoWhiteSpaceAroundCharacter(string s, string itemCheck)
        {           
            var warningCheck = NeedWarningForSingleWhiteSpaceAfterCharacter(s, itemCheck);
         
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
