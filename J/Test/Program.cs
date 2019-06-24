namespace J.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            new LeetCode().Debug();
        }
    }

    public interface IDebug
    {
        void Debug();
    }
}