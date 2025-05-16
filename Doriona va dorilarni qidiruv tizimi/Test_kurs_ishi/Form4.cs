using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test_kurs_ishi
{
    public partial class Form4 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DorixonaOddiy;Trusted_Connection=True;";
        private SqlConnection connection;
        private int id;

        public Form4(int id)
        {
            InitializeComponent();
            this.id = id;
            connection = new SqlConnection(connectionString);
            MalumotniYuklash();
            textBox1.ReadOnly = true; // Id o‘zgarmas
        }

        private void MalumotniYuklash()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM dbo.Dori_malumot WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["Id"].ToString();
                    textBox2.Text = reader["Dori_nomi"].ToString();
                    textBox3.Text = reader["Narxi"].ToString();
                    textBox4.Text = reader["Yaroqlilik_muddat"].ToString();
                    textBox5.Text = reader["Ishlab_chiqaruvchi"].ToString();
                    textBox6.Text = reader["Kategoriya"].ToString();
                    textBox7.Text = reader["Retsept_bilan"].ToString();
                    textBox8.Text = reader["Dori_soni"].ToString();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Foydalanuvchi barcha maydonlarni to‘ldirdimi?
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to‘ldiring!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Narx va soni to‘g‘rimi?
            decimal narxi;
            int doriSoni;
            if (!decimal.TryParse(textBox3.Text, out narxi))
            {
                MessageBox.Show("Narxi raqam bo‘lishi kerak!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox8.Text, out doriSoni) || doriSoni <= 0)
            {
                MessageBox.Show("Dori soni musbat raqam bo‘lishi kerak!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection.Open();
                string query = @"UPDATE dbo.Dori_malumot 
                                SET Dori_nomi = @DoriNomi, 
                                    Narxi = @Narxi, 
                                    Yaroqlilik_muddat = @Yorqillik, 
                                    Ishlab_chiqaruvchi = @IshlabChiq, 
                                    Kategoriya = @Kategoriya, 
                                    Retsept_bilan = @RetseptBil, 
                                    Dori_soni = @DoriSoni 
                                WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoriNomi", textBox2.Text.Trim());
                command.Parameters.AddWithValue("@Narxi", narxi);
                command.Parameters.AddWithValue("@Yorqillik", textBox4.Text.Trim());
                command.Parameters.AddWithValue("@IshlabChiq", textBox5.Text.Trim());
                command.Parameters.AddWithValue("@Kategoriya", textBox6.Text.Trim());
                command.Parameters.AddWithValue("@RetseptBil", textBox7.Text.Trim());
                command.Parameters.AddWithValue("@DoriSoni", doriSoni);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Dori muvaffaqiyatli yangilandi!", "Muvaffaqiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
