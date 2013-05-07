namespace StyleFormatEngine.Helpers.Interface
{
    using System;
    using System.Linq;

    public interface INoSpaceHelper
    {
        bool CheckNoWhiteSpaceAroundCharacter(string s, string commentType);

        string RemoveWhiteSpaceAroundKeyword(string s, string keywordCheck);
    }
}
