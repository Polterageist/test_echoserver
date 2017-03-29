using System;
using System.Diagnostics;
using System.Net;
using CommandLine;
using CommandLine.Text;

namespace TestEchoServer {
    #region options
    class ServerOptions {
        [Option('a', "address", DefaultValue = ":7878", HelpText = "IP address to listen")]
        public string Address { get; set; }
    }

    class ClientOptions {
        [Option('a', "address", DefaultValue = "localhost:7878", HelpText = "Server's address")]
        public string Address { get; set; }

        [ValueOption(0)]
        public string Room { get; set; }

        [ValueOption(1)]
        public string Id { get; set; }
    }

    class Options {
        [VerbOption("client", HelpText = "Run program in client mode")]
        public ClientOptions ClientVerb { get; set; }

        [VerbOption("server", HelpText = "Run program in server mode")]
        public ServerOptions ServerVerb { get; set; }
    }
    #endregion

    class Program {
        static void StartClient(ClientOptions options) {
            Console.WriteLine("Starting client...");
        }

        static void StartServer(ServerOptions options) {
            Console.WriteLine("Starting server...");
        }

        static void Main(string[] args) {
            string command = null;
            object commandOptions = null;
            var options = new Options();
            if (Parser.Default.ParseArgumentsStrict(args, options, (verb, subOptions) => {
                command = verb;
                commandOptions = subOptions;
            })) {
                if (command.Equals("client")) {
                    StartClient((ClientOptions)commandOptions);
                }
                else if (command.Equals("server")) {
                    StartServer((ServerOptions)commandOptions);
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
