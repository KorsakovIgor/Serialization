using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public static class Parsing
    {
        /// <summary>
        /// Разделение строки на подстроки
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns></returns>
        public static List<string> SplitText(string str)
        {
            List<string> result = new List<string>();
            string cur;
            int count;
            int beginIndex;
            int endIndex;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '"')
                {
                    count = QuotesCount(str, i);
                    if (count % 2 != 0)
                    {
                        beginIndex = i + 1;
                        endIndex = EndQuotesIndex(str, beginIndex);
                        cur = str.Substring(beginIndex, endIndex - beginIndex);
                        cur = cur.Replace("\"\"", "\"");
                        result.Add(cur);
                        i = endIndex + 1;
                    }
                }
                else
                {
                    if (str.IndexOf(',', i) >= 0)
                    {
                        cur = str.Substring(i, str.IndexOf(',', i) - i);
                        cur = cur.Replace("\"\"", "\"");
                        result.Add(cur);
                        i = str.IndexOf(',', i);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Подсчет количества подряд идущих кавычек
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="index">Начальный индекс</param>
        /// <returns></returns>
        public static int QuotesCount(string str, int index)
        {
            int count = 0;
            while (str[index] == '"')
            {
                count++;
                index++;
                if (index == str.Length) break;
            }
            return count;
        }

        /// <summary>
        /// Поиск индекса закрывающей кавычки
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="index">Индекс открывающей кавычки</param>
        /// <returns></returns>
        public static int EndQuotesIndex(string str, int index)
        {
            int result = -1;
            int count = 0;
            for (int i = index; i < str.Length; i++)
            {
                if (str[i] == '"')
                {
                    count = QuotesCount(str, i);
                    if (count % 2 != 0)
                    {
                        result = i + count - 1;
                        break;
                    }
                    i += count - 1;
                }
            }
            return result;
        }
    }
}
