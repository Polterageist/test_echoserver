using System;
using System.Diagnostics;
using System.Net;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace TestEchoServer {
    #region options
    
    class Options {
        [Option('p', "port", Default = 7878, HelpText = "Port to listen")]
        public int Port { get; set; }

        [Usage(ApplicationAlias = "TestEchoServer")]
        public static IEnumerable<Example> Examples {
            get {
                yield return new Example("Run at specified port", new Options { Port = 7878 });
            }
        }
    }   
    #endregion

    class Program {
        static int StartServer(Options options) {
            Console.WriteLine("Starting server...");
            try {
                var server = new Server(new IPEndPoint(new IPAddress(0), options.Port));
                server.Run();
                
                return 0;
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                return 2;
            }
        }

        static int Main(string[] args) {
            return Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                (Options options) => StartServer(options),
                (error) => 1);
        }
    }
}
