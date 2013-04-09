namespace JustCodeStyleFormatExtension.Helpers.Interface
{
    using System;
    using System.Linq;

    public interface ISpacingHelper
    {
        bool CheckWhiteSpaceAroundKeyword(string s, string keywordCheck);

        string RemoveWhiteSpaceAroundKeyword(string s, string keywordCheck);
    }
}
