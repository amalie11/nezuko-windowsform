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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            showData();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\amali\source\repos\WindowsFormsApp2\WindowsFormsApp2\fix.mdf;Integrated Security=True;Connect Timeout=30");
        private void showData()
        {
            con.Open();
            string q = "select * from GB";
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fixDataSet.GB' table. You can move, or remove it, as needed.
            this.gBTableAdapter.Fill(this.fixDataSet.GB);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            
            con.Open();
            string q = "insert into GB values('" + id + "','" + txtUpdate.Text + "','" + txtDate.Text + "')";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");
            showData();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string qr = "Delete from GB where id = '" + txtID.Text + "' ";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted");
            showData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select * from GB where id like '" + txtSearch.Text + "%' ";
            SqlDataAdapter adap = new SqlDataAdapter(q, con);
            var ds = new DataSet();
            adap.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
            txtUpdate.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
            txtDate.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
            

        }
    }
}
