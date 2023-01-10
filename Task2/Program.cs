namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            long size = GetTotalSpace(path);

            Console.WriteLine($"{size / 1048576} МБ ({size}) байт");
        }
        static long GetTotalSpace(string directory)
        {
            long totalSpace = 0;
            try
            {
                DirectoryInfo dir;
                if (Directory.Exists(directory))
                {
                    dir = new DirectoryInfo(directory);
                }
                else return 0;

                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (var d in dirs)
                {
                    totalSpace += GetTotalSpace(d.FullName);
                }

                FileInfo[] arrayFile = dir.GetFiles();
                foreach (var file in arrayFile)
                {
                    totalSpace += file.Length;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        return totalSpace;    
        }
    }
}