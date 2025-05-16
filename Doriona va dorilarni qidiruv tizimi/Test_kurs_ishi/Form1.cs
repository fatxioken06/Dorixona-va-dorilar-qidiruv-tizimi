using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Test_kurs_ishi
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DorixonaOddiy;Trusted_Connection=True;";
        private SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            Yuklash();
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.RowHeadersWidth = 50;
        }

        private void Yuklash()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Dori_malumot ORDER BY Dori_nomi ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Qo‘shish
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            Yuklash();
        }

        private void button2_Click(object sender, EventArgs e) // O‘chirish
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Bu dorini o'chirmoqchimisiz?", "Tasdiqlash", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                        connection.Open();
                        string query = "DELETE FROM dbo.Dori_malumot WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Dori o'chirildi");
                        Yuklash();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Iltimos o'chirish uchun dori tanlang!");
            }
        }

        private void button3_Click_1(object sender, EventArgs e) // Izlash
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e) // Yangilash
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Iltimos yangilash uchun dori tanlang!");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            Form4 form4 = new Form4(id);
            form4.ShowDialog();
            Yuklash();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string raqam = (e.RowIndex + 1).ToString();
            var g = e.Graphics;
            var font = new Font("Segoe UI", 9);
            var brush = new SolidBrush(Color.Black);
            var textSize = g.MeasureString(raqam, font);
            float x = e.RowBounds.Left + (dataGridView1.RowHeadersWidth - textSize.Width) / 2;
            float y = e.RowBounds.Top + (e.RowBounds.Height - textSize.Height) / 2;
            g.DrawString(raqam, font, brush, x, y);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                if (Convert.ToInt32(row.Cells["Dori_soni"].Value) < 10)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Yuklash();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Iltimos, sotish uchun dori tanlang!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            string doriNomi = dataGridView1.SelectedRows[0].Cells["Dori_nomi"].Value.ToString();
            decimal narxi = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Narxi"].Value);
            Form5 form5 = new Form5(id, doriNomi, narxi);
            form5.ShowDialog();
            Yuklash();
        }






   
    }
}