using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KtraGKLan2_4551190048_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form_load);
            btnThem.Click += new EventHandler(Them);
            btnSua.Click += new EventHandler(Sua);
            btnXoa.Click += new EventHandler(Xoa);
            dataGridView.CellClick += new DataGridViewCellEventHandler(Data_Click);
        }

        private void Form_load(object sender, EventArgs e)
        {
            DataProvider.moKeNoi();
            loadHD();
            loadKHACHTHUENHA();
            loadNHA();
            DataProvider.dongKetNoi();
        }

        private void Them(object sender, EventArgs e)
        {
            string sql = "INSERT INTO NHA (MaNha,TenChuNha,GiaThue,DaChoThue) VALUES (@manha,@tenchunha,@giathue,@dachothue)";
            string[] name = { "@manha", "@tenchunha", "@giathue", "@dachothue" };
            bool dachothue = true;
            if (rdChuaThue.Checked == true)
                dachothue = false;
            object[] value = {txtMaNha.Text, txtTenChuNha.Text, dachothue, float.Parse(txtGiaThue.Text), cboKhachThueNha.SelectedValue.ToString(), cboSoHopDong.SelectedValue.ToString()};
            DataProvider.moKeNoi();
            DataProvider.updateData(sql, value, name);
            DataProvider.dongKetNoi();
            loadNHA();
        }

        private void Sua(object sender, EventArgs e)
        {
            string sql = string.Format("UPDATE NHA SET MaNha=@manha, TenChuNha=@tenchunha,GiaThue=@giathue,DaChoThue=@dachothue");
            string[] name = { "@manha", "@tenchunha", "@giathue", "@dachothue" };
            bool dachothue = true;
            if (rdChuaThue.Checked == true)
                dachothue = false;
            object[] value = { txtMaNha.Text, txtTenChuNha.Text, dachothue, float.Parse(txtGiaThue.Text), cboKhachThueNha.SelectedValue.ToString(), cboSoHopDong.SelectedValue.ToString() };
            DataProvider.moKeNoi();
            DataProvider.updateData(sql, value, name);
            DataProvider.dongKetNoi();
            MessageBox.Show("Sửa thành công");
            loadNHA();
        }

        private void Xoa(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                int i = dataGridView.CurrentCell.RowIndex;
                int ma = Convert.ToInt32(dataGridView.Rows[i].Cells[0].Value.ToString());
                string sql = string.Format("DELETE FROM NHA WHERE MaNha ='{0}'", ma);
                DataProvider.moKeNoi();
                DataProvider.updateData(sql);
                DataProvider.dongKetNoi();
                MessageBox.Show("Xóa thành công");
                loadNHA();

            }    
        }

        private void Data_Click(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView.CurrentCell.RowIndex;
            txtMaNha.Text = dataGridView.Rows[i].Cells[0].Value.ToString();
            txtTenChuNha.Text = dataGridView.Rows[i].Cells[1].Value.ToString();
            string dachothue = dataGridView.Rows[i].Cells[2].Value.ToString();
            if (dachothue.Equals("True"))
                rdChuaThue.Checked = true;
            else
                rdDaThue.Checked = true;
            txtGiaThue.Text = dataGridView.Rows[i].Cells[3].Value.ToString();
            cboKhachThueNha.SelectedValue = dataGridView.Rows[i].Cells[4].Value.ToString();
            cboSoHopDong.SelectedValue = dataGridView.Rows[i].Cells[5].Value.ToString();
        }
        public bool isNumber (string value)
        {
            bool ktra;
            float result;
            ktra = float.TryParse(value, out result);
            return ktra;
        }
        public void loadHD()
        {

            string sql = "SELECT * FROM HOPDONG ";
            cboSoHopDong.DataSource = DataProvider.GetTable(sql);
            cboSoHopDong.DisplayMember = "MaNha";
            cboSoHopDong.ValueMember = "SoHD";


        }
        public void loadKHACHTHUENHA()
        {
            string sql = "SELECT * FROM KHACHTHUENHA";
            cboKhachThueNha.DataSource = DataProvider.GetTable(sql);
            cboKhachThueNha.DisplayMember = "TenKhach";
            cboKhachThueNha.ValueMember = "MaKhach";
        }
        public void loadNHA()
        {
            string sql = "SELECT * FROM  NHA";
            dataGridView.DataSource = DataProvider.GetTable(sql);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
