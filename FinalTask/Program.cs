using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathSourseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students.dat");

            ListStudents students = new ListStudents(pathSourseFile);

            students.Save();
        }
        
    }
}