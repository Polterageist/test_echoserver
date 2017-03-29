using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace TestEchoServer {
    class Client {
        public Client(string room, string id, IPEndPoint endpoint) {
            Room = room;
            Id = id;
            ConnectEndPoint = endpoint;
        }

        public string Room { get; }

        public string Id { get; }

        public IPEndPoint ConnectEndPoint { get; }
        
        public void Run() {

        }
    }
}
