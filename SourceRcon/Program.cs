using System;
using System.Net;
using System.Threading;


namespace SourceRcon
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            string ipaddress, password, command;
            int port;

            bool interactive;

            interactive = false;
            ipaddress = "127.0.0.1";
            port = 27015;
            password = null;
            command = null;

            if (args.Length > 0) {

                if (args.Length == 3)
                {
                    interactive = true;
                    ipaddress = args[0];
                    port = int.Parse(args[1]);
                    password = args[2];


                }
                else
                {
                    return;

                }
            } 

			SourceRcon Sr = new SourceRcon();
			Sr.Errors += new StringOutput(ErrorOutput);
			Sr.ServerOutput += new StringOutput(ConsoleOutput);

            if (Sr.Connect(new IPEndPoint(IPAddress.Parse(ipaddress), port), password))
			{
				while(!Sr.Connected)
				{
					Thread.Sleep(10);
				}
                if(interactive)
                {
                    Console.WriteLine("Ready for commands:");
				    while(true)
				    {
				    	Sr.ServerCommand(Console.ReadLine());
				    }
                }
                else
                {
                    Sr.ServerCommand(command);
                    Thread.Sleep(1000);
                    return;
                }
			}
			else
			{
				Console.WriteLine("No connection!");
				Thread.Sleep(1000);
			}
		}

		static void ErrorOutput(string input)
		{
			Console.WriteLine("{0}", input);
		}

		static void ConsoleOutput(string input)
		{
			Console.WriteLine("{0}", input);
		}

	}
}
