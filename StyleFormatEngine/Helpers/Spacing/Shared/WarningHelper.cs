namespace StyleFormatEngine.Helpers.Spacing.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleFormatEngine.Extensions;

    public class WarningHelper: Library
    {
        internal bool SingleSpaceAfterKeyword(string s, string keywordCheck)
        {
            var startIndexes = s.WholeWordIndexesOf(keywordCheck);

            return IsWarningNeededAfter(s, startIndexes, keywordCheck);
        }

        internal bool NeedWarningForSingleWhiteSpaceAfterCharacter(string s, string commentType)
        {
            var startIndexes = s.IndexesOf(commentType);

            return IsWarningNeededAfter(s, startIndexes, commentType);
        }

        // returns true is there is no whitespace after keyword
        public bool IsWarningNeededAfter(string s, IEnumerable<int> startIndexes, string keywordCheck)
        {
            try
            {
                var whiteSpaceCount = 0;
                var characterAfterKeyword = 0;
                char nextCharacter = char.MinValue;
                var keywordBuild = keywordCheck;
                var endPoint = s.Length - 1;

                foreach (var startPoint in startIndexes)
                {
                    if (startPoint < endPoint)
                    {
                        if (IsKeywordInQuotes(s, keywordCheck, startPoint) == false)
                        {
                            for (int i = startPoint + keywordCheck.Length; i < endPoint; i++)
                            {
                                if (characterAfterKeyword < 3)
                                {
                                    if (!char.IsWhiteSpace(s[i]))
                                    {
                                        characterAfterKeyword++;
                                        keywordBuild = s[i].ToString();
                                        nextCharacter = s[i + 1];
                                    }
                                    else
                                    {
                                        whiteSpaceCount++;
                                        if (whiteSpaceCount > 1)
                                        {
                                            if (CommentKeys.Contains(keywordCheck))
                                            {
                                                return false;
                                            }
                                        
                                            return true;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var item in Exceptions)
                                    {
                                        if (keywordBuild + nextCharacter.ToString() == item)
                                        {
                                            return false;
                                        }
                                    }
                                
                                    return (whiteSpaceCount == 1) ? false : true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool NeedWarningWhiteSpaceBeforeCharacter(string s, string commentType)
        {
            var isAtBeginningOfRow = IsCharacterAtBeginningOfALine(s, commentType);
            if (isAtBeginningOfRow == false)
            {
                var startIndexes = s.IndexesOf(commentType);
                return IsWarningNeededBefore(startIndexes, s, commentType);
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
                return IsWarningNeededBefore(startIndexes, s, keywordCheck);
            }
            // not warning needed because it is at the beginning of the row
            return false;
        }

        // Exception check when keywords are in quotes like "for"
        private bool IsKeywordInQuotes(string s, string keywordCheck, int startPoint)
        {
            try
            {

            
            if (startPoint > 0)
            {
                if (s.Substring(startPoint - 1, startPoint) == "\"")
                {
                    return true;
                }

                if (s.Substring(startPoint, (startPoint + keywordCheck.Length + 1)) == "\"")
                {
                    return true;
                }
            }

            return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
  
        private bool IsWarningNeededBefore(IEnumerable<int> startIndexes, string s, string keywordCheck)
        {
            try
            {
                var whiteSpaceCount = 0;
                char characterAfterKeyword = new char();

                foreach (var startPoint in startIndexes)
                {
                    if (startPoint > 0)
                    {
                        if (IsKeywordInQuotes(s, keywordCheck, startPoint) == false)
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
            catch (Exception)
            {
                return false;
            }
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

        internal bool IsCharacterAtBeginningOfALine(string s, string commentType)
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

        internal bool IsCharacterAtEndOfALine(string s, string commentType)
        {
            var stringArray = s.Split('\n');
            var wordsInString = s.IndexesOf(commentType);
            var counter = 0;

            foreach (var item in stringArray)
            {
                var stringValue = item.TrimEnd();
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
