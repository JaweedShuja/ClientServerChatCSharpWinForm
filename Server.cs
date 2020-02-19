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

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Socket _cl;
        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            label2.Text = "Sending Mode";
            listBox1.Items.Add("***Waiting For Cliet...");
            IPHostEntry ihe = Dns.GetHostByName(Dns.GetHostName());
            IPAddress ip = ihe.AddressList[0];
            IPEndPoint ep = new IPEndPoint(ip, 8000);
            Socket sk = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sk.Bind(ep);
            sk.Listen(10);
            Socket cl = sk.Accept();
            listBox1.Items.Add("***Connected To Client !");
            label3.Text = "Connected";
            label3.ForeColor = Color.Green;
            _cl = cl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st = textBox1.Text;
            _cl.Send(Encoding.ASCII.GetBytes(st));
            listBox1.Items.Add("Server => " + textBox1.Text);
            label2.Text = "Receiving Mode";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] arr = new byte[1024];
            _cl.Receive(arr);
            listBox1.Items.Add("Client => " + Encoding.ASCII.GetString(arr));
            timer1.Stop();
            label2.Text = "Sending Mode";
        }
    }
}
