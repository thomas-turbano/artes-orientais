using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Maes_solteiras_athur
{
    public partial class Form1 : Form
    {
        private int Id;
        public Form1()
        {
            InitializeComponent();
        }
        private void UpdateListView()
        {
            listView1.Items.Clear();
            Connect conn = new Connect();
            SqlCommand sqlCom = new SqlCommand();

            sqlCom.Connection = conn.ReturnConnection();
            sqlCom.CommandText = "SELECT * FROM [dbo].[User]";

            try
            {
                SqlDataReader dr = sqlCom.ExecuteReader();

                //Enquanto for possível continuar a leitura das linhas que foram retornadas na consulta, execute.
                while (dr.Read())
                {
                    int id = (int)dr["ID"];
                    string Nome = (string)dr["Nome"];
                    string Email = (string)dr["Email"];
                    string tel = (string)dr["Telefone"];
                    string CEP = (string)dr["CEP"];
                    string senha = (string)dr["Senha"];

                    ListViewItem lv = new ListViewItem(id.ToString());
                    lv.SubItems.Add(Nome);
                    lv.SubItems.Add(Email);
                    lv.SubItems.Add(tel);
                    lv.SubItems.Add(CEP);
                    lv.SubItems.Add(senha);
                    listView1.Items.Add(lv);

                }
                dr.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void text1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {

            string nome = text1.Text, Email = text2.Text, telefone = text3.Text, senha = text4.Text;
            string CEP = mtext3.Text;
            MessageBox.Show(
                "Nome: " + nome +
                "\nEmail: " + Email +
               "\nTelefone: " + telefone +
               "\nCEP: " + CEP +
                "\nSenha: " + senha,
               "Informações"
            );

            Connect connection = new Connect();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"INSERT INTO [dbo].[User] VALUES(@Nome,@Email,@Telefone,@CEP,@Senha)";
            sqlCommand.Parameters.AddWithValue("@Nome", text1.Text);
            sqlCommand.Parameters.AddWithValue("@Email", text2.Text);
            sqlCommand.Parameters.AddWithValue("@Telefone", text3.Text);
            sqlCommand.Parameters.AddWithValue("@CEP", mtext3.Text);
            sqlCommand.Parameters.AddWithValue("@Senha", text4.Text);
            sqlCommand.ExecuteNonQuery();

            text1.Clear();
            text2.Clear();
            text3.Clear();
            mtext3.Clear();
            text4.Clear();    

            UpdateListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int index;
            index = listView1.FocusedItem.Index;
            Id = int.Parse(listView1.Items[index].SubItems[0].Text);
            text1.Text = listView1.Items[index].SubItems[1].Text;
            text2.Text = listView1.Items[index].SubItems[2].Text;
            text3.Text = listView1.Items[index].SubItems[3].Text;
            mtext3.Text = listView1.Items[index].SubItems[4].Text;
            text4.Text = listView1.Items[index].SubItems[5].Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
