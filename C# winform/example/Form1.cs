using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.IO.Ports;    // 씨리얼 포트 사용하기 위해 필요
using System.IO;

namespace example
{
    public partial class Form1 : Form
    {
        StreamWriter sw = new StreamWriter("a.txt");
        FileInfo file = new FileInfo("a.txt");
        int duration1 = 59, duration2 = 59;
        int duration = 44, duration3 = 44;
        int stop1 = 0, stop3 = 0, stop4 = 0;
        int stop2 = 0;
        int temp = 1, temp1 = 1, state = 0;
        string data_rx = "1\r";
        string data_rx1 = "1\r";
        private SerialPort ardSerialPort = new SerialPort();        // 사용할 씨리얼포트 객체 생성
        public Form1()
        {
            InitializeComponent();
            ardSerialPort.PortName = "COM3";
            ardSerialPort.BaudRate = 9600;
            ardSerialPort.Open();                                        // 포트 사용 준비 완료
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer5.Start();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            
            duration--;
            label5.Text = duration.ToString();
            stop1++;
            if(stop1 == 45)
            {
                duration = 44;
                label5.Text = 45.ToString();
                timer1.Stop();
                stop1 = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            duration1--;
            
            if (duration1 == -1)
               {
                  duration1 = 59;
               }
            label6.Text = duration1.ToString();
            if(temp % 60 == 0)
            {
                stop2++;
                if (stop2 == 45)
                {
                    label6.Text = "00";
                    timer2.Stop();
                    label1.Text = " 사용 가능";
                    stop2 = 0;
                    duration1 = 59;
                    timer5.Start();
                }
            }
            temp++;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = " 사용 불가능";
            timer3.Enabled = true;
            timer3.Start();
            timer4.Enabled = true;
            timer4.Start();
            label8.Text = duration3.ToString();
            label10.Text = duration2.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            duration3--;
            label8.Text = duration3.ToString();
            stop3++;
            if (stop3 == 45)
            {
                duration3 = 44;
                label8.Text = 45.ToString();
                timer3.Stop();
                stop3 = 0;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            duration2--;

            if (duration2 == -1)
            {
                duration2 = 59;
            }
            label10.Text = duration2.ToString();
            if (temp1 % 60 == 0)
            {
                stop4++;
                if (stop4 == 45)
                {
                    label10.Text = "00";
                    timer4.Stop();
                    label2.Text = " 사용 가능";
                    stop4 = 0;
                    duration2 = 59;
                }
            }
            temp1++;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("b.txt");
            int f1 = Int32.Parse(sr.ReadLine());
            sr.Close();
            data_rx = ardSerialPort.ReadLine();
            sw.WriteLine(data_rx);
            //textBox1.Text = data_rx;
            if (data_rx == "0\r")
            {
                    timer5.Stop();
                    if (timer1.Enabled == false && timer2.Enabled == false)
                    {
                        label1.Text = " 사용 불가능";
                        timer1.Enabled = true;
                        timer2.Enabled = true;
                        timer1.Start();
                        timer2.Start();
                        sw.Close();
                        label5.Text = duration.ToString();
                        label6.Text = duration1.ToString();
                        string imagePath = Application.StartupPath + "\\..\\..\\..\\..\\webserver\\";
                        file.CopyTo(imagePath + "b" + f1 + ".txt");
                        f1++;
                        StreamWriter sw1 = new StreamWriter("b.txt");
                        sw1.WriteLine(f1);
                        sw1.Close();
                        
                    }
            }
        }

        
    }
}
