using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace SocksLib
{
    /// <summary>
    /// Wraps up the socket acception process into a few lines of code.
    /// </summary>
    /// <remarks>
    /// IDisposable is added to the class post-tutorial.
    /// </remarks>
    public sealed class TcpServerSocket : IDisposable
    {
        /// <summary>
        /// Used to indicate if this instance is currently listening in on a socket.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// Used to indicate which IP this listener is currently using.
        /// </summary>
        public IPAddress IP { get; set; }

        /// <summary>
        /// Used to indicate which port this listener is currently using.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Used to indicate if the class has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Called when a connection is accepted.
        /// </summary>
        public event EventHandler<AcceptedTcpSocketEventArgs> Accepted;

        /// <summary>
        /// A list of all the connected clients.
        /// </summary>
        public List<TcpClientSocket> ConnectedClients { get; set; }

        /// <summary>
        /// A string containing the socket IP and Port.
        /// </summary>
        public IPEndPoint SocketEndpoint { get; private set; }

        /// <summary>
        /// Holds the instance of the socket used as a listener.
        /// </summary>
        private Socket listener;

        /// <summary>
        /// Instantiates a new TcpListener.
        /// </summary>
        /// <param name="port">The port to listen on.</param>

        /// <summary>
        /// Instantiates a new TcpListener. (NOT WORKING)
        /// </summary>
        /// <param name="port">The port to listen on.</param>
        /// <param name="ip">The IP address to listen on.</param>
        public TcpServerSocket(IPEndPoint ipEnd)
        {
            this.SocketEndpoint = ipEnd;
        }

        /// <summary>
        /// Starts the listening procedure.
        /// </summary>
        public void Start()
        {
            //If this instance is currently not running:
            if (!this.Running)
            {
                //Instantiate a new socket used for listening.
                this.listener =
                    new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Bind the socket to this PC's loopback, but with the desired port.
                this.listener.Bind(SocketEndpoint);
                //Start listening with a connection queue of 100.
                this.listener.Listen(0);
                //Set running to true.
                this.Running = true;
                //Start the receive loop.
                this.listener.BeginAccept(acceptCallback, null);
            }
            else
            {
                //If we're running, let it be known.
                throw new InvalidOperationException("Already running!");
            }
        }

        /// <summary>
        /// Stops the listening procedure.
        /// </summary>
        public void Stop()
        {
            //If its running:
            if (this.Running)
            {
                //Close the listener.
                this.listener.Close();
                //Remove the reference.
                this.listener = null;
                //Set Running to false.
                this.Running = false;
            }
            else
            {
                //Let it be known if this instance is not listening.
                throw new InvalidOperationException("The server is not running.");
            }
        }

        /// <summary>
        /// The callback for BeginAccept
        /// </summary>
        /// <param name="ar"></param>
        private void acceptCallback(IAsyncResult ar)
        {
            try
            {
                //Get the connected socket.
                var accepted = this.listener.EndAccept(ar);

                //Starts the loop again.
                this.listener.BeginAccept(acceptCallback, null);

                //Call the Accepted event.
                Accepted?.Invoke(this, new AcceptedTcpSocketEventArgs(accepted));
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }

        /// <summary>
        /// Cleans up this instance of any objects that must be released.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                //Only dispose of the socket if it is running...
                if (this.Running)
                {
                    this.listener.Close();
                    this.listener = null;
                }
            }
        }

        public int GetClientListIndex(TcpClientSocket _client)
        {
            return ConnectedClients.IndexOf(_client);
        }
    }
}
