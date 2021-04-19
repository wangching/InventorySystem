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
    public partial class frm_user : Form
    {
        public frm_user()
        {
            InitializeComponent();
        }
        
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_USER_Load(object sender, EventArgs e)
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
            cmd.CommandText = "Select * from db_user";
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;



        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_id.Text == "" || txt_name.Text == "" || txt_password.Text == "" )
                {
                    MessageBox.Show("Please , Insert all Information .", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    //byte[] Image = null;
                    //FileStream stream = new FileStream(Imagename, FileMode.Open, FileAccess.Read);
                    //BinaryReader bar = new BinaryReader(stream);
                    //Image = bar.ReadBytes((int)atream.Length);


                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Insert Into db_user (uId,username,ufullname,upassword,umobailno)Values('" + txt_id.Text + "','" + txt_name.Text + "','" + txt_fullname.Text + "','" + txt_password.Text + "','" + txt_mobile.Text  + "' )";
                    //cmd.Parameters.Add("Image", SqlDbType.Image).Value = Image;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inserted Record Sucessfully");
                    con.Close();
                    Datashow();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult close;
            close = MessageBox.Show("Confirm if you want to Close", "User Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (close == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void db_userDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            if (txt_name.Text == "")
            {
                MessageBox.Show("Select your Record", "User Management");
                return;
            }

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update db_user Set uId='" + txt_id.Text + "',username='" + txt_name.Text + "',ufullname='" + txt_fullname.Text + "',upassword='" + txt_password.Text + "',umobailno='" + txt_mobile.Text + "'where uId = '" + txt_id.Text + "'";
            //cmd.Parameters.Add("Image", SqlDbType.Image).Value = Image;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated Record Sucessfully");
            Datashow();
            Clear();
            con.Close();
        }

        public void Clear()
        {
            txt_id.Clear();
            txt_name.Clear();
            txt_fullname.Clear();
            txt_password.Clear();
            txt_mobile.Clear();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            if (txt_name.Text == "")
            {
                MessageBox.Show("Select your Record", "User Management");
                return;
            }


            DialogResult delete;
            delete = MessageBox.Show("Confirm if you want to Delete", "User Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "Delete db_user where username='" + txt_name.Text + "' and uId = '" + txt_id.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted", "User Management");
                Datashow();
                return;

            }
            else
            {
                MessageBox.Show("Record Not Deleted", "User Management");
                Datashow();
            }
            Clear();
        }

        private void db_userDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txt_name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_fullname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_password.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_mobile.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
