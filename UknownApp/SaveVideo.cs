using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using VideoLibrary;

namespace UknownApp
{
    /// <summary>
    /// Класс скачивает выбранное видео по ссылке
    /// </summary>
    class SaveVideo
    {
        #region Переменые
        // Инициализация экземпляра класса выбора пути сохраняемого файла
        
        #endregion

        #region Методы
        // Пустая переменная для пути сохранения файла
        static string savePath = string.Empty;
        /// <summary>
        /// Метод осуществляет скачивание файла на локальный диск пользователя
        /// </summary>
        public static void SaveVideoYt(string link)
        {
            SaveFileDialog sfdSaveVideo = new SaveFileDialog();

            // Фильтр сохраняемого файла
            sfdSaveVideo.Filter = "Видео (*.mp4)|*.mp4";

            // Условие проверки на пустой список ListView
            if (MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => MainWindow.Self.lsbVideos.Items.Count == 0))
            {
                MessageBox.Show("Похоже твой список пуст");
            }
            // Условие при выборе нескольких Items в ListView
            else if (MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => MainWindow.Self.lsbVideos.SelectedItems.Count > 1))
            {
                MessageBox.Show("Выбери что-то одно");
            }
            // Проверка на наличие пути к видео
            else if (!string.IsNullOrEmpty(MainWindow.Self.linkVideo))
            {
                // Условие открытия диалогового окна сохранения файла
                if (sfdSaveVideo.ShowDialog() == true)
                {
                    // очистка TextBlock с результатом выполнения сохранения
                    MainWindow.Self.progress.Dispatcher.Invoke(() => MainWindow.Self.txtResult.Text = null);

                    // Запуск прогрессбара
                    MainWindow.Self.progress.Dispatcher.Invoke(() => MainWindow.Self.progress.IsIndeterminate = true);

                    // присвоение переменной пути к сохраняемому файлу
                    savePath = sfdSaveVideo.FileName;

                    // Инициализация класса скачивания видео
                    var saveVideo = YouTube.Default;

                    // Скачивание видео
                    var video = saveVideo.GetVideo(link);

                    // присвоение перменной положительного ответа при удачном скачивании видео
                    string res = "Готово :)";

                    try
                    {
                        // Сохранение файла на диск
                        File.WriteAllBytes(savePath, video.GetBytes());
                    }
                    catch
                    {
                        // Присвоение переменной ответа на неудачное скачивание видео
                        res = "Неудача :(";
                        MessageBox.Show("Ошибка скачивания :(");
                    }

                    // Остановка прогрессбара
                    MainWindow.Self.progress.Dispatcher.Invoke(() => MainWindow.Self.progress.IsIndeterminate = false);

                    // Вывод результата
                    MainWindow.Self.progress.Dispatcher.Invoke(() => MainWindow.Self.txtResult.Text = res);
                }
            }
            // Условие при отсутствии выбора Item в ListView
            else if (string.IsNullOrEmpty(MainWindow.Self.linkVideo))
            {
                MessageBox.Show("Сначала выбери что-нибудь из списка");
            }
        }

        /// <summary>
        /// Метод сохраняет список ListView в файл json
        /// </summary>
        public static void SaveJson()
        {
            SaveFileDialog sfdJson = new SaveFileDialog();

            // Фильтр расширения файлов сохранения
            sfdJson.Filter = "список (*.json)|*.json";

            // Пустая переменная для пути сохранения файла
            string json = string.Empty;

            // Проверка на отсутствие Items в ListView
            if (MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => MainWindow.Self.lsbVideos.Items.Count == 0))
            {
                MessageBox.Show("Похоже твой список пуст");
            }
            // Условие сохранения выделенных Items в ListView
            else if (MainWindow.Self.lsbVideos.SelectedItems.Count >= 1)
            {
                // Вызов диалогового окна сохранения файла json
                if (sfdJson.ShowDialog() == true)
                {
                    // Присвоение переменной пути сохранения файла
                    savePath = sfdJson.FileName;

                    // Сериализация списка ListView в файл json
                    json = JsonConvert.SerializeObject(MainWindow.Self.lsbVideos.SelectedItems);                   
                }
            }
            // Условие сохранения всего списка ListView
            else
            {
                // Вызов диалогового окна сохранения файла json
                if (sfdJson.ShowDialog() == true)
                {
                    // Присвоение переменной пути сохранения файла
                    savePath = sfdJson.FileName;

                    // Сериализация списка ListView в файл json
                    json = JsonConvert.SerializeObject(MainWindow.Self.lsbVideos.Items);
                }
            }

            try
            {
                // Запись данных в файл json
                File.WriteAllText(savePath, json);
            }
            catch
            {

            }

        }
        #endregion
    }

}
