using BenchmarkDotNet.Attributes;
using System.Linq;
using System.Text;

namespace Benchmarks
{
    public class IfElseBenchmark
    {
        private const int TOTAL_LIST_COUNT = 10000;

        [Benchmark]
        public bool IfOnly()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Append(i);

                if (stringBuilder.Length > 10000)
                    return true;

                if (stringBuilder.Length > 9000)
                    return true;

                if (stringBuilder.Length > 8500)
                    return true;

                if (stringBuilder.Length <= 8500)
                    continue;
            }

            return false;
        }

        [Benchmark]
        public bool IfElseIfElse()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Append(i);

                if (stringBuilder.Length > 10000)
                    return true;
                else if (stringBuilder.Length > 9000)
                    return true;
                else if (stringBuilder.Length > 8500)
                    return true;
                else
                    continue;
            }

            return false;
        }
    }
}
