using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPServer
{
    public partial class TCPServer : Form
    {
        public TCPServer()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;

        private void TCPServer_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; //enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            textStatus.Invoke((MethodInvoker)delegate ()
            {
                textStatus.Text += e.MessageString;
                textStatus.AppendText(Environment.NewLine);
                

                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void Startbutton_Click(object sender, EventArgs e)
        {
            textStatus.Text += "Server starting...";
            textStatus.AppendText(Environment.NewLine);

            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textHost.Text);
            server.Start(ip, Convert.ToInt32(textPort.Text));
        }

        private void Stopbutton_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }
}
