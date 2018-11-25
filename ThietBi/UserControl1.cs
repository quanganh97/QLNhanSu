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

namespace ThietBi
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=5SCOMPUTER-PC\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True");
        private void UserControl1_Load(object sender, EventArgs e)
        {
            txtma.Enabled = false;
            cmbtinhtrang.Items.Add("Mới");
            cmbtinhtrang.Items.Add  ("Còn tốt");
            cmbtinhtrang.Items.Add("Cũ");
            cmbtinhtrang.Items.Add("Hỏng");
            LoadPB();
            hienthi();
        }

        private void hienthi()
        {
            con.Open();
            string sql = "select IDTB,TenTB,TenPB,ThietBi.MoTa,TinhTrang from PhongBan,ThietBi where PhongBan.ID=ThietBi.ID";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            con.Close();
            dataGrd.DataSource = data;
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

        private void dataGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            try
            {
                txtma.Text = dataGrd.Rows[d].Cells[0].Value.ToString();
                txtten.Text = dataGrd.Rows[d].Cells[1].Value.ToString();
                cmbphongban.Text = dataGrd.Rows[d].Cells[2].Value.ToString();
                cmbtinhtrang.Text = dataGrd.Rows[d].Cells[4].Value.ToString();
                txtmota.Text = dataGrd.Rows[d].Cells[3].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Thao tác sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
           
        }
        private void clear()
        {
            txtma.Text = "";
            txtmota.Text = "";
            txtten.Text = "";
            txttimkiem.Text = "";
            cmbtinhtrang.Text = "";
            cmbphongban.Text = "";
        }

        private void btnnhaplai_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            hienthi();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (txtten.Text.Length > 30||txtten.Text=="")
            {
                MessageBox.Show("Tên thiết bị không bỏ trống và không quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            }
            else
            {
                if (txtmota.Text.Length > 100 || txtmota.Text == "")
                {
                    MessageBox.Show("Mô tả không bỏ trống và không quá 100 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    if (cmbphongban.Text == "")
                    {
                        MessageBox.Show("Chọn phòng ban", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    }
                    else
                    {
                        if (cmbtinhtrang.Text == "")
                        {
                            MessageBox.Show("Chọn tình trạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        }
                        else
                        {
                            con.Open();
                            string sql = "insert into ThietBi values(@tenTB,@phongban,@mota,@tinhtrang)";
                            SqlCommand sqlCommand = new SqlCommand(sql, con);
                            sqlCommand.Parameters.AddWithValue("tenTB", txtten.Text);
                            sqlCommand.Parameters.AddWithValue("phongban", cmbphongban.SelectedValue);
                            sqlCommand.Parameters.AddWithValue("mota", txtmota.Text);
                            sqlCommand.Parameters.AddWithValue("tinhtrang", cmbtinhtrang.Text);
                            sqlCommand.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            hienthi();
                            clear();
                        }
                    }                    
                }
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txtten.Text.Length > 30 || txtten.Text == "")
            {
                MessageBox.Show("Tên thiết bị không bỏ trống và không quá 30 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            }
            else
            {
                if (txtmota.Text.Length > 100 || txtmota.Text == "")
                {
                    MessageBox.Show("Mô tả không bỏ trống và không quá 100 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    if (cmbphongban.Text == "")
                    {
                        MessageBox.Show("Chọn phòng ban", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    }
                    else
                    {
                        if (cmbtinhtrang.Text == "")
                        {
                            MessageBox.Show("Chọn tình trạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        }
                        else
                        {
                            con.Open();
                            string sql = "update ThietBi set TenTB=@tenTB,ID=@phongban,MoTa=@mota,TinhTrang=@tinhtrang where IDTB=@id";
                            SqlCommand sqlCommand = new SqlCommand(sql, con);
                            sqlCommand.Parameters.AddWithValue("id", txtma.Text);
                            sqlCommand.Parameters.AddWithValue("tenTB", txtten.Text);
                            sqlCommand.Parameters.AddWithValue("phongban", cmbphongban.SelectedValue);
                            sqlCommand.Parameters.AddWithValue("mota", txtmota.Text);
                            sqlCommand.Parameters.AddWithValue("tinhtrang", cmbtinhtrang.Text);
                            sqlCommand.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            hienthi();
                            clear();
                        }
                    }
                }
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "delete from ThietBi where IDTB=@id";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("id", txtma.Text);
            sqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            hienthi();
            clear();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select IDTB,TenTB,TenPB,ThietBi.MoTa,TinhTrang from PhongBan,ThietBi where PhongBan.ID=ThietBi.ID and TenTB=@tenTB";
            SqlCommand sqlCommand = new SqlCommand(sql, con);
            sqlCommand.Parameters.AddWithValue("tenTB", txttimkiem.Text);
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
