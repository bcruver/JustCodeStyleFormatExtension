using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JustCodeStyleFormatExtension.Helpers
{
    public class WhiteSpaceHelper
    {
        public bool NeedWarningWhiteSpaceSingleSpace(string s, string keywordCheck)
        {
            var startPoint = s.IndexOf(keywordCheck) + keywordCheck.Length;            
            var whiteSpaceCount = 0;
            var characterAfterKeyword = 0;
            var endPoint = s.Length - 1;

            if (startPoint < endPoint)
            {
                for (int i = startPoint; i < endPoint; i++)
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

            return true;
        }

        public string RemoveAddSingleWhiteSpaceAfterKeyword(string s, string keywordCheck)
        {
            string returnString = s;
            returnString = this.RemoveAllDoubleSpacesOnString(s);
            var warningCheck = this.NeedWarningWhiteSpaceSingleSpace(returnString, keywordCheck);

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
