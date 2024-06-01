using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace produktif_hotel
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        Function func = new Function();

        void Login()
        {
            try
            {
                if (func.conn.State == ConnectionState.Closed) func.conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT id_user, tipe_user, nama FROM tbl_user WHERE username = '"+txtUser.Text+"' AND password = '"+txtPw.Text+"'", func.conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        Function.id_user = dr["id_user"].ToString();
                        Function.nama = dr["nama"].ToString();
                              
                        DialogResult d;
                        d = MessageBox.Show("Login Anda Berhasil", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_log(waktu, id_user, nama) VALUES (GETDATE(), '"+Function.id_user+"', '"+Function.nama+"')", func.conn);
                        cmd.ExecuteNonQuery();

                        if(d == DialogResult.OK)
                        {
                            if (dr["tipe_user"].ToString() == "Admin")
                            {
                                //next page
                            }
                        }
                    }
                }
                else if(txtUser.Text == string.Empty || txtPw.Text == string.Empty)
                {
                    MessageBox.Show("Isi form dengan Lengkap", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Data tidak Valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShow.Checked)
            {
                txtPw.PasswordChar = '\0';
            }
            else
            {
                txtPw.PasswordChar = '*';
            }
        }
    }
}
