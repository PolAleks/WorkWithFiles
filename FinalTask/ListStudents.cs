using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    internal class ListStudents
    {
        private Student[] students;

        private bool isCorectInstantiation = true;

        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private const string DESTINATION_FOLDER = @"\Students\";

        private List<string> group = new List<string>();
        public ListStudents(string path)
        {
            if (String.IsNullOrEmpty(path))
                path = desktopPath + @"Students.data";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        students = (Student[])formatter.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                }
            }
            if (students == null)
                isCorectInstantiation = false;
            FillGroup();
        }
        private Student this[int index]
        { 
            get 
            {
                if (index < 0 || index > students.Length )
                {
                    Console.WriteLine("Вы вышли за допустимый диапазон записей!" +
                                      "Вам вернется запись на 1 студента");
                    return students[0];
                }
                else
                    return students[index];
            }

        }
        /// <summary>
        /// Заполняет список доступных групп  
        /// </summary>
        private void FillGroup()
        {
            if (isCorectInstantiation)
            {
                group.Add(students[0].Group);
                for (int i = 1; i < students.Length; i++)
                {
                    if (!group.Contains(students[i].Group))
                    {
                        group.Add(students[i].Group);
                    }
                }
                group.Sort();
            } 
        }
        /// <summary>
        /// Сохраняет список студентов в файлы с одноименным названием групп
        /// </summary>
        public void Save()
        {
            if(isCorectInstantiation)
            {
                desktopPath += DESTINATION_FOLDER;

                if (!Directory.Exists(desktopPath))
                    Directory.CreateDirectory(desktopPath); //Создаем директорию для файлов если её нет

                foreach (var gr in group)
                {
                    string fileName = desktopPath + gr + ".txt"; //Полное имя файла включающее название группы

                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        string studentFromGroup = "";
                        foreach (var student in students)
                        {
                            if (gr == student.Group)
                                studentFromGroup += String.Format($"Студент {student.Name}, дата рождения {student.DateOfBirth.ToShortDateString()}\n");
                        }
                        sw.Write(studentFromGroup);
                    }

                }
                Console.WriteLine("Запись произведена корректно. Проверьте папку Students на рабочем столе");
            }
            else
            {
                Console.WriteLine("Файл с базой студентов не был корректно передан программе или поврежден");
            }
        }
    }
}
