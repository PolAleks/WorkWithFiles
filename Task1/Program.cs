namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DestructInactive destruct = new DestructInactive();

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Тест";
            string path = @"F:\\СКАНИРОВАНИЕ";
            destruct.Delete(path);

        }
    }
}