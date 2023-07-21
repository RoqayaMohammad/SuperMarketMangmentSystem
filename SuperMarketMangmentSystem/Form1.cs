using System.Data;
using System.Data.SqlClient;

namespace SuperMarketMangmentSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new(@"Data Source=.;Initial Catalog=SMarketDB;Integrated Security=True");


        public static string sellerName = "";


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
          

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }



        private void label6_Click(object sender, EventArgs e)
        {
            UserNameTB.Text = "";
            PassTB.Text = "";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(UserNameTB.Text=="" || PassTB.Text=="")
            {
                MessageBox.Show("Please Enter Your UserNAme and Password");
            }
            else
            {
                if (RoleCB.SelectedIndex > -1)
                {
                    if (RoleCB.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UserNameTB.Text == "Admin" && PassTB.Text == "Admin")
                        {
                            ProductForm form = new ProductForm();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("YOU ARE NOT AN ADMIN");
                        }
                    }
                    else if (RoleCB.SelectedItem.ToString() == "SELLER")
                    {
                       // MessageBox.Show("");
                       con.Open();
                        SqlDataAdapter sda=new SqlDataAdapter($"select count(8) from Seller where Name='{UserNameTB.Text}' and Password='{PassTB.Text}'",con);
                        DataTable dt= new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString()=="1")
                        {
                            sellerName = UserNameTB.Text;
                            SellingForm sell=new SellingForm();
                            sell.Show();
                            this.Hide();
                            con.Close();

                        }
                        else
                        {
                            MessageBox.Show("Wrog UserName Or Password");

                        }
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Your Role");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}