using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace SuperMarketMangmentSystem
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=SMarketDB;Integrated Security=True");

        private void button5_Click(object sender, EventArgs e)
        {
            try
            { 
                con.Open();
               
                string query =  $"insert into Category values ('{CatIdTB.Text}','{CatNameTB.Text}','{CatDesTB.Text}')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
                con.Close();
                populate();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);

            }   
        }

        private void populate()
        {
            con.Open();
            String query = "select * from Category";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesGV.DataSource = ds.Tables[0];
            con.Close();

        }


        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTB.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTB.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDesTB.Text = CategoriesGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatIdTB.Text=="")
                {
                    MessageBox.Show("Select The Category To Delete");
                }
                else
                {
                    con.Open();
                    string query =$"delete from Category where Id='{CatIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
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
                if (CatIdTB.Text == "" || CatNameTB.Text == "" || CatDesTB.Text == "")
                {
                    MessageBox.Show("Missing Info");
                }
                else
                {
                    con.Open();
                    string query = $"update Category set Name='{CatNameTB.Text}',Description='{CatDesTB.Text}' where Id='{CatIdTB.Text}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Successfully Updated");
                    con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show (ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm form = new ProductForm();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerForm form = new SellerForm();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide
                ();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
