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

namespace SuperMarketMangmentSystem
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=SMarketDB;Integrated Security=True");

        private void populate()
        {
            con.Open();
            String query = "select * from Seller";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string query = $"insert into Seller values ('{SellerIdTB.Text}','{SellerNameTB.Text}','{SellerAgeTB.Text}','{SellerPhoneTB.Text}','{SellerPasswordTB.Text}')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully");
                con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void SellerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SellerIdTB.Text = SellerGV.SelectedRows[0].Cells[0].Value.ToString();
            SellerNameTB.Text = SellerGV.SelectedRows[0].Cells[1].Value.ToString();
            SellerAgeTB.Text = SellerGV.SelectedRows[0].Cells[2].Value.ToString();
            SellerPhoneTB.Text = SellerGV.SelectedRows[0].Cells[3].Value.ToString();
            SellerPasswordTB.Text = SellerGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellerIdTB.Text == "")
                {
                    MessageBox.Show("Select The Seller To Delete");
                }
                else
                {
                    con.Open();
                    string query = $"delete from Seller where Id='{SellerIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully");
                    con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellerIdTB.Text == "" || SellerNameTB.Text == "" || SellerAgeTB.Text == "" || SellerPhoneTB.Text == "" || SellerPasswordTB.Text == "")
                {
                    MessageBox.Show("Missing Info");
                }
                else
                {
                    con.Open();
                    string query = $"update Seller set Name='{SellerNameTB.Text}',Age='{SellerAgeTB.Text}',Phone='{SellerPhoneTB.Text}',Password='{SellerPasswordTB.Text}' where Id='{SellerIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated");
                    con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm form= new ProductForm();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();

            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
