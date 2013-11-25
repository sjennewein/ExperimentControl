using System.Net.Sockets;
using System.Text;

namespace ColdNetworkStack
{
    public class StateObject
    {
        // Client socket

        // Size of receive buffer
        public const int BufferSize = 256;

        // Receive buffer
        public byte[] buffer = new byte[BufferSize];

        // Received data string
        public StringBuilder sb = new StringBuilder();
        public Socket workSocket = null;
    }
}