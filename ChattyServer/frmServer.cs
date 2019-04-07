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

namespace ChattyServer
{
    public partial class frmServer : Form
    {
        TcpServerSocket listenerSocket;

        int MessagesReceived = 0;

        public frmServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (listenerSocket == null || !listenerSocket.Running)
            {
                StartServer();
                btnSend.Enabled = true;
                tsslServerStatus.Text = "Server started";
            }
            else
            {
                MessageBox.Show("The server is already started.", "Cannot Start Server");
            }
        }

        void StartServer()
        {
            listenerSocket = new TcpServerSocket(IPAddress.Parse("127.0.0.1"), 25565);
            listenerSocket.ConnectedClients = new List<TcpClientSocket>();

            // Subscribe to find when new clients are connected.
            // <param="s"> The instance of the class that invoded the event. <param>
            // <param="e"> AcceptedTcpSocketEventArgs (which contains the accepted socket, the remote endpoint, the tcpsocket connection state (e.g. an exception that has occured or if the socket is connected, if a data has been received and a reference to the socket in the socket object which can be used for extra purposes.))<param>
            listenerSocket.Accepted += (s, e) =>
            {
                listenerSocket.ConnectedClients.Add(e.AcceptedSocket);
                beginReceiving(listenerSocket.ConnectedClients[listenerSocket.ConnectedClients.IndexOf(e.AcceptedSocket)]);
                // TODO: BeginAccept Again unless it's already listening for client.s
                //listenerSocket.Stop();
            };

            for (int i = 0; i < listenerSocket.ConnectedClients.Count; i++)
            {
                listenerSocket.ConnectedClients[i] = null;
            }

            try
            {
                listenerSocket.Start();

                if (rtbServerMessages.TextLength <= 0)
                {
                    AppendText("Server started.", true);
                }
                else
                {
                    AppendText("Server started.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unable to start the server.");
            }
        }

        private void btnStopListening_Click(object sender, EventArgs e)
        {
            if (listenerSocket.Running)
            {
                try
                {
                    // Disconnect all connected clients
                    foreach (var _client in listenerSocket.ConnectedClients)
                    {
                        _client.Disconnect();
                        //listenerSocket.ConnectedClients.Remove(_client);
                        //clbConnectedClients.Items.Remove(_client.GetIpEndPointString());
                    }

                    listenerSocket.Stop();
                    btnSend.Enabled = false;
                    AppendText("\nServer stopped.", true);
                    tsslServerStatus.Text = "Idle";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Unable to stop the server.");
                }
            }
            else
            {
                MessageBox.Show("The server is not running.", "Cannot Stop Server");
            }
        }

        private void Send(TcpClientSocket _tcpSocket, string _sendMessage)
        {
            if (_tcpSocket != null && _tcpSocket.Connected)
            {
                if (_sendMessage.ToLower() != "disconnect")
                {
                    byte[] msgPayload = Encoding.ASCII.GetBytes(_sendMessage);

                    _tcpSocket.SendAsync(msgPayload);
                }
                else
                {
                    _tcpSocket.Disconnect();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(tbMsg.Text);
        }

        private void SendMessage(string _message)
        {
            if (_message != string.Empty && _message != null)
            {
                if (cbSendToAll.Checked)
                {
                    if (clbConnectedClients.Items.Count > 0)
                    {
                        foreach (var _client in listenerSocket.ConnectedClients)
                        {
                            if (_client.Connected)
                            {
                                Send(_client, _message);
                                AppendText($"Server > {_client.GetIpEndPointString()}: {_message}");
                            }
                            else
                            {
                                AppendText($"Could not send the message to {_client.GetIpEndPointString()}. (Client disconnected)");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No client to send to.", "No Clients Available");
                    }
                }
                else
                {
                    if (clbConnectedClients.CheckedItems.Count > 0)
                    {
                        for (int i = 0; i < clbConnectedClients.CheckedItems.Count; i++)
                        {
                            for (int j = 0; j < listenerSocket.ConnectedClients.Count; j++)
                            {
                                if (listenerSocket.ConnectedClients[j].Connected)
                                {
                                    Send(listenerSocket.ConnectedClients[j], _message);
                                    AppendText($"Server > {listenerSocket.ConnectedClients[j].GetIpEndPointString()}: {_message}");
                                }
                                else
                                {
                                    AppendText($"Could not send the message to {listenerSocket.ConnectedClients[j].GetIpEndPointString()}. (Client disconnected)");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("There are no clients selected.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a message before sending.", "Cannot Send Message");
                //SystemSounds.Asterisk.Play();
            }

            tbMsg.Text = string.Empty; // Clears the message box.
        }

        private void SpamMessage(string _message)
        {
            if (!cbSendToAll.Checked)
            {
                if (clbConnectedClients.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < listenerSocket.ConnectedClients.Count; i++)
                    {
                        for (int j = 0; j < nudSpamAmount.Value; j++)
                        {
                            Send(listenerSocket.ConnectedClients[i], _message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There are not clients to spam.");
                }
            }
            else
            {
                if (clbConnectedClients.SelectedItems.Count > 0)
                {
                    foreach (var _client in listenerSocket.ConnectedClients)
                    {
                        if (_client.Connected)
                        {
                            for (int i = 0; i < nudSpamAmount.Value; i++)
                            {
                                Send(_client, _message);
                                AppendText($"Server > {_client.GetIpEndPointString()}: {_message}");
                            }
                        }
                        else
                        {
                            AppendText($"Could not spam the message to {_client.GetIpEndPointString()}. (Client not connected)");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There are not clients to spam.");
                }
            }
        }

        private void beginReceiving(TcpClientSocket client)
        {
            client.DataReceived += (ds, de) =>
            {
                MessagesReceived++;

                if (MessagesReceived == 1)
                {
                    client.Username = Encoding.ASCII.GetString(de.Payload);
                    this.Invoke((MethodInvoker)(() => clbConnectedClients.Items.Add($"{client.Username}")));
                    AppendText($"{client.Username} has connected. ({client.GetIpEndPointString()})");
                }
                else
                {
                    AppendText($"From {client.GetIpEndPointString()}: {Encoding.ASCII.GetString(de.Payload)}");
                    tsslReceivedMessages.Text = $"Received Messages: {MessagesReceived - 1}";
                }
            };

            client.Disconnected += (ds, de) =>
            {
                AppendText($"{client.Username} has disconnected!");

                clbConnectedClients.InvokeIfRequired(s => { s.Items.Remove(clbConnectedClients.Items.IndexOf(client)); });

                //this.Invoke((MethodInvoker)(() => clbConnectedClients.Items.Remove(clbConnectedClients.Items.IndexOf(client))));

                listenerSocket.ConnectedClients.Remove(client);
            };
        }

        /// <summary>
        /// Pushes text to the chat window.
        /// </summary>
        /// <param name="_textToAppend"> The text being pushed to the chat window.</param>
        /// <param name="removeNewLine"> Whether or not to put a newline in front of the text being displayed.</param>
        public void AppendText(string _textToAppend, bool removeNewLine = false)
        {
            if (!removeNewLine)
                rtbServerMessages.InvokeIfRequired(s => { s.AppendText($"\n{_textToAppend}"); });
            else
                rtbServerMessages.InvokeIfRequired(s => { s.AppendText($"{_textToAppend}"); });
        }

        private void btnSpam_Click(object sender, EventArgs e)
        {
            SpamMessage(tbMsg.Text);
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
