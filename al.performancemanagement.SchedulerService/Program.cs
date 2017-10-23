using System;
using System.Diagnostics;
using System.ServiceModel;

namespace al.performancemanagement.SchedulerService
{
    class Program
    {
        private static ServiceHost service;

        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower().StartsWith("-c"))
            {
                TextWriterTraceListener[] listener = new TextWriterTraceListener[] { new TextWriterTraceListener(Console.Out) };
                Debug.Listeners.AddRange(listener);
                try
                {
                    service = new ServiceHost(SchedulerService.GetService());
                    service.Open();
                    Trace.WriteLine("Scheduler Service Started...");
                    Trace.WriteLine("Press Enter to close Service");
                    Console.ReadLine();
                    service.Close();
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message + "-" + e.StackTrace);
                    Console.ReadLine();
                }
            }
        }
  
    }
}
