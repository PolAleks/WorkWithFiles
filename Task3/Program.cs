namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Тест";

            WorkWithDirectory workDir = new WorkWithDirectory(path);

            workDir.Delete();
        }
    }
}