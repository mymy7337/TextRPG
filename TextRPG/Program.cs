namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new GameManager();
            GameManager.instance.Start();
        }
    }
}
