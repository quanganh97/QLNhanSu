using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PhongBan
{
    public partial class QLPhongBan : UserControl
    {
        public QLPhongBan()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=5SCOMPUTER-PC\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True");
        private void UserControl1_Load(object sender, EventArgs e)
        {
            hienthi();
            txtId.Enabled = false;
        }

        private void hienthi()
        {
            con.Open();
            string sql = "select * from PhongBan";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            con.Close();
            dataGrd.DataSource = data;
        }

        private void dataGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            try
            {
                txtId.Text = dataGrd.Rows[d].Cells[0].Value.ToString();
                txtTen.Text = dataGrd.Rows[d].Cells[1].Value.ToString();
                dateTimePic.Text = dataGrd.Rows[d].Cells[2].Value.ToString();
                txtMota.Text = dataGrd.Rows[d].Cells[3].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Thao tác sai", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
        }

        private void clear()
        {
            txtId.Text = "";
            txtTen.Text = "";
            txtMota.Text = "";
            txtTimkiem.Text = "";
            dateTimePic.Text = "";
        }
        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "")
            {
                MessageBox.Show("Tên phòng ban không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                txtTen.Text = "";
            }
            else
            {
                if (txtTen.Text.Length>30)
                {
                    MessageBox.Show("Tên phòng ban không vượt quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtTen.Text = "";
                }
                else
                {
                    if (txtMota.Text.Length > 150)
                    {
                        MessageBox.Show("Mô tả không vượt quá 150 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        txtMota.Text = "";
                    }
                    else
                    {
                        con.Open();
                        string sql = "insert into PhongBan values(@tenPB,@ngayTao,@moTa)";
                        SqlCommand sqlCommand = new SqlCommand(sql, con);
                        sqlCommand.Parameters.AddWithValue("tenPB", txtTen.Text);
                        sqlCommand.Parameters.AddWithValue("ngayTao", dateTimePic.Text);
                        sqlCommand.Parameters.AddWithValue("moTa", txtMota.Text);
                        sqlCommand.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        hienthi();
                        clear();
                    }
                }
            }          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "")
            {
                MessageBox.Show("Tên phòng ban không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                txtTen.Text = "";
            }
            else
            {
                if (txtTen.Text.Length > 30)
                {
                    MessageBox.Show("Tên phòng ban không vượt quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtTen.Text = "";
                }
                else
                {
                    if (txtMota.Text.Length > 150)
                    {
                        MessageBox.Show("Mô tả không vượt quá 150 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        txtMota.Text = "";
                    }
                    else
                    {
                        con.Open();
                        string sql = "update PhongBan set TenPB=@tenPB, NgayTao=@ngayTao, MoTa=@moTa where ID=@id";
                        SqlCommand sqlCommand = new SqlCommand(sql, con);
                        sqlCommand.Parameters.AddWithValue("id", txtId.Text);
                        sqlCommand.Parameters.AddWithValue("tenPB", txtTen.Text);
                        sqlCommand.Parameters.AddWithValue("ngayTao", dateTimePic.Text);
                        sqlCommand.Parameters.AddWithValue("moTa", txtMota.Text);
                        sqlCommand.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        hienthi();
                        clear();
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "delete from PhongBan where ID=@id";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("id", txtId.Text);
            sqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            hienthi();
            clear();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "Select * from PhongBan where TenPB=@tenPB";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("tenPB", txtTimkiem.Text);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            con.Close();
            dataGrd.DataSource = data;
            txtTimkiem.Text = "";
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            hienthi();
        }
    }
}
