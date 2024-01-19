using System;
using System.Management;

class Program
{
    static void Main(string[] args)
    {
        WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 0, 0, 0 , 1), "TargetInstance isa 'Win32_Process'");
        ManagementEventWatcher watcher = new ManagementEventWatcher(query);
        watcher.EventArrived += new EventArrivedEventHandler(ProcessStarted);
        watcher.Start();
        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable) System.Threading.Thread.Sleep(500);
        watcher.Stop();
    }

    static void ProcessStarted(object sender, EventArrivedEventArgs e)
    {
        ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
        Console.WriteLine("New process started: {0}", instance["Name"]);
    }
}
