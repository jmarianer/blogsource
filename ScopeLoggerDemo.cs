// vim: sw=4

using System;
using System.Threading;
using System.Threading.Tasks;

using static Common;

class Program
{
    public static void Main(string[] args)
    {
        var call1 = CallServer(5, "server1");
        var call2 = CallServerAsync(5, "server2").Result;
        Log($"Results: {call1}, {call2}");
    }
}
