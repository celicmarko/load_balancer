using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;

namespace Worker_Cmp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Pokretanje instance sa portom

            Worker server = new Worker();
            TcpChannel channel = new TcpChannel(int.Parse(args[0]));
            ChannelServices.RegisterChannel(channel, false);
            string uri = "Worker";
            RemotingServices.Marshal(server, uri, server.GetType());

            Console.WriteLine("[Worker]: Spreman za rad!\n");
            Console.WriteLine("PORT: " + args[0]);

            // beskonacna petlja dok se program ne ugasi

            while (true)
            {
                Thread.Sleep(5000); // provera rada na svakih 5 sekundi
            }
        }
    }
}
