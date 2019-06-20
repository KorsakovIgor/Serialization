using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serialization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            ListRand list = new ListRand();

            try
            {
                using (FileStream fstream = File.OpenRead(@"RecordText.txt"))
                {
                    list.Deserialize(fstream);
                }

                for (int i = 0; i < list.count; i++)
                {
                    listBox2.Items.Add(list[i].Data + " ->(Ссылка на случайный)-> " + list[i].Rand.Data);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось десериализовать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ListRand list = new ListRand();
            list.Add("\"A\",B,");
            list.Add("\"B,");
            list.Add("\"C\",");
            list.Add("\",D,\"");
            list.Add(",E\"");
            list.Add(",\"F\",");
            list.Add(",,,G\"\"\"");
            list.Add("\",\"H");

            for (int i = 0; i < list.count; i++)
            {
                listBox1.Items.Add(list[i].Data + " ->(Ссылка на случайный)-> " + list[i].Rand.Data);
            }

            using (FileStream fstream = new FileStream(@"RecordText.txt", FileMode.Create))
            {
                list.Serialize(fstream);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"RecordText.txt");
            }
            catch (System.ComponentModel.Win32Exception d)
            {
                MessageBox.Show(d.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
