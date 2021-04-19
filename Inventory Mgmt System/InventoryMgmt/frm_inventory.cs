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
    public partial class frm_inventory : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();

        public frm_inventory()
        {
            InitializeComponent();
        }

        private void frm_inventory_Load(object sender, EventArgs e)
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
            cmd.CommandText = "Select * from db_inventory";
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;



        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_contact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_website_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_supplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult close;
            close = MessageBox.Show("Confirm if you want to Close", "Inventory Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (close == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            con.Close();
            con.Open();

            try
            {
                if (txt_id.Text == "" || txt_name.Text == "" || txt_qty.Text == "" || txt_cost.Text == "" || txt_supplier.Text == "")
                {
                    MessageBox.Show("Please , Insert all Information .", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {



                    //auto identidy sid

                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select sid from db_supplier where supname='" + txt_supplier.Text + "'";
                    //cmd.ExecuteNonQuery();
                    Int32 sid = Convert.ToInt32(cmd.ExecuteScalar());


                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Insert Into db_inventory (iId,inv_name,inv_color,inv_pattern,inv_size,inv_qty,inv_cost,inv_price,inv_supplier,sid)Values('" + txt_id.Text + "','" + txt_name.Text + "','" + txt_color.Text + "','" + txt_pattern.Text + "','" + txt_size.Text + "','" + txt_qty.Text + "','" + txt_cost.Text + "','" + txt_price.Text + "','" + txt_supplier.Text + "','" + sid + "' )";
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

        private void btn_edit_Click_1(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            if (txt_name.Text == "")
            {
                MessageBox.Show("Select your Record", "Supplier Management");
                return;
            }

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update db_inventory Set iId='" + txt_id.Text + "',inv_name='" + txt_name.Text + "',inv_color='" + txt_color.Text + "',inv_pattern='" + txt_pattern.Text + "',inv_size='" + txt_size.Text + "',inv_qty='" + txt_qty.Text + "',inv_cost='" + txt_cost.Text + "',inv_price='" + txt_price.Text + "',inv_supplier='" + txt_supplier.Text + "'where iId = '" + txt_id.Text + "'";
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
            txt_color.Clear();
            txt_pattern.Clear(); 
            txt_size.Clear();
            txt_qty.Clear();
            txt_cost.Clear();
            txt_price.Clear();
            txt_supplier.Clear();
        }

        private void btn_remove_Click_1(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            if (txt_name.Text == "")
            {
                MessageBox.Show("Select your Record", "Supplier Management");
                return;
            }


            DialogResult delete;
            delete = MessageBox.Show("Confirm if you want to Delete", "Inventory Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "Delete db_inventory where inv_name='" + txt_name.Text + "' and iId = '" + txt_id.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted", "Inventory Management");
                Datashow();
                return;
               

            }
            else
            {
                MessageBox.Show("Record Not Deleted", "Inventory Management");
                Datashow();
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                txt_id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txt_name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_color.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_pattern.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txt_size.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txt_qty.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txt_cost.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txt_price.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txt_supplier.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }
    }
}
