using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DGVPrinterHelper;

namespace InventoryMgmt
{
    public partial class frm_purchaselist : Form
    {
        public frm_purchaselist()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ccw\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Datashow();
        }

        private void frm_purchaselist_Load(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                //Datashow();
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
            cmd.CommandText = "Select * from db_inventory Where inv_qty <= '" + Int32.Parse(txt_search.Text) + "'";
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult close;
            close = MessageBox.Show("Confirm if you want to Close", "Purchase List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (close == DialogResult.Yes)
            {
                this.Close();
            }
        }

        string name, color, size, supplier; int  qty, flag; float cost, totprice, total=0;

        float amount;

        private void btn_prt_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Purchase List";
            printer.SubTitle = string.Format("Date: {0} ", DateTime.Now);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = " Total Payable Amount : " + lbl_rst.Text;
            printer.FooterSpacing = 15;
          //printer.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView1);

            total = 0;
            dataGridView1.Rows.Clear();
            lbl_rst.Text = "Rs." + total;



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { amount = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                    
             }
              catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            try {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            total -= amount;
            lbl_rst.Text = "Rs." + total;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                name = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                color = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                size = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                //qty = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[5].Value.ToString());
                cost = float.Parse(dataGridView2.SelectedRows[0].Cells[6].Value.ToString());
                supplier = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();

                flag = 1;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        protected int n, num;
        private void btn_add_Click(object sender, EventArgs e)
        {

            try
            {

                //n= dataGridView1
                if (txt_qty.Text == "")
                    MessageBox.Show("Enter Quentity of Product");
                else if (flag == 0)
                    MessageBox.Show(" Select the Product");
                else
                {
                    num = num + 1;
                    qty = Convert.ToInt32(txt_qty.Text);
                    totprice = qty * cost;
                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = num;
                    dataGridView1.Rows[n].Cells[1].Value = name;
                    dataGridView1.Rows[n].Cells[2].Value = color;
                    dataGridView1.Rows[n].Cells[3].Value = size;
                    dataGridView1.Rows[n].Cells[4].Value = qty;
                    dataGridView1.Rows[n].Cells[5].Value = cost;
                    dataGridView1.Rows[n].Cells[6].Value = supplier;
                    dataGridView1.Rows[n].Cells[7].Value = totprice;

                    flag = 0;

                    total += totprice;
                    lbl_rst.Text = "Rs." + total;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        

        



        }
    }
}
