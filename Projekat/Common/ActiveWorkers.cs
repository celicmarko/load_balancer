namespace Common
{
    public class ActiveWorkers
    {
        private static int workersCount = 0;

        public int WorkersCount { get => workersCount; set => workersCount = value; }
    }
}
