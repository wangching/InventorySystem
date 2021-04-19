using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace InventoryMgmt
{
    public partial class frm_supplier : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        //string Image;

        public frm_supplier()
        {
            InitializeComponent();
        }

        private void frm_supplier_Load(object sender, EventArgs e)
        {

            //int x = new int();
            //int y = new int();
            //x = Screen.PrimaryScreen.WorkingArea.Width - 200;
            //y = Screen.PrimaryScreen.WorkingArea.Width - 200;
            //this.Location = new Point(x, y);



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
            cmd=con.CreateCommand();
            cmd.CommandText = "Select * from db_supplier";
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;



        }
            

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_fullname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_mobailno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_id.Text == ""|| txt_supplier.Text == "" || txt_phone.Text == "" || txt_contact.Text == "" || txt_website.Text == "" )
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
                    cmd.CommandText = "Insert Into db_supplier (sId,supname,supphone,supcontact,supweb)Values('" + txt_id.Text + "','" + txt_supplier.Text + "','" + txt_phone.Text + "','" + txt_contact.Text + "','" + txt_website.Text + "' )";
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

        private void btn_remove_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            if (txt_supplier.Text == "")
            {
                MessageBox.Show("Select your Record", "Supplier Management");
                return;
            }


            DialogResult delete;
            delete = MessageBox.Show("Confirm if you want to Delete", "Supplier Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "Delete db_supplier where supname='" + txt_supplier.Text +"' and sId = '" + txt_id.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted", "Supplier Management");
                Datashow();
                return;
                
            }
            else
            {
                MessageBox.Show("Record Not Deleted", "Supplier Management");
                Datashow();
            }
            Clear();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();

            if (txt_supplier.Text == "")
            {
                MessageBox.Show("Select your Record", "Supplier Management");
                return;
            }
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update db_supplier Set sId='"  + txt_id.Text + "',supname='" + txt_supplier.Text + "',supphone='" + txt_phone.Text + "',supcontact='" + txt_contact.Text + "',supweb='" + txt_website.Text + "'where sId = '" + txt_id.Text + "'";
            //cmd.Parameters.Add("Image", SqlDbType.Image).Value = Image;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated Record Sucessfully");
            Datashow();
            Clear();
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult close;
            close = MessageBox.Show("Confirm if you want to Close", "Supplier Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (close == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public void Clear()
        {
            txt_id.Clear() ;
            txt_supplier.Clear();
            txt_phone.Clear();
            txt_contact.Clear();
            txt_website.Clear();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txt_supplier.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_phone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_contact.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_website.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
