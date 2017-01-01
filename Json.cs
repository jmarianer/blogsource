// vim: sw=4

using System;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using static Common;

class Program
{
    public static void Main(string[] args)
    {
        var myOneOffObject = new {
            someField = 3,
            someOtherField = "name",
            someArray = new[] {1, 2, 3},
            someObjectArray = new[] {
                new { f1 = 1, f2 = "apple" },
                new { f1 = 2, f2 = "pear" },
            }
        };

        Log(JsonConvert.SerializeObject(myOneOffObject, Formatting.Indented));

        var otherOneOffObject = new {
            someField = 3,
            someOtherField = "name",
            someArray = new[] {1, 2, 3},
            someObjectArray = new dynamic[] {
                new { f1 = 1, f2 = "apple" },
                new { f1 = 2, f3 = "pear" },
            }
        };

        Log(JsonConvert.SerializeObject(otherOneOffObject, Formatting.Indented));
    }
}
