using SocksLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatty
{
    public partial class frmChatty : Form
    {
        // Socket
        TcpClientSocket socket;
        IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25565);

        public frmChatty()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text != string.Empty && tbUsername.Text != null)
            {
                StartClient(tbUsername.Text);
                btnSend.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please enter a user name before connecting.", "No Username Specified");
            }
        }

        void StartClient(string userName)
        {
            socket = new TcpClientSocket();

            socket.AsyncConnectionResult += (s, e) =>
            {
                if (e.Connected)
                {
                    byte[] msgPayload = Encoding.ASCII.GetBytes(userName);
                    socket.SendAsync(msgPayload);

                    beginReceiving(socket);

                    this.Invoke((MethodInvoker)(() => rtbClientMessages.AppendText("Connected to the server.")));

                    this.Invoke((MethodInvoker)(() => btnConnect.Text = "Connected"));
                }
                else
                {
                    StartClient(userName);
                }
            };

            try
            {
                // Set the conncet controls to disabled
                btnConnect.Text = "Connecting";
                btnConnect.Enabled = false;
                tbUsername.Enabled = false;

                // Connect to the server.
                socket.ConnectAsync(ipEnd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unable to connect to the server.");

                // Set the conncet controls to enabled
                btnConnect.Text = "Connect";
                btnConnect.Enabled = true;
                tbUsername.Enabled = true;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (TbMsg.Text != string.Empty && TbMsg.Text != null)
            {
                string msg = TbMsg.Text;

                if (socket != null && socket.Connected)
                {
                    if (msg.ToLower() != "!disconnect")
                    {
                        byte[] msgPayload = Encoding.ASCII.GetBytes(msg);

                        socket.SendAsync(msgPayload);

                        AppendText($"\nYou: {msg}");
                    }
                    else
                    {
                        AppendText("Disconnected from the server.");
                        socket.Disconnect();
                    }
                    TbMsg.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("You are not connected to the server.");
                    TbMsg.Clear();
                }
            }
            else
            {
                //MessageBox.Show("Please enter a message before sending", "No Message To Send");
            }
        }

        private void TbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // removes the Windows sound when the key is pressed.
                e.Handled = true;
                e.SuppressKeyPress = true;

                BtnSend_Click(this, new EventArgs());   // Invokes the BtnSend click method.
            }
        }

        private void beginReceiving(TcpClientSocket client)
        {
            client.DataReceived += (ds, de) =>
            {
                if (client.GetIpEndPointString().Contains("25565"))
                {
                    AppendText($"Server: {Encoding.ASCII.GetString(de.Payload)}");
                }
                else
                {
                    AppendText($"{client.GetIpEndPointString()}: {Encoding.ASCII.GetString(de.Payload)}");
                }
            };

            client.Disconnected += (ds, de) =>
            {
                AppendText($"\nDisconnected from the server.");

                // Changes the connect controls to allow connecting to the server.
                btnConnect.InvokeIfRequired(s => { s.Enabled = true; });
                tbUsername.InvokeIfRequired(s => { s.Enabled = true; });
                //this.Invoke((Delegate)(() => BtnConnect.Enabled)));
                //this.Invoke((MethodInvoker)(() => TbUsername.Enabled = true)));
            };
        }

        public void AppendText(string _textToAppend)
        {
            rtbClientMessages.InvokeIfRequired(s => { s.AppendText($"{_textToAppend}"); });

            //if (this.InvokeRequired)
            //    this.Invoke((MethodInvoker)(() => StatusBox.AppendText($"\n{_textToAppend}")));
            //else
            //    StatusBox.AppendText($"{_textToAppend}");
        }

        private void TbMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // removes the Windows sound when the key is pressed.
                e.Handled = true;
                e.SuppressKeyPress = true;

                BtnSend_Click(this, new EventArgs());   // Invokes the BtnSend click method.
            }
        }
    }

    public static class ControlHelpers
    {
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => action(control)), null);
            }
            else
            {
                action(control);
            }
        }
    }
}
