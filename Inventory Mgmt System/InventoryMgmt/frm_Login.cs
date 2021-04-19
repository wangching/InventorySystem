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

namespace InventoryMgmt
{
    public partial class frm_Login : Form
    {


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        SqlCommand cmd = new SqlCommand();

        //DataTable dt = new DataTable();

        public frm_Login()
        {
            InitializeComponent();
        }

       



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_showpassword.Checked)
            {
                txt_Password.UseSystemPasswordChar = true;
            }
            else
            {
                txt_Password.UseSystemPasswordChar = false;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Username.Text == "" || txt_Password.Text == "")
                {
                    MessageBox.Show("Please Enter username and password", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select * From db_user Where username = '" + txt_Username.Text.Trim() + "' and upassword = '" + txt_Password.Text.Trim() + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        frm_Home fh = new frm_Home();
                        fh.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Yor are Loggin Detail Is Invalied", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //frm_Home fh = new frm_Home();
                    //fh.Show();
                    //this.Hide();

                    

                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
