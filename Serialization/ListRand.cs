using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int count;

        public static Random rand = new Random();

        /// <summary>
        /// Добавление элемента в конец списка
        /// </summary>
        /// <param name="data">Информационная часть</param>
        public void Add(string data)
        {
            if (Head == null)
            {
                Head = new ListNode();
                Head.Data = data;
                Head.Rand = Head;
                Tail = Head;
                count = 1;
            }
            else
            {
                ListNode newElem = new ListNode();
                Tail.Next = newElem;
                newElem.Prev = Tail;
                Tail = newElem;
                Tail.Data = data;
                count++;

                for(int i=0; i<count; i++)
                {
                    this[i].Rand = this[rand.Next(0, count)];
                }
            }
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        /// <param name="s"></param>
        public void Serialize(FileStream s)
        {
            string text = "";
            string text1;
            ListNode cur = Head;

            while (cur != null)
            {
                //Индекс рандомного элемента, на который ссылается данный элемент
                text += GetIndex(cur.Rand).ToString() + ",";

                //Далее информационная часть элемента, разделителем значений является символ запятой
                text1 = cur.Data;

                //Если в информационной части встречаются кавычки, то они представляются в виде двух кавычек подряд
                text1=text1.Replace("\"", "\"\"");

                //Если информационная часть содержит запятые, то обрамляем кавычками
                if (text1.Contains(","))
                {
                    text1 = text1.Insert(0, "\"");
                    text1 += "\"";
                }
                text += text1;
                text += ",";
                cur = cur.Next;
            }


            byte[] array = System.Text.Encoding.Default.GetBytes(text);
            s.Write(array, 0, array.Length);
            
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="s"></param>
        public void Deserialize(FileStream s)
        {
            byte[] array = new byte[s.Length];
            s.Read(array, 0, array.Length);
            string text = System.Text.Encoding.Default.GetString(array);
            List<string> splitText = Parsing.SplitText(text);

            for (int i = 1; i < splitText.Count; i+=2)
            {
                this.Add(splitText[i]);
            }

            for(int i=0; i<splitText.Count; i+=2)
            {
                this[i / 2].Rand = this[Convert.ToInt32(splitText[i])];
            }
        }

        public ListNode this[int index]
        {
            get
            {
                ListNode cur = this.Head;
                for(int i=0; i<index; i++)
                {
                    cur = cur.Next;
                }
                return cur;
            }
        }
        
        /// <summary>
        /// Получение индекса 
        /// </summary>
        /// <param name="elemList">Элемент списка</param>
        /// <returns></returns>
        public int GetIndex(ListNode elemList)
        {
            ListNode cur = this.Head;
            int counter = 0;
            while(cur!=elemList)
            {
                counter++;
                cur = cur.Next;
            }
            return counter;
        }
            
    }
}
