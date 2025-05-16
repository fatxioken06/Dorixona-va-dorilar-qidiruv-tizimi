using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test_kurs_ishi
{
    public partial class Form2 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DorixonaOddiy;Trusted_Connection=True;";
        private SqlConnection connection;

        public Form2()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Barcha maydonlarni tekshirish
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

            // Ma'lumotlarni olish
            string doriNomi = textBox2.Text.Trim();

            decimal narxi;
            if (!decimal.TryParse(textBox3.Text, out narxi))
            {
                MessageBox.Show("Narxi raqam bo‘lishi kerak!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string yorqillik = textBox4.Text.Trim();
            string ishlabChiq = textBox5.Text.Trim();
            string kategoriya = textBox6.Text.Trim();
            string retseptBil = textBox7.Text.Trim();

            int doriSoni;
            if (!int.TryParse(textBox8.Text, out doriSoni) || doriSoni <= 0)
            {
                MessageBox.Show("Dori soni musbat raqam bo‘lishi kerak!", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection.Open();
                string query = @"INSERT INTO dbo.Dori_malumot (Dori_nomi, Narxi, Yaroqlilik_muddat, Ishlab_chiqaruvchi, Kategoriya, Retsept_bilan, Dori_soni) 
                                VALUES (@DoriNomi, @Narxi, @Yorqillik, @IshlabChiq, @Kategoriya, @RetseptBil, @DoriSoni)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoriNomi", doriNomi);
                command.Parameters.AddWithValue("@Narxi", narxi);
                command.Parameters.AddWithValue("@Yorqillik", yorqillik);
                command.Parameters.AddWithValue("@IshlabChiq", ishlabChiq);
                command.Parameters.AddWithValue("@Kategoriya", kategoriya);
                command.Parameters.AddWithValue("@RetseptBil", retseptBil);
                command.Parameters.AddWithValue("@DoriSoni", doriSoni);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Dori muvaffaqiyatli qo‘shildi!", "Muvaffaqiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
