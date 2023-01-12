using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class DestructInactive
    {
        private string Path { get; set; } = String.Empty;
        
        private DirectoryInfo directory;
        
        public void Delete(string path)
        {
            Path = CreateDir(path);
            try
            { 
                directory = new DirectoryInfo(Path);

                DirectoryInfo[] arrDirectory = directory.GetDirectories();

                foreach (var dir in arrDirectory)
                {
                    if (IsAccessMore30Min(dir))
                    {
                        Console.WriteLine($"Последнее обращение к каталогу *{dir.Name}* было " +
                                          $"{(uint)(DateTime.Now - dir.LastAccessTime).TotalMinutes} " +
                                          $"минут назад - папка будет удалена!");
                        dir.Delete(true);
                    }
                }
            }
            catch(Exception ex)
            {             
                Console.WriteLine(ex.Message);
            }
            try
            {
                FileInfo[] arrFile = directory.GetFiles();

                foreach (var file in arrFile)
                {
                    if (IsAccessMore30Min(file))
                    {
                        Console.WriteLine($"Последнее обращение к файлу *{file.Name}* было " +
                                          $"{(uint)(DateTime.Now - file.LastAccessTime).TotalMinutes} " +
                                          $"минут назад - файл будет удален!");
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private bool IsAccessMore30Min(FileSystemInfo arr)
        {
            if ((DateTime.Now - arr.LastAccessTime) > TimeSpan.FromMinutes(30))
                return true;
            return false;
        }
        private string CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                try 
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            return path;
        }
        
    }
}
