using Common;

namespace Logger
{
    public class LoggerService : ISlanjePoruke
    {
        public static string primljenaPoruka = "";

        public void SlanjeMerenja(string merenje)
        {
            primljenaPoruka = merenje;
        }
    }
}
