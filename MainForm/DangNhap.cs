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

namespace MainForm
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=5SCOMPUTER-PC\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True");

        public void btndangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string tk = txttendn.Text;
                string mk = txtmatkhau.Text;
                string sql = "select * from NguoiDung where TenDN='"+tk+"'and MatKhau='"+mk+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read() == true)
                {
                    Form1 frm1 = new Form1();
                    frm1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

        }

      

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
