using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APDTrigger_WinForms.Helper
{
    public class TcpServer
    {
        private TcpListener _listener;
        private readonly List<TcpClient> _myTcpClients = new List<TcpClient>();
        private Controller _myController;

        public TcpServer(Controller controller)
        {
            _myController = controller;
            //controller.RunDone += sendRunData;
            _listener = new TcpListener(IPAddress.Any, 9898);
            Thread listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }

        private void ListenForClients()
        {
            _listener.Start();
            try
            {
                TcpClient client = _listener.AcceptTcpClient();
                _myTcpClients.Add(client);
                Thread clientThread = new Thread(HandleClientComm);
                clientThread.Start(client);
            }
            catch(Exception e)
            {
                //dirty hack to surpress the thrown exception
                //we are killing a blocking function here which is not nice
                System.Console.WriteLine("EVIL: " + e);
            }
            finally
            {
                _listener.Stop();
                //_listener = null;
            }
                       
        }

        public void Stop()
        {
            foreach(TcpClient client in _myTcpClients )
            {
                client.GetStream().Close();
                client.Close();
            }

            _listener.Stop();
            
        }
        
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient) client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);                    
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }

    }
}