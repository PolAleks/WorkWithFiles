using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathSourseFile = Path.Combine(path, @"Students.dat");

            
            ListStudents students = new ListStudents(pathSourseFile);

            students.Save();

        }
        
    }
}