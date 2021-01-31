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
using System.Threading;


namespace BloodBankMS
{
    public partial class Form1 : Form
    {
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
        }
        private string conString = "Data Source=DESKTOP-HU9VRD9;Initial Catalog=DbConnection;Integrated Security=True";

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                string query1 = "select * from LOGINTABLE where USERID = '" + txtusername.Text.ToString() + "'and PASSWORD = '" + txtpassword.Text.ToString() + "' and ACTIVE=1";
                SqlDataAdapter sda = new SqlDataAdapter(query1, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Close();
                    this.Hide();
                    thread = new Thread(newForm3);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    txtusername.BackColor = txtpassword.BackColor = Color.White;
                }
                else
                {
                    txtusername.BackColor = txtpassword.BackColor = Color.Tomato;
                    MessageBox.Show("Wrong username or password\nOR ID NOT ACTIVATED");
                }


            }
            catch (Exception ex)
            {
                txtusername.BackColor = txtpassword.BackColor = Color.Tomato;
                MessageBox.Show("Error...\nEnter userName and password");
            }

        }

        private void newForm3()
        {
            //MessageBox.Show(textBox1.Text.ToString());
            Application.Run(new Form3(txtusername.Text.ToString()));
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                thread = new Thread(newForm2);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();


            }
            catch (Exception ex)
            {

            }
        }

        private void newForm2()
        {
            Application.Run(new Form2());
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                thread = new Thread(newForm4);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

            }
            catch (Exception ex)
            {

            }
        }

        private void newForm4()
        {
            Application.Run(new Form4());

        }

    }
}
