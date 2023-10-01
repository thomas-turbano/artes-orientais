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

namespace Maes_solteiras_athur
{
    public partial class Form2 : Form
    {
        private int Id;
        public Form2()
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
        private void button1_Click(object sender, EventArgs e)
        {
            Connect connection = new Connect();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"DELETE FROM [dbo].[User] WHERE ID = @ID";
            sqlCommand.Parameters.AddWithValue("@ID", Id);
            try
            {
                sqlCommand.ExecuteNonQuery();
                UpdateListView();
                MessageBox.Show("Exclusão foi feita com sucesso",
               "AVISO",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                throw new Exception("Erro: Problemas ao excluir usuário no banco.\n" + err.Message);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {

            Connect connection = new Connect();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"UPDATE [dbo].[User] SET 
                 Nome       = @Nome, 
                 Email      = @Email,
                 CEP        = @CEP, 
                 Telefone   = @telefone, 
                 Senha      = @Senha
                 WHERE ID   = @ID";

            sqlCommand.Parameters.AddWithValue("@Nome", text1.Text);
            sqlCommand.Parameters.AddWithValue("@Email", text2.Text);
            sqlCommand.Parameters.AddWithValue("@Telefone", text3.Text);
            sqlCommand.Parameters.AddWithValue("@CEP", mtext3.Text);
            sqlCommand.Parameters.AddWithValue("@Senha", text4.Text);
            sqlCommand.Parameters.AddWithValue("@ID", txtID.Text);

            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Edicão feita com sucesso",
                "AVISO",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            text1.Clear();
            text2.Clear();
            text3.Clear();
            mtext3.Clear();
            text4.Clear();
            txtID.Clear();

            UpdateListView();
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            int index;
            index = listView1.FocusedItem.Index;
            Id = int.Parse(listView1.Items[index].SubItems[0].Text);
            txtID.Text = listView1.Items[index].SubItems[0].Text;
            text1.Text = listView1.Items[index].SubItems[1].Text;
            text2.Text = listView1.Items[index].SubItems[2].Text;
            text3.Text = listView1.Items[index].SubItems[3].Text;
            mtext3.Text = listView1.Items[index].SubItems[4].Text;
            text4.Text = listView1.Items[index].SubItems[5].Text;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
