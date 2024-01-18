using System.Diagnostics;
using System.Management;

namespace Detect_msg_process
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "calc";
            UseProcessClass(name);
            Console.WriteLine("!.!.!.!.!.!.!.!.!.!.!.!.!.!.!.!.");
            //◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•
            UseManagementEventWatcher(name);


        }
        private static void UseProcessClass(string name)
        {
            bool c = true;
                Console.WriteLine();
             Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
             while (c && stopwatch.ElapsedMilliseconds < 2000)
            {
                Process[] processes = Process.GetProcesses();
                var process = processes.SingleOrDefault(p => p.ProcessName.Contains(name));
                if (stopwatch.ElapsedMilliseconds % 100 == 0  ) 
                {
                    Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());
                    Console.Write(". ");
                
                }
                
                if (process != null)
                {
                    Console.WriteLine("msg.exe is running.");
                    //process.Kill();
                    //Console.WriteLine("process killed");
                    c = false;
                }
            }
            Console.WriteLine("");
            var text = c ? "not found" : "msg.exe found!";
            Console.WriteLine(text);
        }
        private static void UseManagementEventWatcher(string processName)
        {
            Console.WriteLine("1");
            string queryString = $"SELECT TargetInstance FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process' AND TargetInstance.Name LIKE '%{processName}%'";
            Console.WriteLine("2");

            string scope = @"\\.\root\CIMV2";

            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, queryString);
            watcher.EventArrived += new EventArrivedEventHandler(ProcessCreated);

            try
            {
                watcher.Start();
                var t = watcher.Options;
                watcher.WaitForNextEvent();
                watcher.Stop();
                watcher.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception :");
                Console.WriteLine(e.Message);
                throw e;
            }
            Console.WriteLine("5");
        }
        private static void ProcessCreated(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("3");
            Console.WriteLine("Process created");
            Console.WriteLine("4");
        }
    }
}
