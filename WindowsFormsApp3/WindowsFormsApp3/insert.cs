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


namespace WindowsFormsApp3
{
    public partial class insert : Form
    {
        private SqlConnection sqltt;
        public insert(SqlConnection sql )
        {
            InitializeComponent();
            sqltt = sql;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand otv = new SqlCommand("INSERT INTO [belonn](Name,Fulname,Day)VALUES(@Name,@Fulname,@Day)", sqltt);
            otv.Parameters.AddWithValue("Name", textBox2.Text);
            otv.Parameters.AddWithValue("Fulname", textBox3.Text);
            otv.Parameters.AddWithValue("Day", Convert.ToInt32(textBox4.Text));
            try {
                await otv.ExecuteNonQueryAsync();

            }
            catch (Exception ec) {
                MessageBox.Show(ec.Message.ToString(), ec.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
