using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace UknownApp
{
    class Search
    {
        #region Переменные

        // Коллекция результатов поиска
        public static ObservableCollection<Tbot> videos { get; set; } = new ObservableCollection<Tbot>();

        #endregion

        #region Методы
        /// <summary>
        /// Метод инициализирует поток поиска видео на youtube
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async void SearchMethod(string msg, string userId)
        {
            // Переменная колличества страниц результатов поиска
            int pages = 1;

            // Инициализация класса поиска видео
            var response = new VideoSearch();
            
            // Цикл вывода результата поиска
            foreach (var result in response.SearchQuery(msg, pages))
            {
                //Получение ID видео
                var id = result.Url.Substring(result.Url.LastIndexOf('=') + 1);
                // Получение ссылки на просмотр в режиме полного экрана
                var link = "https://www.youtube.com/embed/" + result.Url.Substring(result.Url.LastIndexOf('=') + 1);

                //Добавление названий и ссылок на видео с youtube в конечную коллекцию с результатами
                MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => videos.Add(new Tbot()
                {
                    Name = result.Title,
                    Link = link,
                    Id = id
                }));

                // Отправка сообщений с результатом
                //await MainWindow.tBot.SendTextMessageAsync(userId, string.Format("Название: {0}, \nАдрес: {1}", result.Title, result.Url));
            }

            // Колличество отправляемых ссылок пользователю в Telegram
            var howMuch = 5;

            for(int i = 0; i < howMuch; i++)
            {
                // Отправка 5 результатов пользователю в Telegram
                await MainWindow.tBot.SendTextMessageAsync(userId, string.Format("Название: {0}, \nАдрес: {1}", videos[i].Name, videos[i].Link));
            }
        }

        public void SearchMethod(string msg)
        {
            // Переменная колличества страниц результатов поиска
            int pages = 1;

            // Инициализация класса поиска видео
            var response = new VideoSearch();

            // Цикл вывода результата поиска
            foreach (var result in response.SearchQuery(msg, pages))
            {
                //Получение ID видео
                var id = result.Url.Substring(result.Url.LastIndexOf('=')+1);
                // Получение ссылки на просмотр в режиме полного экрана
                var link = "https://www.youtube.com/embed/"+result.Url.Substring(result.Url.LastIndexOf('=') + 1);

                //Добавление названий и ссылок на видео с youtube в конечную коллекцию с результатами
                MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => videos.Add(new Tbot()
                {
                    Name = result.Title,
                    Link = link,
                    Id = id
                }));
            }

        }
        #endregion
    }
}
