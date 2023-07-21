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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SuperMarketMangmentSystem
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new(@"Data Source=.;Initial Catalog=SMarketDB;Integrated Security=True");
        private void FillCategCB()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Name from Category", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name",typeof(string));
            dt.Load(rdr);
            ProdCatCB.ValueMember = "Name";
            ProdCatCB.DataSource= dt;
            comboBox2.ValueMember= "Name";
            comboBox2.DataSource= dt;
            con.Close();

        }

        private void populate()
        {
            con.Open();
            String query = "select * from Product";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillCategCB();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string query = $"insert into Product values ('{ProdIdTB.Text}','{ProdNameTB.Text}','{ProdQuanTB.Text}','{ProdPriceTB.Text}','{ProdCatCB?.SelectedValue?.ToString()}')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                con.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm form = new SellerForm();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void ProductGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ProdIdTB.Text = ProductGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdNameTB.Text = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdQuanTB.Text = ProductGV.SelectedRows[0].Cells[2].Value.ToString();
            ProdPriceTB.Text = ProductGV.SelectedRows[0].Cells[3].Value.ToString();
            ProdCatCB.Text = ProductGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdIdTB.Text == "")
                {
                    MessageBox.Show("Select The Product To Delete");
                }
                else
                {
                    con.Open();
                    string query = $"delete from Product where Id='{ProdIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
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
                if (ProdIdTB.Text == "" || ProdNameTB.Text == "" || ProdPriceTB.Text == "" || ProdQuanTB.Text == "")
                {
                    MessageBox.Show("Missing Info");
                }
                else
                {
                    con.Open();
                    string query = $"update Product set Name='{ProdNameTB.Text}',Quantity='{ProdQuanTB.Text}',Price='{ProdPriceTB.Text}, Category='{ProdCatCB.SelectedValue.ToString()}' where Id='{ProdIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated");
                    con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from Product where Category=" + comboBox2.SelectedValue.ToString();
            SqlDataAdapter sda=new SqlDataAdapter(query, con);
            SqlCommandBuilder builder=new SqlCommandBuilder(sda);
            var ds=new DataSet();
            sda.Fill(ds);
            ProductGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide
                ();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
