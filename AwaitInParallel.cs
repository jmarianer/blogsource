// vim: sw=4

using System;
using System.Threading;
using System.Threading.Tasks;

using static Common;

class Program
{
    private static async Task<Tuple<int, int>> TwoAwaitsBad()
    {
        var result1 = await CallServerAsync(5, "server1");
        var result2 = await CallServerAsync(5, "server2");
        return Tuple.Create(result1, result2);
    }

    private static async Task<Tuple<int, int>> TwoAwaitsGood()
    {

        var call1 = CallServerAsync(5, "server1");
        var call2 = CallServerAsync(5, "server2");
        return Tuple.Create(await call1, await call2);
    }

    private static async Task<Tuple<int, int>> TwoAwaitsGoodAlternative()
    {

        var call1 = CallServerAsync(5, "server1");
        var call2 = CallServerAsync(5, "server2");
        var result1 = await call1;
        var result2 = await call2;
        return Tuple.Create(result1, result2);
    }

    private static async Task<Tuple<int, int>> TwoAwaitsBest()
    {

        var call1 = CallServerAsync(5, "server1");
        var call2 = CallServerAsync(5, "server2");
        await call1;
        await call2;
        return Tuple.Create(call1.Result, call2.Result);
    }

    public static void Main(string[] args)
    {
        Log("Demo: call two servers in parallel from Main");
        var call1 = CallServerAsync(5, "server1");
        var call2 = CallServerAsync(5, "server2");
        Log($"Results: {call1.Result}, {call2.Result}");

        Log("Demo: two awaits in a task don't run in parallel");
        var results = TwoAwaitsBad();
        Log("Doing other processing while TwoAwaitsBad runs");
        Log($"Results: {results.Result.Item1}, {results.Result.Item2}");

        Log("Demo: two awaits that run the right way");
        results = TwoAwaitsGood();
        Log("Doing other processing while TwoAwaitsGood runs");
        Log($"Results: {results.Result.Item1}, {results.Result.Item2}");

        Log("Demo: two awaits that run the right way (alternative)");
        results = TwoAwaitsGoodAlternative();
        Log("Doing other processing while TwoAwaitsGoodAlternative runs");
        Log($"Results: {results.Result.Item1}, {results.Result.Item2}");

        Log("Demo: two awaits that run the right way (probably the best way)");
        results = TwoAwaitsBest();
        Log("Doing other processing while TwoAwaitsBest runs");
        Log($"Results: {results.Result.Item1}, {results.Result.Item2}");
    }
}
