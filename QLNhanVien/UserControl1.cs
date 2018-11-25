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

namespace NhanVien
{
    public partial class QLNhanVien: UserControl
    {
        public QLNhanVien()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=5SCOMPUTER-PC\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True");


        private void QLNhanVien_Load(object sender, EventArgs e)
        {
            txtma.Enabled = false;
            LoadPB();
            hienthi();
        }

        private void LoadPB()
        {
            con.Open();
            string sql = "Select * from PhongBan";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            cmbphongban.DataSource = data;
            cmbphongban.DisplayMember = "TenPB";
            cmbphongban.ValueMember = "ID";
            con.Close();
        }
        private void hienthi()
        {
            con.Open();
            string sql = "select IDNV,TenNV,TenPB,GioiTinh,NgaySinh,DiaChi,SDT,CMTND,Luong from PhongBan,NhanVien where PhongBan.ID=NhanVien.ID";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            con.Close();
            dataGrd.DataSource = data;
        }
        private void clear()
        {
            txtma.Text = "";
            txtten.Text = "";
            txtsdt.Text = "";
            txtdiachi.Text = "";
            datengaysinh.Text = "";
            txtcmtnd.Text = "";
            txtluong.Text = "";
            cmbphongban.Text = "";
            radionam.Checked = false;
            radionu.Checked = false;
            radiokhac.Checked = false;
            txttimkiem.Text = "";
        }

        private void dataGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            try
            {
                txtma.Text = dataGrd.Rows[d].Cells[0].Value.ToString();
                txtten.Text = dataGrd.Rows[d].Cells[1].Value.ToString();
                cmbphongban.Text = dataGrd.Rows[d].Cells[2].Value.ToString();
                if (dataGrd.Rows[d].Cells[3].Value.ToString() == "nam")
                {
                    radionam.Checked = true;
                }
                else
                {
                    if (dataGrd.Rows[d].Cells[3].Value.ToString() == "nu")
                    {
                        radionu.Checked = true;
                    }
                    else
                    {
                        radiokhac.Checked = true;
                    }
                }
                datengaysinh.Text = dataGrd.Rows[d].Cells[4].Value.ToString();
                txtdiachi.Text = dataGrd.Rows[d].Cells[5].Value.ToString();
                txtsdt.Text = dataGrd.Rows[d].Cells[6].Value.ToString();
                txtcmtnd.Text = dataGrd.Rows[d].Cells[7].Value.ToString();
                txtluong.Text = dataGrd.Rows[d].Cells[8].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Thao tác sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            hienthi();
        }
        private void them()
        {
            con.Open();
            string sql = "insert into NhanVien values(@tenNV,@gioitinh,@ngaysinh,@phongban,@diachi,@sdt,@cmtnd,@luong)";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("tenNV", txtten.Text);
            string gioitinh = "";
            if (radionam.Checked == true)
            {
                gioitinh = "nam";
            }
            else
            {
                if (radionu.Checked == true)
                {
                    gioitinh = "nữ";
                }
                else
                {
                    gioitinh = "khác";
                }
            }
            sqlCommand.Parameters.AddWithValue("gioitinh", gioitinh);
            sqlCommand.Parameters.AddWithValue("ngaysinh", datengaysinh.Text);
            sqlCommand.Parameters.AddWithValue("phongban", cmbphongban.SelectedValue);
            sqlCommand.Parameters.AddWithValue("diachi", txtdiachi.Text);
            sqlCommand.Parameters.AddWithValue("sdt", txtsdt.Text);
            sqlCommand.Parameters.AddWithValue("cmtnd", txtcmtnd.Text);
            sqlCommand.Parameters.AddWithValue("luong", txtluong.Text);
            sqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            hienthi();
            clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (txtten.Text == "" || txtten.Text.Length > 30)
            {
                MessageBox.Show("Tên nhân viên không được bỏ trống và không vượt quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                txtten.Text = "";
            }
            else
            {
                if (txtdiachi.Text == "" || txtdiachi.Text.Length > 100)
                {
                    MessageBox.Show("Địa chỉ không được bỏ trống và không vượt quá 100 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtdiachi.Text = "";
                }
                else
                {
                    if (txtsdt.Text.Length > 11 || !IsNumber(txtsdt.Text)||txtsdt.Text=="")
                    {
                        MessageBox.Show("SĐT không được bỏ trống, phải là số và không vượt quá 11 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        txtsdt.Text = "";
                    }
                    else
                    {
                        if (txtcmtnd.Text.Length > 13 || !IsNumber(txtcmtnd.Text) || txtcmtnd.Text == "")
                        {
                            MessageBox.Show("CMTND không được bỏ trống, phải là số và không vượt quá 13 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            txtcmtnd.Text = "";
                        }
                        else
                        {
                            if (!IsNumber(txtluong.Text) || txtluong.Text == "")
                            {
                                MessageBox.Show("Lương không được bỏ trống và phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                txtluong.Text = "";
                            }
                            else
                            {
                                if (radionam.Checked == false && radionu.Checked == false && radiokhac.Checked == false)
                                {
                                    MessageBox.Show("Vui lòng chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                }
                                else
                                {
                                    them();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void sua()
        {
            con.Open();
            string sql = "update NhanVien set TenNV=@tenNV,GioiTinh=@gioitinh,NgaySinh=@ngaysinh,ID=@phongban,DiaChi=@diachi,SDT=@sdt,CMTND=@cmtnd,Luong=@luong where IDNV=@id";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("id", txtma.Text);
            sqlCommand.Parameters.AddWithValue("tenNV", txtten.Text);
            string gioitinh = "";
            if (radionam.Checked == true)
            {
                gioitinh = "nam";
            }
            else
            {
                if (radionu.Checked == true)
                {
                    gioitinh = "nữ";
                }
                else
                {
                    gioitinh = "khác";
                }
            }
            sqlCommand.Parameters.AddWithValue("gioitinh", gioitinh);
            sqlCommand.Parameters.AddWithValue("ngaysinh", datengaysinh.Text);
            sqlCommand.Parameters.AddWithValue("phongban", cmbphongban.SelectedValue);
            sqlCommand.Parameters.AddWithValue("diachi", txtdiachi.Text);
            sqlCommand.Parameters.AddWithValue("sdt", txtsdt.Text);
            sqlCommand.Parameters.AddWithValue("cmtnd", txtcmtnd.Text);
            sqlCommand.Parameters.AddWithValue("luong", txtluong.Text);
            sqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            hienthi();
            clear();
        }

        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtten.Text == "" || txtten.Text.Length > 30)
            {
                MessageBox.Show("Tên nhân viên không được bỏ trống và không vượt quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                txtten.Text = "";
            }
            else
            {
                if (txtdiachi.Text == "" || txtdiachi.Text.Length > 100)
                {
                    MessageBox.Show("Địa chỉ không được bỏ trống và không vượt quá 100 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtdiachi.Text = "";
                }
                else
                {
                    if (txtsdt.Text.Length > 11 || !IsNumber(txtsdt.Text) || txtsdt.Text == "")
                    {
                        MessageBox.Show("SĐT không được bỏ trống, phải là số và không vượt quá 11 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        txtsdt.Text = "";
                    }
                    else
                    {
                        if (txtcmtnd.Text.Length > 13 || !IsNumber(txtcmtnd.Text) || txtcmtnd.Text == "")
                        {
                            MessageBox.Show("CMTND không được bỏ trống, phải là số và không vượt quá 13 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            txtcmtnd.Text = "";
                        }
                        else
                        {
                            if (!IsNumber(txtluong.Text) || txtluong.Text == "")
                            {
                                MessageBox.Show("Lương không được bỏ trống và phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                txtluong.Text = "";
                            }
                            else
                            {
                                sua();
                            }
                        }
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "delete from NhanVien where IDNV=@id";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("id", txtma.Text);
            sqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            hienthi();
            clear();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select IDNV,TenNV,TenPB,GioiTinh,NgaySinh,DiaChi,SDT,CMTND,Luong from PhongBan,NhanVien where PhongBan.ID=NhanVien.ID and TenNV=@tenNV";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("tenNV", txttimkiem.Text);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            con.Close();
            dataGrd.DataSource = data;
            txttimkiem.Text = "";
        }
    }
}
