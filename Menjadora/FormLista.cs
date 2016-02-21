using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.Net;

namespace Menjadora
{
    public partial class FormLista : Form
    {
        public ArrayList feedList = new ArrayList();
        static int index = 0;
        SQLiteConnection m_dbConnection;

        public FormLista()
        {
            InitializeComponent();
            //SQLiteConnection.CreateFile("MyDatabase.sqlite");


            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            //string sql = "create table if not exists feed (id INTEGER PRIMARY KEY AUTOINCREMENT, amount INTEGER, time INTEGER);";
            //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            
            string sql = "select * from feed";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int amount = Convert.ToInt32(reader["amount"]);
                string time = Convert.ToString(reader["time"]);

                string hora = time[1].ToString() + time[2].ToString();
                string min = time[4].ToString() + time[5].ToString();
                
                String valor = "Amount: " + amount + "  Time: " + hora + ":" + min;
                listBox1.Items.Add(valor);
            }
        }

        private void FormLista_Load(object sender, EventArgs e)
        {

        }

           
        private void FormLista_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Management form1 = new Management(this, m_dbConnection);
            form1.ShowDialog();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Management gest = new Management(this, listBox1.Items[index].ToString());
            gest.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = listBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String json = "";
                String valor;
                for (int i = 0; i < listBox1.Items.Count; ++i)
                {
                    valor = listBox1.Items[i].ToString();
                    char[] delimiterChars = { ' ', ':' };
                    string[] words = valor.Split(delimiterChars);

                    ClassFeed obj = new ClassFeed(Convert.ToInt32(words[2]), Convert.ToInt32(words[words.Length - 2]), Convert.ToInt32(words[words.Length - 1]));
                    json = json + JsonConvert.SerializeObject(obj) + (i == listBox1.Items.Count - 1 ? "" : ",");
                }

                json = "{\"actions\":[" + json + "]}";

                var client = new System.Net.Http.HttpClient
                {
                    BaseAddress = new Uri("http://10.4.180.158:5000")
                };

                var response = client.PostAsync("/schedule", new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("yay");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Couldn't reach the server.","Error");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string html = string.Empty;
                string url = @"http://10.4.180.158:5000/capture";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.GetResponse();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Couldn't reach the server.","Error");
            }            
        }
    }
}
