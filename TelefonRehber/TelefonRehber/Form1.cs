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

namespace TelefonRehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        
            
        }
      SqlConnection bgl = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=rehberapp;Integrated Security=True;Connect Timeout=30;");
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=rehberapp;Integrated Security=True;Connect Timeout=30; 
       
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From rehbertable",bgl);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            Txt_Ad.Text = "";
            Txt_Id.Text = "";
            Txt_Mail.Text = "";
            Txt_Soyad.Text = "";
            Txt_Ad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();

   
        }

        private void Btn_ekle_Click(object sender, EventArgs e)
        {
            bgl.Open();

            SqlCommand komut = new SqlCommand("insert into rehbertable (AD,SOYAD,TELEFON,MAİL,PROFİLFOTO) values " +
                "(@P1,@P2,@P3,@P4,@P5)",bgl);
            komut.Parameters.AddWithValue("@P1", Txt_Ad.Text);
            komut.Parameters.AddWithValue("@P2", Txt_Soyad .Text);
            komut.Parameters.AddWithValue("@P3",Msk_telefon.Text);
            komut.Parameters.AddWithValue("@P4", Txt_Mail.Text);
            komut.Parameters.AddWithValue("@P5",label7.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi sisteme kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void Btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txt_Id.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
           Txt_Ad .Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
           Txt_Soyad .Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Msk_telefon .Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
         Txt_Mail  .Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label7.Text=dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            pictureBox1.ImageLocation = label7.Text;
        }

        private void Btn_sil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand kmt = new SqlCommand("Delete from rehbertable where ID=" + Txt_Id.Text, bgl);
            kmt.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi rehberden silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
            temizle();


        }

        private void Btn_güncelle_Click(object sender, EventArgs e)
        {

            bgl.Open();
            SqlCommand kmt = new SqlCommand("update   rehbertable set AD=@p1,SOYAD=@p2,TELEFON=@p3,MAİL=@p4,PROFİLFOTO=@p5 where ID=" + Txt_Id.Text, bgl);
            kmt.Parameters.AddWithValue("@p1", Txt_Ad.Text);
            kmt.Parameters.AddWithValue("@p2",Txt_Soyad.Text);
            kmt.Parameters.AddWithValue("@p3",Msk_telefon.Text);
            kmt.Parameters.AddWithValue("@p4",Txt_Mail.Text);
            kmt.Parameters.AddWithValue("@p5", label7.Text);
            kmt.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi rehberden güncellendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            openFileDialog1.ShowDialog(this);
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            label7.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            openFileDialog1.ShowDialog(this);
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            label7.Text = openFileDialog1.FileName;
        }
    }
}
