using Common;
using System;
using System.ServiceModel;

namespace Logger
{
    class LoggerComp
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(LoggerService)))
            {
                NetTcpBinding binding = new NetTcpBinding();

                serviceHost.AddServiceEndpoint(typeof(ISlanjePoruke), binding, new Uri("net.tcp://localhost:4000/ISlanjePoruke"));
                serviceHost.Open();

                while (true)
                {
                    if (LoggerService.primljenaPoruka != "")
                    {
                        Console.WriteLine(LoggerService.primljenaPoruka);
                        LoggerService.primljenaPoruka = "";
                    }
                }
            }
        }
    }
}
