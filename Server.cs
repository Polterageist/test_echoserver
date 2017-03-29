using System;
using System.Net;
using System.Net.Sockets;

namespace TestEchoServer {
    class Server {
        public Server(IPEndPoint listenEndPoint) {
            ListenEndPoint = listenEndPoint;
        }

        public IPEndPoint ListenEndPoint { get; }

        public void Run() {

        }
    }
}
