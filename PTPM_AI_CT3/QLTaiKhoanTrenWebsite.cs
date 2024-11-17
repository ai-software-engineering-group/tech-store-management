using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace PTPM_AI_CT3
{
    public partial class QLTaiKhoanTrenWebsite : Form
    {
        UserBLL userBLL = new UserBLL();
        public QLTaiKhoanTrenWebsite()
        {
            InitializeComponent();
            this.Load += QLTaiKhoanTrenWebsite_Load;
        }

        private void QLTaiKhoanTrenWebsite_Load(object sender, EventArgs e)
        {
            loadDB();
        }
        public void loadDB()
        {
            dgv_DSTK.DataSource = userBLL.GetUser();
            dgv_DSTK.Columns["UserId"].HeaderText = "Mã người dùng";
            dgv_DSTK.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgv_DSTK.Columns["PasswordHash"].HeaderText = "Mật khẩu mã hóa";
            dgv_DSTK.Columns["Email"].HeaderText = "Email";
            dgv_DSTK.Columns["EmailConfirmed"].HeaderText = "Email được xác nhận";
            dgv_DSTK.Columns["Phone"].HeaderText = "Số điện thoại";
            dgv_DSTK.Columns["PhoneConfirmed"].HeaderText = "Số điện thoại được xác nhận";
            dgv_DSTK.Columns["Avatar"].HeaderText = "Ảnh đại diện";
            dgv_DSTK.Columns["RandomKey"].HeaderText = "Khóa ngẫu nhiên";
            dgv_DSTK.Columns["FullName"].HeaderText = "Tên người dùng";
            dgv_DSTK.Columns["DOB"].HeaderText = "Ngày sinh";
            dgv_DSTK.Columns["Gender"].HeaderText = "Giới tính";
            dgv_DSTK.Columns["IsActive"].HeaderText = "Còn hoạt động";
            dgv_DSTK.Columns["RoleId"].HeaderText = "Vai trò";
            dgv_DSTK.Columns["AuthenticationProvider"].HeaderText = "Nhà cung cấp xác thực";
            dgv_DSTK.Columns["EmployeeId"].HeaderText = "Mã nhân viên";
            dgv_DSTK.Columns["GroupId"].HeaderText = "Mã nhóm";

            dgv_DSTK.Columns["Employee"].Visible = false;
            dgv_DSTK.Columns["Role"].Visible = false;
            dgv_DSTK.Columns["UserGroup"].Visible = false;
            dgv_DSTK.Columns["UserGroup1"].Visible = false;
        }
    }
}
