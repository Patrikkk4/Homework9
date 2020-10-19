using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace UknownApp
{
    /// <summary>
    /// Класс для десереализации выбранного файла json
    /// </summary>
    class loadFile
    {
        #region Методы
        /// <summary>
        /// Метод десереализует выбранный файл json
        /// </summary>
        public static void loadJsonFile()
        {
            // Пустая переменная для пути к файлу json
            string filePath = string.Empty;

            // Экземпляр класса диалогового окна выбора файла
            OpenFileDialog opn = new OpenFileDialog();

            // Фильт расширений файлов
            opn.Filter = "Json (*.json)| *.json";

            // Условие при корректном выборе файла
            if(opn.ShowDialog() == true)
            {
                // Присвоение переменной пути к файлу
                filePath = opn.FileName;

                // Поток чтения файла
                var temp = File.ReadAllText(filePath);

                // Десереализация файла json
                var openedFile = JsonConvert.DeserializeObject<ObservableCollection<Tbot>>(temp);

                // Цикл добавление элементов в коллекцию
                foreach(var t in openedFile)
                {
                    // Добавление в коллекцию
                    SearchNoUsed.videos.Add(t);
                }
            }
        }
        #endregion
    }
}
