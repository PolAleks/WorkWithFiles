using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class WorkWithDirectory
    {
        private string Path { get; set; }

        private DirectoryInfo directory;

        public WorkWithDirectory(string path)
        {
            Path = CreateDir(path);
            directory = new DirectoryInfo(Path);
        }
        /// <summary>
        /// Возврашает размер каталога/файла в байтах
        /// </summary>
        /// <param name="directory">путь к каталогу</param>
        /// <returns></returns>
        private static long GetTotalSpace(string directory)
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
        /// <summary>
        /// Удаление каталогов и файлов, последние обращение к которым было более 30 минут
        /// </summary>
        public void Delete()
        {
            long totalSpace = GetTotalSpace(Path);
            long freeSpace = 0;
            int amountFileDelete = 0;
            Console.WriteLine($"Размер каталога до удаления {totalSpace / 1048576} МБ ({totalSpace}) байт");
            try
            {
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
            catch (Exception ex)
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
                        amountFileDelete++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            freeSpace = totalSpace - GetTotalSpace(Path);
            Console.WriteLine($"После удаления {amountFileDelete} файлов " +
                              $"освобождено {freeSpace} байт\n" +
                              $"Размер каталога после удаления {GetTotalSpace(Path)} байт.");

        }
        /// <summary>
        /// true если обращение к Каталогу/файлу более 30 минут
        /// </summary>
        /// <param name="arr">Каталог/файл</param>
        /// <returns></returns>
        private bool IsAccessMore30Min(FileSystemInfo arr)
        {
            if ((DateTime.Now - arr.LastAccessTime) > TimeSpan.FromMinutes(30))
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает string путь к каталогу который существует
        /// </summary>
        /// <param name="path">Путь к директории</param>
        /// <returns></returns>
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
