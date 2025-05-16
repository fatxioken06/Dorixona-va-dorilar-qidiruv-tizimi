using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test_kurs_ishi
{
    public partial class Form3 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DorixonaOddiy;Trusted_Connection=True;";
        private SqlConnection connection;
        private Form1 parentForm;

        public Form3(Form1 parent)
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            parentForm = parent;

            // comboBox1 ni boshlanishda "teng" qilib qo'yamiz (3-chi indeks)
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 2;
        }

        private string GetComparisonOperator(string selected)
        {
            switch (selected.ToLower())
            {
                case "katta": return ">";
                case "kichik": return "<";
                case "teng": return "=";
                case "katta yoki teng": return ">=";
                case "kichik yoki teng": return "<=";
                case "teng emas": return "<>";
                default: return "=";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string whereClause = "";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                whereClause += "Id LIKE @Id AND ";
                command.Parameters.AddWithValue("@Id", "%" + textBox1.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                whereClause += "Dori_nomi LIKE @DoriNomi AND ";
                command.Parameters.AddWithValue("@DoriNomi", "%" + textBox2.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                decimal narx;
                if (decimal.TryParse(textBox3.Text.Trim(), out narx))
                {
                    string op = GetComparisonOperator(comboBox1.Text); // comboBox1 — Narxi uchun
                    whereClause += "Narxi " + op + " @Narxi AND ";
                    command.Parameters.AddWithValue("@Narxi", narx);
                }
                else
                {
                    MessageBox.Show("Narx noto‘g‘ri kiritilgan!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                whereClause += "Yaroqlilik_mud LIKE @Yaroqlilik AND ";
                command.Parameters.AddWithValue("@Yaroqlilik", "%" + textBox4.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox5.Text))
            {
                whereClause += "Ishlab_chiqar LIKE @IshlabChiq AND ";
                command.Parameters.AddWithValue("@IshlabChiq", "%" + textBox5.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox6.Text))
            {
                whereClause += "Kategoriya LIKE @Kategoriya AND ";
                command.Parameters.AddWithValue("@Kategoriya", "%" + textBox6.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox7.Text))
            {
                whereClause += "Retsept_bilan LIKE @RetseptBil AND ";
                command.Parameters.AddWithValue("@RetseptBil", "%" + textBox7.Text.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(textBox8.Text))
            {
                int doriSoni;
                if (int.TryParse(textBox8.Text.Trim(), out doriSoni))
                {
                    string op = GetComparisonOperator(comboBox2.Text); // comboBox2 — Dori_soni uchun
                    whereClause += "Dori_soni " + op + " @DoriSoni AND ";
                    command.Parameters.AddWithValue("@DoriSoni", doriSoni);
                }
                else
                {
                    MessageBox.Show("Dori soni noto‘g‘ri kiritilgan!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string query = "SELECT * FROM dbo.Dori_malumot";
            if (whereClause.Length > 0)
            {
                whereClause = whereClause.Substring(0, whereClause.Length - 5); // " AND " ni olib tashlash
                query += " WHERE " + whereClause;
            }

            query += " ORDER BY Dori_nomi ASC";
            command.CommandText = query;

            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                parentForm.dataGridView1.DataSource = dt;
                connection.Close();
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
