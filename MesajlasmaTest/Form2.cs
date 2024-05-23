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

namespace MesajlasmaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=NTB1000088;Initial Catalog=MesajlasmaDb;User ID=sa;Password=sa321sa?;");

        void gelenkutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From TblMesajlar WHERE alici=" + numara, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * From TblMesajlar where Gonderen=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            gelenkutusu();
            gidenkutusu();

            //Ad Soyadı Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Ad,Soyad from TblKisiler where numara=" + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();   
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblMesajlar (gonderen,alici,baslik,icerik) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız iletildi.");
            gidenkutusu();


        }
    }
}
