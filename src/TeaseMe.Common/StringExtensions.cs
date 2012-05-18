using System;

namespace TeaseMe.Common
{
    public static class StringExtensions
    {
        public static string Remove(this string originalText, params char[] charsToRemove)
        {
            return String.Concat(originalText.Split(charsToRemove));
        }

        /// <summary>
        /// Returns the part of the string before the first occurence of the given string,
        /// or null when the original text does not contain the text to find. 
        /// </summary>
        public static string BeforeFirst(this string originalText, string textToFind)
        {
            if (String.IsNullOrEmpty(originalText) || !originalText.Contains(textToFind))
            {
                return null;
            }

            return originalText.Substring(0, originalText.IndexOf(textToFind, StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns the part of the string before the last occurence of the given string,
        /// or null when the original text does not contain the text to find. 
        /// </summary>
        public static string BeforeLast(this string originalText, string textToFind)
        {
            if (String.IsNullOrEmpty(originalText) || !originalText.Contains(textToFind))
            {
                return null;
            }

            return originalText.Substring(0, originalText.LastIndexOf(textToFind, StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns the part of the string after the first occurence of the given string, 
        /// or null when the original text does not contain the text to find.
        /// </summary>
        public static string AfterFirst(this string originalText, string textToFind)
        {
            if (String.IsNullOrEmpty(originalText) || !originalText.Contains(textToFind))
            {
                return null;
            }
            
            return originalText.Substring(originalText.IndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length);
        }

        /// <summary>
        /// Returns the part of the string after the last occurence of the given string, 
        /// or null when the original text does not contain the text to find.
        /// </summary>
        public static string AfterLast(this string originalText, string textToFind)
        {
            if (String.IsNullOrEmpty(originalText) || !originalText.Contains(textToFind))
            {
                return null;
            }
            
            return originalText.Substring(originalText.LastIndexOf(textToFind, StringComparison.Ordinal) + textToFind.Length);
        }

        /// <summary>
        /// Returns the part of the string between the first occurence of the openingText and the LAST occurence of the closingText, 
        /// or null when the original text does not contain the opening and/or closing text.
        /// </summary>
        public static string BetweenGreedy(this string originalText, string openingText, string closingText)
        {
            if (String.IsNullOrEmpty(originalText) || !(originalText.Contains(openingText) && originalText.AfterFirst(openingText).Contains(closingText)))
            {
                return null;
            }

            return originalText.AfterFirst(openingText).BeforeLast(closingText);
        }
        
        /// <summary>
        /// Returns the part of the string between the first occurence of the openingText and the FIRST occurence of the closingText, 
        /// or null when the original text does not contain the opening and/or closing text.
        /// </summary>
        public static string BetweenNonGreedy(this string originalText, string openingText, string closingText)
        {
            if (String.IsNullOrEmpty(originalText) || !(originalText.Contains(openingText) && originalText.AfterFirst(openingText).Contains(closingText)))
            {
                return null;
            }

            return originalText.AfterFirst(openingText).BeforeFirst(closingText);
        }

        /// <summary>
        /// Returns the part of the string starting at the first opening bracket and ending with the matching closing bracket, 
        /// or null when the original text does not contain brackets or is unbalanced.
        /// </summary>
        public static string FirstMatchingBrackets(this string originalText)
        {
            if (String.IsNullOrEmpty(originalText))
            {
                return null;
            }

            int start = 0;
            int end = 0;
            int level = 0;
            bool betweenQuotes = false;
            char quoteChar = '"';
            for (var i = 0; i < originalText.Length; i++)
            {
                char current = originalText[i];

                if (!betweenQuotes)
                {
                    if (current == '\'')
                    {
                        quoteChar = '\'';
                        betweenQuotes = true;
                    }
                    else if (current == '"')
                    {
                        quoteChar = '"';
                        betweenQuotes = true;
                    }
                    else
                    {
                        if (current == '(')
                        {
                            if (level == 0)
                            {
                                start = i;
                            }
                            level++;
                        }
                        else if (current == ')')
                        {
                            level--;
                            if (level == 0)
                            {
                                end = i;
                                // matching bracket found, we can stop now.
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // between quotes
                    if (current == quoteChar)
                    {
                        betweenQuotes = false;
                    }
                }
            }

            if (end > 0)
            {
                return originalText.Substring(start, end-start+1);
            }

            return null;
        }
    }
}
