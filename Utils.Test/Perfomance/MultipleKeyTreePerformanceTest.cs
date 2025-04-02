using NUnit.Framework.Internal;
using NUnit.Framework;
using System.Diagnostics;
using Utils.Collections;

namespace Utils.Test.Perfomance
{
    [TestFixture]
    public class MultipleKeyTreePerformanceTest
    {
        private const int ITERATIONS_COUNT = 70000;
        private const int KEYS_COUNT = 3;


        [Test]
        public void CompareMultipleKeyTreeWithStringDictionary()
        {
            MultipleKeyTree<string, string> multipleKeyTree = new MultipleKeyTree<string, string>();
            Dictionary<string, string> stringDictionary = new Dictionary<string, string>();
            Random random = new Random(42);
            List<string[]> keys = new List<string[]>();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                string[] keyParts = new string[KEYS_COUNT];
                for (int j = 0; j < KEYS_COUNT; j++)
                {
                    keyParts[j] = $"C:\\Program Files\\WindowsApps\\ROBLOXCORPORATION.ROBLOX_2025.315.2148.0_neutral_~_55nm5eh3cm0pr{j}_{random.Next(1000)}";
                }

                keys.Add(keyParts);
            }

            Stopwatch addToTreeStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                multipleKeyTree.Add(keys[i], $"value_{i}");
            }

            addToTreeStopwatch.Stop();
            Stopwatch addToStringDictionaryStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                string stringKey = string.Join("|", keys[i]);
                stringDictionary[stringKey] = $"value_{i}";
            }

            addToStringDictionaryStopwatch.Stop();
            string treeValue = null;
            Stopwatch getFromTreeStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                multipleKeyTree.TryGetValue(keys[i], out treeValue);
            }

            getFromTreeStopwatch.Stop();
            string stringDictValue = null;
            Stopwatch getFromStringDictionaryStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                string stringKey = string.Join("|", keys[i]);
                stringDictionary.TryGetValue(stringKey, out stringDictValue);
            }
            getFromStringDictionaryStopwatch.Stop();

            Stopwatch removeFromTreeStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                multipleKeyTree.Remove(keys[i]);
            }

            removeFromTreeStopwatch.Stop();
            Stopwatch removeFromStringDictionaryStopwatch = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                string stringKey = string.Join("|", keys[i]);
                stringDictionary.Remove(stringKey);
            }

            removeFromStringDictionaryStopwatch.Stop();
            Console.WriteLine("\nPerformance comparison between MultipleKeyTree and Dictionary with String key:");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"Add operation - MultipleKeyTree: {addToTreeStopwatch.ElapsedMilliseconds} ms, String Dictionary: {addToStringDictionaryStopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Get operation - MultipleKeyTree: {getFromTreeStopwatch.ElapsedMilliseconds} ms, String Dictionary: {getFromStringDictionaryStopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Remove operation - MultipleKeyTree: {removeFromTreeStopwatch.ElapsedMilliseconds} ms, String Dictionary: {removeFromStringDictionaryStopwatch.ElapsedMilliseconds} ms");
        }
    }
}
