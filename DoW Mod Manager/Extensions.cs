using System;

namespace DoW_Mod_Manager
{
    public static class Extensions
    {
        /// <summary>
        /// This method gets a value from a line of text
        /// </summary>
        /// <param name="deleteModule"></param>
        /// <returns>string</returns>
        public static string GetValueFromLine(this string line, bool deleteModule)
        {
            int indexOfEqualSigh = line.IndexOf('=');

            if (indexOfEqualSigh > 0)
            {
                // Deleting all chars before equal sigh
                line = line.Substring(indexOfEqualSigh + 1, line.Length - indexOfEqualSigh - 1);

                if (deleteModule)
                    return line.Replace(" ", "").Replace(".module", "");
                else
                    return line.Replace(" ", "");
            }
            else
                return "";
        }
    }
}
