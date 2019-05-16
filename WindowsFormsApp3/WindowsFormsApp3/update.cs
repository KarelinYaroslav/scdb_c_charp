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
    public partial class update : Form
    {
        private SqlConnection sqltt;
        private int id;

        public update(SqlConnection sq,int id)
        {
            InitializeComponent();
            sqltt = sq;
            this.id = id;

        }

        private async void update_Load(object sender, EventArgs e)
        {
            SqlCommand co = new SqlCommand("SELECT [Name], [Fulname], [Day] FROM [belonn] WHERE [Id]=@Id", sqltt);
            co.Parameters.AddWithValue("Id", id);
            SqlDataReader re = null;
            try
            {
                re = await co.ExecuteReaderAsync() ;
                while (await re.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(re["Name"]);
                    textBox3.Text = Convert.ToString(re["Fulname"]);
                    textBox2.Text = Convert.ToString (re["Day"]);

                }
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.Message.ToString(), ext.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally {
                if (re != null && !re.IsClosed) {
                    re.Close();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand upd = new SqlCommand("UPDATE [belonn] SET [Name]=@Name, [Fulname]=@Fulname, [Day]=@Day WHERE [Id]=@Id", sqltt);
            upd.Parameters.AddWithValue("Name", textBox1.Text);
            upd.Parameters.AddWithValue("Fulname", textBox3.Text);
            upd.Parameters.AddWithValue("Day", Convert.ToInt32(textBox2.Text));
            upd.Parameters.AddWithValue("Id", id);
            try
            {
                await upd.ExecuteNonQueryAsync();

            }
            catch (Exception el) {
                MessageBox.Show(el.Message.ToString(), el.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
