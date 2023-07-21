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
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
            //printDocument1.DocumentName = "Bill Document";
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }
        SqlConnection con = new(@"Data Source=.;Initial Catalog=SMarketDB;Integrated Security=True");
        private void populate()
        {
            con.Open();
            String query = "select Name, Price, Quantity from Product";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void populateBills()
        {
            con.Open();
            String query = "select * from Bill";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void FillCategCB()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Name from Category", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Load(rdr);
            comboBox2.ValueMember = "Name";
            comboBox2.DataSource = dt;

            con.Close();

        }

        private void ReduceProductQuantity(string ProductName, int quantityToReduce)
        {
            //string connectionString = "your connection string";
            string updateQuery = "UPDATE Product SET Quantity = Quantity - @QuantityToReduce WHERE Name = @ProductName";

            //using (SqlConnection connection = new SqlConnection(connectionString))

            SqlCommand command = new SqlCommand(updateQuery, con);
                
                    command.Parameters.AddWithValue("@QuantityToReduce", quantityToReduce);
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Handle the case where the product with the given Id was not found in the database
                    }
                    con.Close();
            populate();
            //populateBills();
               
            
        }

        private void ProdQuanTB_TextChanged(object sender, EventArgs e)
        {

        }
        int flag = 0;
        private void ProdGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTB.Text = ProdGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdPriceTB.Text = ProdGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdQuanTB.Text = ProdGV.SelectedRows[0].Cells[2].Value.ToString();
            flag = 1;
        
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBills();
            FillCategCB();
            SellerNamelbl.Text = Form1.sellerName;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DateLabel.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString() ;

        }
        int GrdTotlal = 0;
        int n = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            

            if (ProdNameTB.Text == "" || ProdQuanTB.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                
                int total = Convert.ToInt32(ProdPriceTB.Text) * Convert.ToInt32(ProdQuanTB.Text);

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(OrdersGV);
                row.Cells[0].Value = n + 1;
                row.Cells[1].Value = ProdNameTB.Text;
                row.Cells[2].Value = ProdPriceTB.Text;
                row.Cells[3].Value = ProdQuanTB.Text;
                row.Cells[4].Value = int.Parse(ProdPriceTB.Text) * int.Parse(ProdQuanTB.Text);
                OrdersGV.Rows.Add(row);
                n++;
                GrdTotlal += total;
                AmountLabel.Text = GrdTotlal.ToString();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {


            try
            {
                if (BillIdTB.Text == "")
                {
                    MessageBox.Show("Misiing Bill ID");
                }
                else
                {


                    con.Open();

                    string query = $"insert into Bill values ('{BillIdTB.Text}','{SellerNamelbl.Text}','{DateLabel.Text}','{AmountLabel.Text}')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");
                    con.Close();
                    int index = OrdersGV.SelectedRows.Count > 0 ? OrdersGV.SelectedRows[0].Index : -1;
                    //ProdNameTB.Text = ProdGV.SelectedRows[0].Cells[0].Value.ToString();
                    ReduceProductQuantity(OrdersGV.SelectedRows[index].Cells[1].Value.ToString(), Convert.ToInt32(OrdersGV.SelectedRows[index].Cells[3].Value));
                   
                    populateBills();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void BillGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //flag = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            e.Graphics.DrawString("SUPERMARKET", new Font("Century Gothic", 25,FontStyle.Bold), Brushes.Red,new Point(230));
            e.Graphics.DrawString("Bill ID: "+BillGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100,70));
            e.Graphics.DrawString("Seller Name: " + BillGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Bill Date: " + BillGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 130));

            e.Graphics.DrawString("Total Amount: " + BillGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("©RoqayaMohammad", new Font("Century Gothic", 20, FontStyle.Italic), Brushes.Red, new Point(230,230));

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select Name, Price, Quantity from Product where Category" + comboBox2.SelectedValue.ToString();
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide
                ();
            Form1 form= new Form1();
            form.Show();
        }
    }
}
