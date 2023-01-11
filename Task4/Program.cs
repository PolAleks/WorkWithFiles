using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathSourseFile = Path.Combine(path, @"Students.dat");

            ReadBinaryFile(pathSourseFile);

        }
        static void ReadBinaryFile(string path) 
        { 

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Console.WriteLine(path);
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        var student = (Student[])formatter.Deserialize(fs);
                        Console.WriteLine(student[0].Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                }
            }
            
        }
    }
}