namespace StyleFormatEngine.Helpers.Interface
{
    using System;
    using System.Linq;

    public interface ISpacingHelper
    {
        bool CheckWhiteSpaceAroundKeyword(string s, string keywordCheck);

        bool CheckWhiteSpaceAroundCharacter(string s, string commentType);        

        string RemoveWhiteSpaceAroundKeyword(string s, string keywordCheck);

        string RemoveWhiteSpaceAroundComment(string s, string commentType);
    }
}
