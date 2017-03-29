using System;
using System.Diagnostics;
using System.Net;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace TestEchoServer {
    #region options
    
    [Verb("server", HelpText = "Run program in server mode")]
    class ServerOptions {
        [Option('p', "port", Default = 7878, HelpText = "Port to listen")]
        public int Port { get; set; }

        [Usage(ApplicationAlias = "TestEchoServer")]
        public static IEnumerable<Example> Examples {
            get {
                yield return new Example("Run as server", new ServerOptions { Port = 7878 });
            }
        }
    }

    [Verb("client", HelpText = "Run program in client mode")]
    class ClientOptions {
        [Option('h', "host", Default = "localhost", HelpText = "Server's host name")]
        public string Host { get; set; }

        [Option('p', "port", Default = 7878, HelpText = "Server's port")]
        public int Port { get; set; }

        [Value(0, Required = true, MetaName = "room", HelpText = "Room name to connect")]
        public string Room { get; set; }

        [Value(1, Required = true, MetaName = "login", HelpText = "Client id")]
        public string Login { get; set; }

        [Usage(ApplicationAlias = "TestEchoServer")]
        public static IEnumerable<Example> Examples {
            get {
                yield return new Example("Run as client", new ClientOptions { Host = "localhost", Port = 7878, Room = "<room>", Login = "<login>" });
            }
        }
    }
    
    #endregion

    class Program {
        static int StartClient(ClientOptions options) {
            Console.WriteLine("Starting client...");
            try {
                var addresses = Dns.GetHostAddresses(options.Host);
                if(addresses.Length == 0) {
                    throw new ArgumentException(String.Format("Hostname \"{0}\" can not be resolved", options.Host));
                }         

                using (var client = new Client(options.Room, options.Login, new IPEndPoint(addresses[0], options.Port))) {
                    client.Run();
                }
                return 0;
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                return 2;
            }
        }

        static int StartServer(ServerOptions options) {
            Console.WriteLine("Starting server...");
            try {
                using (var server = new Server(new IPEndPoint(new IPAddress(0), options.Port))) {
                    server.Run();
                }
                return 0;
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                return 2;
            }
        }

        static int Main(string[] args) {
            return Parser.Default.ParseArguments<ClientOptions, ServerOptions>(args)
                .MapResult(
                (ClientOptions options) => StartClient(options),
                (ServerOptions options) => StartServer(options),
                (error) => 1);
        }
    }
}
