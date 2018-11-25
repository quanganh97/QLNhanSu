using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NhanVien;
using PhongBan;
using ThietBi;

namespace MainForm
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
      
        SqlConnection con = new SqlConnection(@"Data Source=5SCOMPUTER-PC\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
        
            
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            QLPhongBan phongBan = new QLPhongBan();
            phongBan.Size = panel.Size;
            panel.Controls.Add(phongBan);
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            QLNhanVien nhanVien = new QLNhanVien();
            nhanVien.Size = panel.Size;
            panel.Controls.Add(nhanVien);

        }

      
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            UserControl1 tb = new UserControl1();
            tb.Size = panel.Size;
            panel.Controls.Add(tb);
        }

      
    }
}
