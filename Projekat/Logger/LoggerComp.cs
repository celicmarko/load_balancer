using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerComp
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(LoggerService)))
            {
                serviceHost.AddServiceEndpoint(typeof(ISlanjePoruke), new NetTcpBinding(), new Uri("net.tcp://localhost:5500/ISlanjePoruke"));
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
