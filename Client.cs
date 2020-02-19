using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Socket _sk;
        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "Receiving Mode";
            timer1.Start();
            IPHostEntry ihe = Dns.GetHostByName(Dns.GetHostName());
            IPAddress ip = ihe.AddressList[0];
            IPEndPoint ep = new IPEndPoint(ip, 8000);
            Socket sk = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sk.Connect(ep);

            listBox1.Items.Add("***Connected To Server !");
            label3.Text = "Connected";
            label3.ForeColor = Color.Green;
            _sk = sk;
        }

        public void rec(Socket sk)
        {
            byte[] arr = new byte[1024];
            sk.Receive(arr);
            listBox1.Items.Add("Server => " + Encoding.ASCII.GetString(arr));
            timer1.Stop();
            label2.Text = "Sending Mode";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rec(_sk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            _sk.Send(Encoding.ASCII.GetBytes(str));
            listBox1.Items.Add("Client => " + textBox1.Text);
            label2.Text = "Receiving Mode";
            timer1.Start();
        }
    }
}
