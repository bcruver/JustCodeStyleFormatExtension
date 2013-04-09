namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JustCodeStyleFormatExtension.Extensions;
    using JustCodeStyleFormatExtension.Helpers.Interface;

    public class WhiteSpaceHelper : ISpacingHelper
    {
        public bool CheckWhiteSpaceAroundKeyword(string s, string keywordCheck)
        {
            var warningCheck = this.NeedWarningWhiteSpaceBeforeKeyword(s, keywordCheck);
            if (warningCheck != true)
            {
                warningCheck = this.NeedWarningForSingleWhiteSpaceAfterKeyword(s, keywordCheck);
            }

            return warningCheck;
        }

        public string RemoveWhiteSpaceAroundKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            returnString = this.RemoveAllDoubleSpacesOnString(s);
            var warningCheck = this.NeedWarningWhiteSpaceBeforeKeyword(returnString, keywordCheck);
            if (warningCheck == true)
            {
                returnString = RemoveAddExtraWhiteSpaceBeforeKeyword(returnString, keywordCheck);
            }

            warningCheck = this.NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, keywordCheck);

            if (warningCheck == true)
            {
                returnString = RemoveAddSingleWhiteSpaceAfterKeyword(s, keywordCheck);
            }
            return returnString;
        }

        private bool NeedWarningForSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            var startIndexes = s.WholeWordIndexesOf(keywordCheck);
            var whiteSpaceCount = 0;
            var characterAfterKeyword = 0;
            char nextCharacter = char.MinValue;
            var endPoint = s.Length - 1;
            var exception = "new[";

            foreach (var startPoint in startIndexes)
            {
                if (startPoint < endPoint)
                {
                    for (int i = startPoint + keywordCheck.Length; i < endPoint; i++)
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
            }
            return false;
        }

        // todo handle \n and \t
        private bool NeedWarningWhiteSpaceBeforeKeyword(string s, string keywordCheck)
        {
            var isAtBeginningOfRow = IsKeywordAtTheBeginningOfALine(s, keywordCheck);
            if (isAtBeginningOfRow == false)
            {
                var startIndexes = s.WholeWordIndexesOf(keywordCheck);
                var whiteSpaceCount = 0;
                char characterAfterKeyword = new char();

                foreach (var startPoint in startIndexes)
                {
                    if (startPoint > 0)
                    {
                        for (int i = startPoint - 1; i > 0; i--)
                        { 
                            if (characterAfterKeyword == '\0')
                            {
                                if (!char.IsWhiteSpace(s[i]))
                                {
                                    characterAfterKeyword = s[i];
                                }
                                else
                                {
                                    if (s[i] != '\t' && s[i] != '\n' && s[i] != '\r')
                                    {
                                        whiteSpaceCount++;
                                    }

                                    if (whiteSpaceCount > 1)
                                    {
                                        return true;
                                    }

                                    if ((s[i] == '\n' || s[i] == '\r') && whiteSpaceCount < 2)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                if (whiteSpaceCount == 1)
                                {
                                    return false;
                                }
                                else
                                {
                                    if (characterAfterKeyword == '\n')
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool IsKeywordAtTheBeginningOfALine(string s, string keyworkCheck)
        {
            var stringArray = s.Split('\n');
            var wordsInString = s.WholeWordIndexesOf(keyworkCheck);
            var counter = 0;

            foreach (var item in stringArray)
            {
                var stringValue = item.TrimStart();
                if (stringValue.WholeWordIndexOf(keyworkCheck) > 0)
                {
                    return false;
                }

                counter++;
                if (counter >= wordsInString.Count())
                {
                    break;
                }
            }
            return true;
        }

        private string RemoveAddExtraWhiteSpaceBeforeKeyword(string s, string keywordCheck)
        {
            string returnString = s;
           
            var startPointIndex = s.IndexesOf(keywordCheck);

            foreach (var startPoint in startPointIndex)
            {
                if (startPoint - 1 > 0)
                {
                    returnString = returnString.Insert(startPoint - 1, " ");
                }
            }
            return returnString;
        }

        private string RemoveAddSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            var warningCheck = this.NeedWarningForSingleWhiteSpaceAfterKeyword(returnString, keywordCheck);
            if (warningCheck == true)
            {
                int index2 = returnString.IndexOf(keywordCheck);
                var exceptionCheck = returnString.Substring(index2, 1);
                if (exceptionCheck != "new[")
                {
                    returnString = returnString.Insert(index2 + keywordCheck.Length, " ");
                }
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
