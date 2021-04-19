using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventoryMgmt
{
    public partial class frm_Home : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();


        public frm_Home()
        {
            InitializeComponent();
        }

        private void btn_inv_Click(object sender, EventArgs e)
        {
            frm_inventory inv = new frm_inventory();
            //frm_inventory();
            inv.ShowDialog();
        }

        private void btn_user_Click(object sender, EventArgs e)
        {


            frm_user user = new frm_user();
            user.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_supplier_Click(object sender, EventArgs e)
        {
            frm_supplier supplier = new frm_supplier();
            supplier.ShowDialog();
        }

        private void btn_users_Click(object sender, EventArgs e)
        {


            frm_user user = new frm_user();
            user.ShowDialog();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {

        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            frm_purchaselist user = new frm_purchaselist();
            user.ShowDialog();
        }

        private void frm_Home_Load(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                Datashow();
                con.Close();
                con.Open();
            }
        }

        public void Datashow()
        {


            if (con.State != ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
            cmd = con.CreateCommand();
            cmd.CommandText = "Select count(iId) from db_inventory";
            //cmd.ExecuteNonQuery();
            Int32 rows_count = Convert.ToInt32(cmd.ExecuteScalar());
            lb_p_count.Text = rows_count.ToString();

            cmd.CommandText = "Select count(sId) from db_supplier";
            //cmd.ExecuteNonQuery();
            Int32 s_count = Convert.ToInt32(cmd.ExecuteScalar());
            lb_s_count.Text = s_count.ToString();

            cmd.CommandText = "Select count(uId) from db_user";
            Int32 u_count = Convert.ToInt32(cmd.ExecuteScalar());
            lb_u_count.Text = u_count.ToString();
            
            cmd.CommandText = "Select count(iId) from db_inventory where inv_qty <= 5 ";
            //cmd.ExecuteNonQuery();
            Int32 low_count = Convert.ToInt32(cmd.ExecuteScalar());
            lb_low_count.Text = low_count.ToString();

            cmd.CommandText = "Select iId as ID ,inv_name as Name, inv_qty as Qty from db_inventory where inv_qty <= 5";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;



        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }

            Datashow();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lb_u_count_Click(object sender, EventArgs e)
        {

        }
    }


}
