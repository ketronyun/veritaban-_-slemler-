
using MySql.Data.MySqlClient;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace veritabanı
{
    public partial class Form1 : Form
    {
        string connStr = "server=localhost;user=root;database=bagla;port=3306;password=12345678;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT ad,soyad,yas FROM isim";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ad = reader.GetString("ad");
                        string soyad = reader.GetString("soyad");
                        int yaş = reader.GetInt32("yas");

                        dataGridView1.Rows.Add(ad, soyad, yaş);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // textboxlardan değerleri al
            string ad = textBox1.Text.Trim();
            string soyad = textBox2.Text.Trim();
            int yas;

            // yaş sayıya çevrilebilir mi kontrol et
            if (!int.TryParse(textBox3.Text, out yas))
            {
                MessageBox.Show("Lütfen geçerli bir yaş giriniz!");
                return;
            }

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO isim (ad, soyad, yas) VALUES (@ad, @soyad, @yas)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@yas", yas);

                    cmd.ExecuteNonQuery(); // veriyi ekler
                }
            }

            MessageBox.Show("Kayıt başarıyla eklendi!");

            // DataGridView’e yeni satırı ekle (ekleme sonrası listeyi güncellemek için)
            dataGridView1.Rows.Add(ad, soyad, yas);

            // TextBoxları temizle
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

    }
}




