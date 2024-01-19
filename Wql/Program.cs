using System.Management;

//namespace Wql
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            UseManagementEventWatcher();
//        }
//        private static void UseManagementEventWatcher()
//        {
//            string queryString =
//                $"SELECT TargetInstance " +
//                $"FROM __InstanceCreationEvent " +
//                $"WITHIN 0.001 " +
//                $"WHERE TargetInstance ISA 'Win32_Process'"  
//                //AND TargetInstance.Name LIKE '%{processName}%'
//                ;
//            Console.WriteLine("2");
//            string scope = @"\\.\root\CIMV2";

//            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, queryString);
//            //watcher.EventArrived += new EventArrivedEventHandler(ProcessCreated);

//            try
//            {
//                bool control = true;
//                while (control)
//                {
//                    watcher.Start();
//                    watcher.WaitForNextEvent();
//                    watcher.Stop();
//                    watcher.Dispose();
//                }

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Exception :");
//                Console.WriteLine(e.Message);
//                throw e;
//            }
//        }



//    }
//}
