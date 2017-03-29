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
        [Option('a', "address", Default = ":7878", HelpText = "IP address to listen")]
        public string Address { get; set; }

        [Usage(ApplicationAlias = "TestEchoServer")]
        public static IEnumerable<Example> Examples {
            get {
                yield return new Example("Run as server", new ServerOptions { Address = ":7878" });
            }
        }
    }

    [Verb("client", HelpText = "Run program in client mode")]
    class ClientOptions {
        [Option('a', "address", Default = "localhost:7878", HelpText = "Server's address")]
        public string Address { get; set; }

        [Value(0, Required = true, MetaName = "room", HelpText = "Room name to connect")]
        public string Room { get; set; }

        [Value(1, Required = true, MetaName = "login", HelpText = "Client id")]
        public string Login { get; set; }

        [Usage(ApplicationAlias = "TestEchoServer")]
        public static IEnumerable<Example> Examples {
            get {
                yield return new Example("Run as client", new ClientOptions { Address = "localhost:7878", Room = "<room>", Login = "<login>" });
            }
        }
    }
    
    #endregion

    class Program {
        static int StartClient(ClientOptions options) {
            Console.WriteLine("Starting client...");
            return 0;
        }

        static int StartServer(ServerOptions options) {
            Console.WriteLine("Starting server...");
            return 0;
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
