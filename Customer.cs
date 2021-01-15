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

namespace Ado.Net_sql
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }


        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            var conn = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
            var comm = new SqlCommand("Select  * from Customer", conn);

            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                listBox1.Items.Add(reader["Name"]);
                
            }

            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
                SqlCommand comm = new SqlCommand("Insert into Customer values('" + txtCustomername.Text + "','" + txtdateTimePicker1.Value + "','" + txtcustomerEmail.Text + "','" + txtCustomercontact.Text + "')", conn); // jis series  of table mai databae mai hai wo hi series coding m ani chahyea
              //  comm.CommandText = "Insert into Customer values('" + txtCustomername.Text + "','" + txtdateTimePicker1.Value + "','" + txtcustomerEmail.Text + "','" + txtCustomercontact.Text + "')";

             //   comm.Connection = conn;
                conn.Open();
                comm.ExecuteNonQuery(); 
                conn.Close();
                UpdateListBox();
                MessageBox.Show("Customer Added Successfully", "Hurraay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomername.Text = txtCustomercontact.Text =  txtcustomerEmail.Text = "";
                txtdateTimePicker1.Value = System.DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Updatebutton.Enabled = false;
            Deletebutton.Enabled = false;
            button1.Enabled = true;

           //MessageBox.Show("Cstomer Added Successfolly", "Hurraay", MessageBoxButtons.OK, MessageBoxIcon.Information);
           //txtCustomername.Text = txtCustomercontact.Text = txtCustomerAddress.Text = txtcustomerEmail.Text = "";
           //txtdateTimePicker1.Value = System.DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            SqlConnection connlist = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
            SqlCommand commlist = new SqlCommand("Select * from Customer",connlist);
           
            connlist.Open();
           
            SqlDataReader reader = commlist.ExecuteReader();
            button1.Enabled = true;
            button3.Enabled = true;
            Deletebutton.Enabled = false;
            Updatebutton.Enabled = false;
            
            while (reader.Read())
            {
                listBox1.Items.Add(reader["Name"]);
                
            }
            connlist.Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
            SqlCommand comm1 = new SqlCommand("Select * from Customer where Name ='"+ listBox1.SelectedItem+"'",conn1);
          
            button1.Enabled = false;
            button3.Enabled = true;

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Id", typeof(int));


            //DataRow dr = dt.NewRow();
            //dr["Name"] = listBox1.SelectedItem;
            ////dr["Id"] =  listBox1.SelectedItem;

            //dt.Rows.Add(dr);

            //dataGridView1.DataSource = dt;
            conn1.Open();
            SqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
               txtCustomercontact.Text = reader["ContactNo"].ToString();
               txtCustomername.Text = reader["Name"].ToString();
               txtcustomerEmail.Text = reader["Email"].ToString();
               txtdateTimePicker1.Value = Convert.ToDateTime(reader["DOB"]);
               txtid.Text = reader["Id"].ToString();

            }
            conn1.Close();
            if (txtEnable.Text == "123")
            {
                Deletebutton.Enabled= true;
                Updatebutton.Enabled = true;
                button1.Enabled = false;
                txtCustomername.Enabled = true;
                txtcustomerEmail.Enabled = true;
                txtCustomercontact.Enabled = true;
                txtid.Enabled = false;
                txtdateTimePicker1.Enabled = true;
              

            }
            else
            {
                Updatebutton.Enabled = false;
                Deletebutton.Enabled = false;
                button1.Enabled = false;

                txtCustomername.Enabled = false;
                txtcustomerEmail.Enabled = false;
                txtCustomercontact.Enabled = false;
                txtid.Enabled = false;
                txtdateTimePicker1.Enabled = false;
               
            } 
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var conn = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
                var comm = new SqlCommand("Update Customer Set Name ='" + txtCustomername.Text + "', Email = '" + txtcustomerEmail.Text + "', ContactNo = '" + txtCustomercontact.Text + "', DOB = '" + txtdateTimePicker1.Value+ "' where Name = '" + listBox1.SelectedItem + "'", conn);

                conn.Open();

                comm.ExecuteNonQuery();

                conn.Close();

                UpdateListBox();

                txtCustomercontact.Text = txtcustomerEmail.Text = txtCustomername.Text =txtid.Text= "";
                txtdateTimePicker1.Value = System.DateTime.Now;

                MessageBox.Show("Customer Updated Successfully");
            }
            button1.Enabled = true;
            Deletebutton.Enabled = false;
            Updatebutton.Enabled = false;
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you Sure!", "Confirm", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                var conn = new SqlConnection("Data Source=DESKTOP-B77S1AJ;Initial Catalog=OrderManagementSystemDataBase;Integrated Security=True;");
                var comm = new SqlCommand("Delete from Customer where Name = '" + listBox1.SelectedItem + "'", conn);
                try
                {
                    conn.Open();

                    comm.ExecuteNonQuery();

                    conn.Close();

                    UpdateListBox();

                    txtCustomercontact.Text = txtcustomerEmail.Text = txtid.Text=txtCustomername.Text = "";
                    txtdateTimePicker1.Value = System.DateTime.Now;

                   
                    MessageBox.Show("Customer Deleted Successfully");


                    button1.Enabled = true;
                    Deletebutton.Enabled = false;
                    Updatebutton.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtCustomername.Clear();
            txtCustomercontact.Clear();
            txtcustomerEmail.Clear();
            button1.Enabled = true;
            Deletebutton.Enabled = false;
            Updatebutton.Enabled = false;
            txtCustomername.Enabled = true;
            txtcustomerEmail.Enabled = true;
            txtCustomercontact.Enabled = true;
            txtid.Enabled = false;
            txtdateTimePicker1.Enabled = true;
               
        }

        private void txtEnable_TextChanged(object sender, EventArgs e)
        {
            if (txtEnable.Text == "123")
            {
                Enter_button.Enabled = true;
            }
        }

        private void Enter_button_Click(object sender, EventArgs e)
        {

            if (txtEnable.Text == "123")
            {
                Updatebutton.Enabled = true;
                Deletebutton.Enabled = true;
               button1.Enabled = true;

            }
            else
            {
                MessageBox.Show("Sorry... WrongPassword", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
