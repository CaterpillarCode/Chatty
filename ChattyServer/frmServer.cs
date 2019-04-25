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
        // Socket used to listen for clients
        TcpServerSocket serverSocket;

        // Total messages received from clients
        int TotalMessagesRecieved = 0;

        public frmServer()
        {
            InitializeComponent();
        }

        #region Starting
        /// <summary>
        /// Starts listening for new connections.
        /// </summary>
        void StartServer(IPEndPoint _ipEndpoint)
        {
            // Starts listening if the server isn't already listening.
            if (this.serverSocket == null || !this.serverSocket.Running)
            {
                this.serverSocket = new TcpServerSocket(_ipEndpoint)
                {
                    ConnectedClients = new List<TcpClientSocket>()
                };

                // Subscribe to find when new clients are connected.
                // <param="s"> The instance of the class that invoded the event. <param>
                // <param="e"> AcceptedTcpSocketEventArgs (which contains the accepted socket, the remote endpoint, the tcpsocket connection state (e.g. an exception that has occured or if the socket is connected, if a data has been received and a reference to the socket in the socket object which can be used for extra purposes.))<param>
                this.serverSocket.Accepted += (s, e) =>
                {
                    this.serverSocket.ConnectedClients.Add(e.AcceptedSocket);
                    beginReceiving(this.serverSocket.ConnectedClients[this.serverSocket.ConnectedClients.IndexOf(e.AcceptedSocket)]);
                    TsslConnectedClients.Text = $"Connected Clients: {this.serverSocket.ConnectedClients.Count}";
                };

                this.serverSocket.ConnectedClients.Clear();

                for (int i = 0; i < this.serverSocket.ConnectedClients.Count; i++)
                {
                    this.serverSocket.ConnectedClients[i] = null;
                }

                try
                {
                    this.serverSocket.Start();

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

                btnSend.Enabled = true;
                tsslServerStatus.Text = "State: Running";
            }
            else
            {
                MessageBox.Show("The server is already started.", "Cannot Start Server");
            }
        }
        #endregion

        #region Stoping
        void StopServer()
        {
            if (this.serverSocket.Running)
            {
                try
                {
                    // Disconnect all connected clients
                    foreach (var _client in this.serverSocket.ConnectedClients)
                    {
                        if (_client is TcpClientSocket tcs)
                        {
                            Send(_client, "The server has been shutdown.");
                            tcs.Disconnect();
                        }
                    }

                    this.serverSocket.Stop();
                    btnSend.Enabled = false;
                    AppendText("\nServer stopped.", true);
                    tsslServerStatus.Text = "State: Stopped";
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
        #endregion

        #region Sending
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

        private void SendMessage(string _message)
        {
            if (_message != string.Empty && _message != null)
            {
                if (cbSendToAll.Checked)
                {
                    if (clbConnectedClients.Items.Count > 0)
                    {
                        foreach (var _client in this.serverSocket.ConnectedClients)
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
                            for (int j = 0; j < this.serverSocket.ConnectedClients.Count; j++)
                            {
                                if (this.serverSocket.ConnectedClients[j].Connected)
                                {
                                    Send(this.serverSocket.ConnectedClients[j], _message);
                                    AppendText($"Server > {this.serverSocket.ConnectedClients[j].GetIpEndPointString()}: {_message}");
                                }
                                else
                                {
                                    AppendText($"Could not send the message to {this.serverSocket.ConnectedClients[j].GetIpEndPointString()}. (Client disconnected)");
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
                    for (int i = 0; i < this.serverSocket.ConnectedClients.Count; i++)
                    {
                        for (int j = 0; j < nudSpamAmount.Value; j++)
                        {
                            Send(this.serverSocket.ConnectedClients[i], _message);
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
                    foreach (var _client in this.serverSocket.ConnectedClients)
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
        #endregion

        #region Receiving
        private void beginReceiving(TcpClientSocket client)
        {
            client.DataReceived += (ds, de) =>
            {
                string payloadString = Encoding.ASCII.GetString(de.Payload);

                // If the message starts with a special string, update the username
                if (payloadString.StartsWith("<c>username"))
                {
                    client.Username = payloadString.Remove(0, 11);
                    this.Invoke((MethodInvoker)(() => clbConnectedClients.Items.Add($"{client.Username}")));
                    AppendText($"{client.Username} has connected. ({client.GetIpEndPointString()})");
                }
                else
                {
                    TotalMessagesRecieved++;
                    AppendText($"{client.Username}: {Encoding.ASCII.GetString(de.Payload)}");
                    TsslReceivedMessages.Text = $"Received Messages: {TotalMessagesRecieved}";
                }
            };

            client.Disconnected += (ds, de) =>
            {
                try
                {
                    // Output to the server console
                    AppendText($"{client.Username} has disconnected!");

                    // Remove the socket from the checked list box
                    clbConnectedClients.InvokeIfRequired(s => { s.Items.RemoveAt(this.serverSocket.GetClientListIndex(client)); });

                    // Remove the socket from the list of connected clients.
                    this.serverSocket.ConnectedClients.Remove(client);

                    // Displays how many clients are connected
                    TsslConnectedClients.Text = $"Connected Clients: {this.serverSocket.ConnectedClients.Count}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            };
        }
        #endregion

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

        #region Control Events

        /// <summary>
        /// Tries to start listeing for connections.
        /// </summary>
        private void btnListen_Click(object sender, EventArgs e)
        {   
            // Server IP
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            // Server Port
            int port = 25565;
            // Server Endpoint
            IPEndPoint ipEnd = new IPEndPoint(ipAddress, port);

            // Starts listening for clients
            StartServer(ipEnd);
        }

        /// <summary>
        /// Tries to stop listening for connections.
        /// </summary>
        private void btnStopListening_Click(object sender, EventArgs e)
        {
            // Stops the server
            StopServer();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(tbMsg.Text);
        }

        private void btnSpam_Click(object sender, EventArgs e)
        {
            SpamMessage(tbMsg.Text);
        }

        private void rtbServerMessages_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtbServerMessages.SelectionStart = rtbServerMessages.Text.Length;
            // scroll to the latest message
            rtbServerMessages.ScrollToCaret();
        }
        #endregion
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
