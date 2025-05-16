using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test_kurs_ishi
{
    public partial class Form5 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DorixonaOddiy;Trusted_Connection=True;";
        private SqlConnection connection;
        private int id;
        private decimal narxi;
        private int maxDoriSoni;

        public Form5(int id, string doriNomi, decimal narxi)
        {
            InitializeComponent();
            this.id = id;
            this.narxi = narxi;
            connection = new SqlConnection(connectionString);

            textBox1.Text = doriNomi;
            textBox2.Text = narxi.ToString("F2");
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
            numericUpDown1.Minimum = 1;

            YuklashMaxDoriSoni(); // Maksimal sonni yuklash
            numericUpDown1.Value = 1; // Boshlang‘ich qiymat
            numericUpDown1_ValueChanged(null, null); // Umumiy summani darhol hisoblash
        }

        private void YuklashMaxDoriSoni()
        {
            try
            {
                connection.Open();
                string query = "SELECT Dori_soni FROM dbo.Dori_malumot WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    maxDoriSoni = Convert.ToInt32(result);
                    numericUpDown1.Maximum = maxDoriSoni;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int soni = (int)numericUpDown1.Value;
            decimal umumiySumma = soni * narxi;
            textBox3.Text = umumiySumma.ToString("F2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sotilganSoni = (int)numericUpDown1.Value;
            try
            {
                connection.Open();

                // Joriy mavjud sonni olish
                string checkQuery = "SELECT Dori_soni FROM dbo.Dori_malumot WHERE Id = @Id";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Id", id);
                int joriySoni = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (joriySoni < sotilganSoni)
                {
                    MessageBox.Show("Yetarli miqdor mavjud emas!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return;
                }

                // Qolgan dorilar sonini yangilash
                int yangiSoni = joriySoni - sotilganSoni;
                string updateQuery = "UPDATE dbo.Dori_malumot SET Dori_soni = @YangiSoni WHERE Id = @Id";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@YangiSoni", yangiSoni);
                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Dori muvaffaqiyatli sotildi!\nUmumiy summa: " + textBox3.Text, "Muvaffaqiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
