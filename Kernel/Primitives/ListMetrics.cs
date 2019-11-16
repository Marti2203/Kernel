#define FastCopyES
using Kernel.Arithmetic;
using static CarFamily;
namespace Kernel.Primitives
{
    public static class ListMetrics
    {
        public static Integer Pairs(List metric)
        => metric is Null ? 0 : Car<Integer>(metric as Pair);
        public static Boolean WithNull(List metric)
        => metric is Null || Cdar<Integer>(metric as Pair) != 0;
        public static Boolean Cyclic(List metric)
        => !(metric is Null) && Cdddar<Integer>(metric as Pair) != 0;
    }
}