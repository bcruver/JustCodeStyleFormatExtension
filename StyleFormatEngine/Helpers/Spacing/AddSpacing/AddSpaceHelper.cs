namespace StyleFormatEngine.Helpers.Spacing.AddSpacing
{
    using System;
    using System.Linq;
    using StyleFormatEngine.Helpers.Spacing.Shared;

    public class AddSpaceHelper : CleanerHelper, StyleFormatEngine.Helpers.Interface.ISpacingHelper
    {
        public bool CheckWhiteSpaceAroundKeyword(string s, string itemCheck)
        {
            var warningCheck = NeedWarningWhiteSpaceBeforeKeyword(s, itemCheck);
            if (warningCheck != true)
            {
                warningCheck = SingleSpaceAfterKeyword(s, itemCheck);
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
            var returnValue = NeedWarningForSingleWhiteSpaceAfterCharacter(s, itemCheck);
            return returnValue ? false : returnValue;
        }

        public string RemoveWhiteSpaceAroundKeyword(string s, string itemCheck)
        {
            string returnString = s;
            returnString = RemoveAllDoubleSpacesOnString(s);
            var warningCheck = NeedWarningWhiteSpaceBeforeKeyword(returnString, itemCheck);
            if (warningCheck == true)
            {
                returnString = SpacingBeforeKeyword(returnString, itemCheck);
            }

            warningCheck = SingleSpaceAfterKeyword(returnString, itemCheck);

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
