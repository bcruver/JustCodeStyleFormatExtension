﻿namespace JustCodeStyleFormatExtension.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JustCodeStyleFormatExtension.Extensions;

    public class WhiteSpaceKeyWordWarningHelper
    {
        internal bool NeedWarningForSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            var startIndexes = s.WholeWordIndexesOf(keywordCheck);

            return IsWarningNeededAfter(s, startIndexes, keywordCheck);
        }

        internal bool NeedWarningForSingleWhiteSpaceAfterComment(string s, string commentType)
        {
            var startIndexes = s.IndexesOf(commentType);

            return IsWarningNeededAfterComment(s, startIndexes, commentType);
        }

        private bool IsWarningNeededAfterComment(string s, IEnumerable<int> startIndexes, string keywordCheck)
        {
            var whiteSpaceCount = 0;
            var characterAfterKeyword = 0;
            var endPoint = s.Length - 1;

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
                            }
                            else
                            {
                                whiteSpaceCount++;
                                if (whiteSpaceCount > 1)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return (whiteSpaceCount > 0) ? false : true;
                        }
                    }
                }
            }
            return false;
        }




































  
        private bool IsWarningNeededAfter(string s, IEnumerable<int> startIndexes, string keywordCheck)
        {
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

        internal bool NeedWarningWhiteSpaceBeforeComment(string s, string commentType)
        {
            var isAtBeginningOfRow = IsCommentAtBeginningOfALine(s, commentType);
            if(isAtBeginningOfRow == false)
            {
                var startIndexes = s.IndexesOf(commentType);
                bool returnValue;
                if (IsWarningNeededBefore(startIndexes, s, out returnValue))
                {
                    return returnValue;
                }
            }

            // not warning needed because it is at the beginning of the row
            return false;
        }

        internal bool NeedWarningWhiteSpaceBeforeKeyword(string s, string keywordCheck)
        {
            var isAtBeginningOfRow = IsKeywordAtTheBeginningOfALine(s, keywordCheck);
            if (isAtBeginningOfRow == false)
            {
                var startIndexes = s.WholeWordIndexesOf(keywordCheck);
                bool returnValue;
                if (IsWarningNeededBefore(startIndexes, s, out returnValue))
                {
                    return returnValue;
                }
            }
            // not warning needed because it is at the beginning of the row
            return false;
        }
  
        private bool IsWarningNeededBefore(IEnumerable<int> startIndexes, string s, out bool returnValue)
        {
            returnValue = false;
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
                                    returnValue = true;
                                    return true;
                                }

                                if ((s[i] == '\n' || s[i] == '\r') && whiteSpaceCount < 2)
                                {
                                    returnValue = false;
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (whiteSpaceCount == 1)
                            {
                                returnValue = false;
                                return true;
                            }
                            else
                            {
                                if (characterAfterKeyword == '\n')
                                {
                                    returnValue = false;
                                    return true;
                                }
                                else
                                {
                                    returnValue = true;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        internal bool IsKeywordAtTheBeginningOfALine(string s, string keyworkCheck)
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

                return IsCounterGreaterThenArray(wordsInString, counter);
            }
            return true;
        }
  
        private bool IsCounterGreaterThenArray(IEnumerable<int> wordsInString, int counter)
        {
            counter++;
            if (counter >= wordsInString.Count())
            {
                return true;
            }
            return false;
        }

        internal bool IsCommentAtBeginningOfALine(string s, string commentType)
        {
            var stringArray = s.Split('\n');
            var wordsInString = s.IndexesOf(commentType);
            var counter = 0;

            foreach (var item in stringArray)
            {
                var stringValue = item.TrimStart();
                if (stringValue.IndexOf(commentType) > 0)
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
    }
}
