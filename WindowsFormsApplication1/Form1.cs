using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using parking;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AutoParkingDto GetModelFromUI()
        {
            return new AutoParkingDto()
            {
                Filled = dateTimePicker1.Value,
                TimeOut = dateTimePicker2.Value,
                AutoName = textBox1.Text,
                ParkingNumber = (int)numericUpDown1.Value,
                AutoNumber = textBox2.Text,
                Price = numericUpDown2.Value,
            };
        }
        private void SetModelToUI(AutoParkingDto dto)
        {
            //button4.Enabled = false;
            dateTimePicker1.Value = dto.Filled;
            dateTimePicker2.Value = dto.TimeOut;
            textBox1.Text = dto.AutoName;
            textBox2.Text = dto.AutoNumber;
            numericUpDown2.Value = dto.Price;
            numericUpDown1.Value = dto.ParkingNumber;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void FIO_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new BodyType();
            var res = form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new OwnerInfo();
            var res = form.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Файлы заказов|*.prkng" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = GetModelFromUI();
                AutoParkingHelper.WriteToFile(sfd.FileName, dto);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Файл заказа|*.prkng" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = AutoParkingHelper.LoadFromFile(ofd.FileName);
                SetModelToUI(dto);
            }
        }
    }
}
