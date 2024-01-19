using System.Diagnostics;
using System.Management;

namespace Detect_msg_process
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "msg";
            Console.WriteLine("._._._._._._._x86._._._._._._._._._");
            //UseProcessClass_GetProcessesByName(name);


            //UseProcessClass(name);
            //◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•◘•
            UseManagementEventWatcher(name);

            Console.ReadKey();
            Console.WriteLine("._._._._._._._x86._._._._._._._._._");
        }

        private static void UseProcessClass_GetProcessesByName(string name)
        {
            bool c = true;
            while (c)
            {
                Process[] processes = Process.GetProcesses();
                var process = processes.SingleOrDefault(p => p.ProcessName.Contains(name));


                int ID = process != null ? process.Id : 0;

                if (process != null)
                {

                    Console.WriteLine("msg.exe is running.");
                    Process[] prcs = Process.GetProcessesByName(name);

                    Process instance = Process.GetProcessById(ID);
                }
            }
            Console.WriteLine("");
            var text = c ? "not found" : "msg.exe found!";
            Console.WriteLine(text);
        }


        private static void UseProcessClass(string name)
        {
            bool c = true;
            Console.WriteLine();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (c)
            {
                Process[] processes = Process.GetProcesses();
                var process = processes.SingleOrDefault(p => p.ProcessName.Contains(name));
                if (process != null)
                {
                    Console.WriteLine("msg.exe is running.");
                }
            }
            Console.WriteLine("");
            var text = c ? "not found" : "msg.exe found!";
            Console.WriteLine(text);
        }
        private static void UseManagementEventWatcher(string processName)
        {
            string queryString =
                $"SELECT TargetInstance " +
                $"FROM __InstanceCreationEvent " +
                $"WITHIN 1 " +
                $"WHERE TargetInstance ISA 'Win32_Process' AND TargetInstance.Name LIKE '%{processName}%'";
            Console.WriteLine("2");
            string scope = @"\\.\root\CIMV2";

            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, queryString);
            watcher.EventArrived += new EventArrivedEventHandler(ProcessCreated);

            try
            {
                bool control = true;
                while (control)
                {
                    watcher.Start();
                    watcher.WaitForNextEvent();
                    watcher.Stop();
                    watcher.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception :");
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        private static void ProcessCreated(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("ManagementEventWatcher");
        }
    }
}
