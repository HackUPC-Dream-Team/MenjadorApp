using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menjadora
{
    public partial class Management : Form
    {
        FormLista Fl = null;
        Boolean mod = false;
        String valorB;
        SQLiteConnection con=null;

        public Management(FormLista fl, SQLiteConnection m_dbConnection)
        {
            Fl = fl;
            InitializeComponent();
            con = m_dbConnection;
            
        }

        public Management(FormLista fl, String valor)
        {
            Fl = fl;
            InitializeComponent();
           
            char[] delimiterChars = { ' ', ':' };
            string[] words = valor.Split(delimiterChars);

            textBox1.Text = words[2];
            valorB = valor;
            textBoxHora.Text = words[words.Length-2];
            textBoxMin.Text =  words[words.Length-1];
            mod = true;

            buttonDelete.Visible = true;


        }
        



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(mod)
                {
                    Fl.listBox1.Items.Remove(valorB);
                    deleteReg(true);
                }

                int min = Convert.ToInt32(textBoxMin.Text);
                int hora = Convert.ToInt32(textBoxHora.Text);


                con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                con.Open();

                string sql = "insert into feed values(NULL, "+textBox1.Text+", "+ "1" +textBoxHora.Text + "1" + textBoxMin.Text + ");";
                SQLiteCommand command = new SQLiteCommand(sql, con);
                command.ExecuteNonQuery();
                
                String valor = "Amount: " + textBox1.Text + "   Time: " + textBoxHora.Text + ":" + textBoxMin.Text;
                Fl.listBox1.Items.Add(valor);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Couldn't be saved.");
            }
            finally
            {
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fl.listBox1.Items.Remove(valorB);
            deleteReg(false);
            
        }

        private void deleteReg(Boolean mod)
        {
            con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            con.Open();
            string sql = "";

            if(mod)
            {
                char[] delimiterChars = { ' ', ':' };
                string[] words = valorB.Split(delimiterChars);
                sql = "delete from feed where amount =" + words[2] + " and time =1" + words[words.Length - 2] + "1" + words[words.Length - 1] + ";";
            }
            else
            {
                sql = "delete from feed where amount =" + textBox1.Text + " and time =1" + textBoxHora.Text + "1" + textBoxMin.Text + ";";
            }
            
            SQLiteCommand command = new SQLiteCommand(sql, con);
            command.ExecuteNonQuery();
            this.Close();
        }

        private void textBoxHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
