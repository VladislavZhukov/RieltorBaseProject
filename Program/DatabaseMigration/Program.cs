namespace DatabaseMigration
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using DatabaseMigration.XmlCollections;
    using RieltorBase.Domain;

    /// <summary>
    /// Программа для выполнения единовременной миграции
    /// данных из XML в базу данных объектов недвижимости.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Стартовая функция программы.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            try
            {
                DirectoryInfo dir = GetSourceDir(args);

                Console.WriteLine("Получение объектов недвижимости...");
                XmlRealtyObjectsCollection collection =
                    new XmlRealtyObjectsCollection(dir);

                Console.WriteLine("Получение данных, совместимых с БД...");
                IEnumerable<Firm> firms = collection.GetFirms();

                Console.WriteLine("Сохранение данных...");

                int i = 1;
                foreach (Firm firm in firms)
                {
                    try
                    {
                        MigrationContext.DbContext.Firms.Add(firm);
                        MigrationContext.DbContext.SaveChanges();
                        Console.WriteLine("Сохранено фирм: " + i);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    i++;
                }

                Console.WriteLine("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка миграции данных xml.");
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Получить исходную папку, содержащую объекты недвижимости.
        /// </summary>
        /// <param name="args">Параметры командной строки.</param>
        /// <returns>Папка с объектами недвижимости.</returns>
        private static DirectoryInfo GetSourceDir(string[] args)
        {
            bool correctCommandLineParam = args.Length == 1
                && !string.IsNullOrWhiteSpace(args[0]);

            string dirPath;

            if (correctCommandLineParam)
            {
                dirPath = args[0];
            }
            else
            {
                Console.WriteLine("Введите путь к существующей папке apartment: ");
                dirPath = Console.ReadLine() ?? string.Empty;
            }

            DirectoryInfo dir = new DirectoryInfo(dirPath);
            return dir;
        }
    }
}
