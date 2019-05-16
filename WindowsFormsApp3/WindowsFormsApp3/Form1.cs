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
    public partial class Form1 : Form
    {
        SqlConnection sqlt;
        SqlDataReader red = null;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\WindowsFormsApp3\WindowsFormsApp3\belon.mdf;Integrated Security=True";
            sqlt = new SqlConnection(s);
            await sqlt.OpenAsync();
            SqlCommand otprv = new SqlCommand("SELECT * FROM[belonn] ", sqlt);
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Id");
            listView1.Columns.Add("Name");
            listView1.Columns.Add("Fulname");
            listView1.Columns.Add("Day");
            try
            {
                red = await otprv.ExecuteReaderAsync();
                while (await red.ReadAsync())
                {
                    ListViewItem et = new ListViewItem(new string[] {
                       Convert.ToString(red["Id"]),
                       Convert.ToString(red["Name"]),
                       Convert.ToString(red["Fulname"]),
                       Convert.ToString(red["Day"])

                   });
                    listView1.Items.Add(et);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString(), exc.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (red != null && !red.IsClosed)
                {
                    red.Close();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlt != null && sqlt.State != ConnectionState.Closed)
            {
                sqlt.Close();
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();


            SqlCommand d = new SqlCommand("SELECT * FROM[belonn] ", sqlt);
            try
            {

                red = await d.ExecuteReaderAsync();
                while (await red.ReadAsync())
                {
                    ListViewItem et = new ListViewItem(new string[] {
                       Convert.ToString(red["Id"]),
                       Convert.ToString(red["Name"]),
                       Convert.ToString(red["Fulname"]),
                       Convert.ToString(red["Day"])

                   });
                    listView1.Items.Add(et);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString(), exc.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (red != null && !red.IsClosed)
                {
                    red.Close();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//insert
        {
            insert inst = new insert(sqlt);
            inst.Show();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                update upq = new update(sqlt, Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                upq.Show();
            }
            else
            {
                MessageBox.Show("Не одна строка была выделена в листе базы данных ");
            }
        }

        private async void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult q = MessageBox.Show("Вы хотите удалить его ?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);


                if (q == DialogResult.OK)
                {
                    SqlCommand udl = new SqlCommand("DELETE  FROM [belonn] WHERE [Id]=@Id", sqlt);
                    udl.Parameters.AddWithValue("Id", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                    try
                    {
                        await udl.ExecuteNonQueryAsync();


                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show(ep.Message.ToString(), ep.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }



                }
                } } }
    }
    