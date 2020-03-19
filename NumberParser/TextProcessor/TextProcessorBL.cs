using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessor
{
    /// <summary>
    /// Text Processor extracts numbers from the given key stroke lines.
    /// </summary>
    public class TextProcessorBL
    {
        #region Public Methods

        /// <summary>
        /// Takes input text lines and returns List of number strings
        /// </summary>
        /// <param name="allLines">List of the lines from the text file </param>
        /// <returns>List of Nummeric value strings</returns>
        public static IEnumerable<string> ParseText(List<string> allLines)
        {
            List<string> resultStrs = new List<string>();

            try
            {
                if (allLines.Count % 4 == 0)
                {
                    string resultline = string.Empty;
                    for (int lcount = 0; lcount < allLines.Count(); lcount += 4)
                    {
                        var lineSet = allLines.Skip(lcount).Take(4);
                        //Checking the each line length is consistent or not
                        if (IsValidNumberSet(lineSet))
                        {
                            var NumberListBuilder = ParseNumberSet(lineSet);
                            resultStrs.Add(ExtractNumberFromSymbolSet(NumberListBuilder));
                        }
                        else
                        {
                            resultStrs.Add(string.Format("{0} numerical line format is not consistent",lcount/4+1));
                        }
                    }
                }
                else
                {
                    resultStrs.Add("File format is not consistent");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return resultStrs;
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// Validates one numerical row to avoid exceptions and to get acurate values.
        /// </summary>
        /// <param name="numberSet"></param>
        /// <returns></returns>
        private static bool IsValidNumberSet(IEnumerable<string> numberSet)
        {
            bool isValid = true;
            int lineLength = numberSet.FirstOrDefault().Length;
            foreach (string line in numberSet)
            {
                if (line.Length % 5 == 0 && lineLength==line.Length)
                    continue;
                else
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Takes Numberical row as input and returns List of symbol strings
        /// </summary>
        /// <param name="lineSet">One numerical row(4 lines)</param>
        /// <returns>List string Builders objects with symbols</returns>
        private static List<StringBuilder> ParseNumberSet(IEnumerable<string> lineSet)
        {
             List<StringBuilder> NumberStrings= new List<StringBuilder>() ;
            int index = 0;
            foreach(string line in lineSet) 
            {
                if (index == 0) 
                    for(int j=0;j<line.Length/5;j++)
                        NumberStrings.Add(new StringBuilder()); //Initilizing the string Builder object very first time.
                index = 0;
                for (int i=0;i<line.Length;i += 5)
                 {
                    NumberStrings[index].Append(line.Substring(i, 5)); 
                    index += 1;
                 }
               
            }
            return NumberStrings;
        }
        
        /// <summary>
        /// Exstract number from symbol set
        /// </summary>
        /// <param name="numberStrings">List of Symbol sets</param>
        /// <returns>NUmber string for given list</returns>
        private  static string ExtractNumberFromSymbolSet(List<StringBuilder> numberStrings)
        {
            StringBuilder resultBuilder = new StringBuilder();
            int numericTotal = 0;
            foreach (var numBuilder in numberStrings)
            {
                //Preparing list of letters with count using the Lamda expression by excluding the empty spaces.
                var charGrp = numBuilder.ToString()
                                    .Where(c => !Char.IsWhiteSpace(c))
                                    .GroupBy(c => c)
                                    .Select(group => new
                                    {
                                        Letter = group.Key,
                                        Count = group.Count()
                                    });
                numericTotal = 0;
                foreach (var charSet in charGrp)
                {
                    numericTotal += (int)charSet.Letter * charSet.Count;
                }
                resultBuilder.Append(GetNumberbyCharTotal(numericTotal));
                resultBuilder.Append(" ");
            }
            return resultBuilder.ToString();
        }

        /// <summary>
        /// Takes symbols ASCII total and returns the number. 
        /// </summary>
        /// <param name="charSum">Sum of ASCII value of charecter and count of the Symbols</param>
        /// <returns></returns>
        private static int GetNumberbyCharTotal(int charSum)
        {
            int num = -1;
            switch (charSum)
            {
                case 496:
                    num = 1;
                    break;
                case 613:
                    num = 2;
                    break;
                case 364:
                    num = 3;
                    break;
                case 1029:
                    num = 4;
                    break;
                case 1262:
                    num = 5;
                    break;
                case 1314:
                    num = 6;
                    break;
            }
            return num;
        }
        #endregion

    }
}
