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
                Series = textBox5.Text,
                Number = textBox3.Text,
                FullName = textBox6.Text,
                PhoneNumber = textBox4.Text,
                Type = (Body)comboBox1.SelectedIndex
            };
        }
   
        private void SetModelToUI(AutoParkingDto dto)
        {
            dateTimePicker1.Value = dto.Filled;
            dateTimePicker2.Value = dto.TimeOut;
            textBox1.Text = dto.AutoName;
            textBox2.Text = dto.AutoNumber;
            numericUpDown2.Value = dto.Price;
            numericUpDown1.Value = dto.ParkingNumber;
            textBox6.Text = dto.FullName;
            textBox4.Text = dto.PhoneNumber;
            textBox5.Text = dto.Series;
            textBox3.Text = dto.Number;
            comboBox1.SelectedIndex = (int)dto.Type;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number)&&number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number)&& number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Файлы заказов|*.prkng" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = GetModelFromUI();
                AutoParkingHelper.WriteToFile(sfd.FileName, dto);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Файл заказа|*.prkng" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var dto = AutoParkingHelper.LoadFromFile(ofd.FileName);
                SetModelToUI(dto);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var lv = new LicenceValidator();
            if (!lv.HasLicense)
            {
                MessageBox.Show("Лицензия не найдена. Приобретите лицензию у владельца ПО.");
                Application.Exit();
            }
            if (!lv.IsValid)
            {
                MessageBox.Show("Лицензия просрочена. Приобретите лицензию у владельца ПО.");
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
