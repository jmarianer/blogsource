// vim: sw=4

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

static class Common
{
    public static void Log(string log)
    {
        var ElapsedTime = DateTime.Now - Process.GetCurrentProcess().StartTime;
        Console.WriteLine($@"[{ElapsedTime:mm'm'ss's'}] {log}");
    }

    public class ScopeLogger : IDisposable
    {
        string name;

        public ScopeLogger(string name)
        {
            this.name = name;
            Log($"Entering {name}");
        }

        public void Dispose()
        {
            Log($"Leaving {name}");
        }
    }

    public static int CallServer(int delaySecs, string serverName)
    {
        using (new ScopeLogger($"CallServer for {serverName}"))
        {
            Thread.Sleep(TimeSpan.FromSeconds(delaySecs));
            return 0;
        }
    }

    public static async Task<int> CallServerAsync(int delaySecs, string serverName)
    {
        using (new ScopeLogger($"CallServerAsync for {serverName}"))
        {
            await Task.Delay(TimeSpan.FromSeconds(delaySecs));
            return 0;
        }
    }
}
