using System;

namespace NLog.Telegram.NetStandart.Sample
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                throw new ApplicationException("Any Exception");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "TestException");
            }

            Console.WriteLine("Done. check chat");
            Console.ReadLine();
        }
    }
}
