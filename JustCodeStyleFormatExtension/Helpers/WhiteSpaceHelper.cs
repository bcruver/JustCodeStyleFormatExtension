namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JustCodeStyleFormatExtension.Extensions;

    public class WhiteSpaceHelper
    {
        public bool NeedWarningForSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            var startPoint = s.IndexOf(keywordCheck) + keywordCheck.Length;            
            var whiteSpaceCount = 0;
            var characterAfterKeyword = 0;
            char nextCharacter = char.MinValue;
            var endPoint = s.Length - 1;
            var exception = "new[";

            if (startPoint < endPoint)
            {
                for (int i = startPoint; i < endPoint; i++)
                {
                    if (characterAfterKeyword == 0)
                    {
                        if (!char.IsWhiteSpace(s[i]))
                        {
                            characterAfterKeyword++;
                            nextCharacter = s[i];
                        }
                        else
                        {
                            whiteSpaceCount++;
                            if (whiteSpaceCount > 1)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (keywordCheck + nextCharacter.ToString() == exception)
                        {
                            return false;
                        }
                        return (whiteSpaceCount == 1) ? false : true;
                    }
                }
            }

            return true;
        }

        public bool NeedWarningWhiteSpaceBeforeKeyword(string s, string keywordCheck)
        {
            var startPointIndex = s.IndexesOf(keywordCheck);
            var whiteSpaceCount = 0;
            var characterAfterKeyword = 0;

            foreach (var startPoint in startPointIndex)
            {
                if (startPoint > 0)
                {
                    for (int i = startPoint - 1; i > 0; i--)
                    {
                        if (characterAfterKeyword == 0)
                        {
                            if (!char.IsWhiteSpace(s[i]))
                            {
                                characterAfterKeyword++;
                            }
                            else
                            {
                                whiteSpaceCount++;
                                if (whiteSpaceCount > 1)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            return (whiteSpaceCount == 1) ? false : true;
                        }
                    }
                }
            }
            return true;
        }

        public string RemoveAddSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            returnString = this.RemoveAllDoubleSpacesOnString(s);
            var warningCheck = this.NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, keywordCheck);

            if (warningCheck == true)
            {
                int index2 = returnString.IndexOf(keywordCheck);
                returnString = returnString.Insert(index2 + keywordCheck.Length, " ");
            }

            return returnString;
        }

        private string RemoveAllDoubleSpacesOnString(string s)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            return regex.Replace(s, @" ");
        }
    }
}
