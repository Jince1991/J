using J.LeetCodeTest;

namespace J.Debug
{
    class LeetCode : IDebug
    {
        public void Debug()
        {
            ISolution solution = new FindMedianSortedArrays();
            solution.Test();
        }
    }
}