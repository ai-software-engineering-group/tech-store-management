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
    public partial class QLDanhMucSanPham : Form
    {
        MenuBLL menuBLL = new MenuBLL();
        Menu1BLL menu1BLL = new Menu1BLL();
        Menu2BLL menu2BLL = new Menu2BLL();
        public QLDanhMucSanPham()
        {
            InitializeComponent();
            this.Load += QLDanhMucSanPham_Load;
            dgv_DM.CellClick += Dgv_DM_CellClick;
            dgv_DM1.CellClick += Dgv_DM1_CellClick;
            dgv_DM2.CellClick += Dgv_DM2_CellClick;
        }

        private void Dgv_DM2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DM2.Rows[e.RowIndex];

                cb_MaDanhMuc1.Text = row.Cells["MenuLevel1Id"].Value.ToString();
                txt_MaDM2.Text = row.Cells["Id"].Value?.ToString();
                txt_TenDM2.Text = row.Cells["MenuName"].Value?.ToString();
                txt_URL2.Text = row.Cells["RedirectUrl"].Value?.ToString();

            }
        }

        private void Dgv_DM1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DM1.Rows[e.RowIndex];

                // Hiển thị dữ liệu
                cb_MaDanhMuc.SelectedValue = row.Cells["MenuId"].Value; // Chọn giá trị MenuId
                txt_MaDM1.Text = row.Cells["Id"].Value?.ToString();
                txt_TenDM1.Text = row.Cells["MenuName"].Value?.ToString();
                txt_URL1.Text = row.Cells["RedirectUrl"].Value?.ToString();
            }
        }

        private void Dgv_DM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DM.Rows[e.RowIndex];
                txt_MaDM.Text = row.Cells["Id"].Value?.ToString();
                txt_TenDM.Text = row.Cells["MenuName"].Value?.ToString();
                txt_URL.Text = row.Cells["RedirectUrl"].Value?.ToString();
                txt_BieuTuong.Text = row.Cells["MenuIcon"].Value?.ToString();
            }
        }

        private void QLDanhMucSanPham_Load(object sender, EventArgs e)
        {
            loadMenu();
            loadMenu1();
            loadMenu2();
        }

        public void loadMenu()
        {
            dgv_DM.DataSource = menuBLL.GetMenus();
            dgv_DM.Columns["Id"].HeaderText = "Mã danh mục";
            dgv_DM.Columns["MenuName"].HeaderText = "Tên danh mục";
            dgv_DM.Columns["RedirectUrl"].HeaderText = "Url chuyển hướng";
            dgv_DM.Columns["MenuIcon"].HeaderText = "Biểu tượng";
            // Load Id của dgv_DM vào ComboBox cb_MaDanhMuc
            cb_MaDanhMuc.DataSource = menuBLL.GetMenus(); // Đặt nguồn dữ liệu cho ComboBox
            cb_MaDanhMuc.DisplayMember = "MenuId"; // Hiển thị tên danh mục
            cb_MaDanhMuc.ValueMember = "Id"; // Giá trị ẩn là Id
            cb_MaDanhMuc.SelectedIndex = -1; // Bỏ chọn mặc định


        }
        public void loadMenu1()
        {
            dgv_DM1.DataSource = menu1BLL.GetMenus1();
            dgv_DM1.Columns["Id"].HeaderText = "Mã danh mục cấp 1";
            dgv_DM1.Columns["MenuName"].HeaderText = "Tên danh mục";
            dgv_DM1.Columns["RedirectUrl"].HeaderText = "Url chuyển hướng";
            dgv_DM1.Columns["MenuId"].HeaderText = "Mã danh mục";
            dgv_DM1.Columns["Menu"].Visible = false;
            // Load Id của dgv_DM vào ComboBox cb_MaDanhMuc
            cb_MaDanhMuc1.DataSource = menu1BLL.GetMenus1(); // Đặt nguồn dữ liệu cho ComboBox
            cb_MaDanhMuc1.DisplayMember = "Id"; // Hiển thị tên danh mục
            cb_MaDanhMuc1.ValueMember = "Id"; // Giá trị ẩn là Id
            cb_MaDanhMuc1.SelectedIndex = -1; // Bỏ chọn mặc định
        }
        public void loadMenu2()
        {
            dgv_DM2.DataSource = menu2BLL.GetMenus2();
            dgv_DM2.Columns["Id"].HeaderText = "Mã danh mục cấp 2";
            dgv_DM2.Columns["MenuName"].HeaderText = "Tên danh mục";
            dgv_DM2.Columns["RedirectUrl"].HeaderText = "Url chuyển hướng";
            dgv_DM2.Columns["MenuLevel1Id"].HeaderText = "Mã danh mục cấp 1";
            dgv_DM2.Columns["MenuLevel1"].Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra ràng buộc: các trường không được để trống
            if (string.IsNullOrWhiteSpace(txt_TenDM.Text) || string.IsNullOrWhiteSpace(txt_URL.Text) || string.IsNullOrWhiteSpace(txt_BieuTuong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng menu từ dữ liệu nhập
                DTO.Menu newMenu = new DTO.Menu
                {
                    MenuName = txt_TenDM.Text,
                    RedirectUrl = txt_URL.Text,
                    MenuIcon = txt_BieuTuong.Text
                };

                // Thêm menu vào cơ sở dữ liệu
                menuBLL.AddMenu(newMenu);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaDM.Text) || !int.TryParse(txt_MaDM.Text, out int menuId))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Xóa danh mục khỏi cơ sở dữ liệu
                menuBLL.DeleteMenu(menuId);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Kiểm tra ràng buộc: các trường không được để trống
            if (string.IsNullOrWhiteSpace(txt_TenDM.Text) || string.IsNullOrWhiteSpace(txt_URL.Text) || string.IsNullOrWhiteSpace(txt_BieuTuong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_MaDM.Text) || !int.TryParse(txt_MaDM.Text, out int menuId))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng menu với dữ liệu đã sửa
                DTO.Menu updatedMenu = new DTO.Menu
                {
                    Id = menuId, // Mã danh mục cần sửa
                    MenuName = txt_TenDM.Text,
                    RedirectUrl = txt_URL.Text,
                    MenuIcon = txt_BieuTuong.Text
                };

                // Cập nhật thông tin danh mục
                menuBLL.UpdateMenu(updatedMenu);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddMenu1_Click(object sender, EventArgs e)
        {
            // Kiểm tra ràng buộc: các trường không được để trống
            if (string.IsNullOrWhiteSpace(txt_TenDM1.Text) ||string.IsNullOrWhiteSpace(txt_URL1.Text) ||string.IsNullOrWhiteSpace(cb_MaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Chuyển đổi cb_MaDanhMuc.Text sang kiểu int
                if (!int.TryParse(cb_MaDanhMuc.Text, out int menuId))
                {
                    MessageBox.Show("Mã danh mục phải là một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo đối tượng menu từ dữ liệu nhập
                MenuLevel1 newMenu = new MenuLevel1
                {
                    MenuName = txt_TenDM1.Text,
                    RedirectUrl = txt_URL1.Text,
                    MenuId = menuId // Gán giá trị đã chuyển đổi
                };

                // Thêm menu vào cơ sở dữ liệu
                menu1BLL.AddMenu1(newMenu);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu1();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Delete1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaDM1.Text) || !int.TryParse(txt_MaDM1.Text, out int menuId))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Xóa danh mục khỏi cơ sở dữ liệu
                menu1BLL.DeleteMenu1(menuId);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu1();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Update1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TenDM1.Text) ||string.IsNullOrWhiteSpace(txt_URL1.Text) ||string.IsNullOrWhiteSpace(cb_MaDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txt_MaDM1.Text, out int menu1Id))
            {
                MessageBox.Show("Vui lòng chọn danh mục hợp lệ để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng menu cập nhật
                MenuLevel1 updatedMenu = new MenuLevel1
                {
                    Id = menu1Id,
                    MenuName = txt_TenDM1.Text,
                    RedirectUrl = txt_URL1.Text,
                    MenuId = int.Parse(cb_MaDanhMuc.Text) // Chuyển đổi MenuId nếu cần
                };

                // Cập nhật menu
                menu1BLL.UpdateMenu1(updatedMenu);

                // Thông báo thành công
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu1();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddMenu2_Click(object sender, EventArgs e)
        {
            // Kiểm tra ràng buộc: các trường không được để trống
            if (string.IsNullOrWhiteSpace(txt_TenDM2.Text) || string.IsNullOrWhiteSpace(txt_URL2.Text) || string.IsNullOrWhiteSpace(cb_MaDanhMuc1.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Chuyển đổi cb_MaDanhMuc.Text sang kiểu int
                if (!int.TryParse(cb_MaDanhMuc1.Text, out int menuId))
                {
                    MessageBox.Show("Mã danh mục phải là một số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo đối tượng menu từ dữ liệu nhập
                MenuLevel2 newMenu = new MenuLevel2
                {
                    MenuName = txt_TenDM2.Text,
                    RedirectUrl = txt_URL2.Text,
                    MenuLevel1Id = menuId // Gán giá trị đã chuyển đổi
                };

                // Thêm menu vào cơ sở dữ liệu
                menu2BLL.AddMenu2(newMenu);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu2();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Delete2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaDM2.Text) || !int.TryParse(txt_MaDM2.Text, out int menuId))
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Xóa danh mục khỏi cơ sở dữ liệu
                menu2BLL.DeleteMenu2(menuId);

                // Thông báo thành công
                MessageBox.Show("Danh mục đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu2();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Update2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TenDM2.Text) || string.IsNullOrWhiteSpace(txt_URL2.Text) || string.IsNullOrWhiteSpace(cb_MaDanhMuc1.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txt_MaDM2.Text, out int menu2Id))
            {
                MessageBox.Show("Vui lòng chọn danh mục hợp lệ để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng menu cập nhật
                MenuLevel2 updatedMenu = new MenuLevel2
                {
                    Id = menu2Id,
                    MenuName = txt_TenDM2.Text,
                    RedirectUrl = txt_URL2.Text,
                    MenuLevel1Id = int.Parse(cb_MaDanhMuc1.Text) // Chuyển đổi MenuId nếu cần
                };

                // Cập nhật menu
                menu2BLL.UpdateMenu2(updatedMenu);

                // Thông báo thành công
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại danh sách menu
                loadMenu2();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        }
}
