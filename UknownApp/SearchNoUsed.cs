using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace UknownApp
{
    /// <summary>
    /// Клас запроса поиска видео на youtube. БОЛЬШЕ НЕ ИСПОЛЬЗУЕТСЯ
    /// Требует Google.Apis.YouTube.v3; Google.Apis.Services;
    /// </summary>
    internal class SearchNoUsed
    {
        #region Переменные

        // Коллекция результатов поиска
        public static ObservableCollection<Tbot> videos { get; set; } = new ObservableCollection<Tbot>();

        #endregion

        //#region Методы
        ///// <summary>
        ///// Метод инициализирует поток поиска видео на youtube
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public async Task SearchMethod(string msg, string userId)
        //{
        //    // Инициализация экземпляра сервисов Google
        //    YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        // Ключ для использования Api Google
        //        ApiKey = "AIzaSyAsja9tn63gQb6GNXVHHtqTBeUQW2zqceE"
        //    });
            
        //    // Коллекция с параметрами результата поиска 
        //    var search = youtube.Search.List("snippet");
            
        //    // Ввод параметра поиска
        //    search.Q = msg;
        //    // Максимальное количество выводимых результатов поиска
        //    search.MaxResults = 5;

        //    // Коллекция содержит результаты поска по введенному параметру
        //    var response = await search.ExecuteAsync();

        //    // Цикл вывода результата поиска
        //    foreach (var result in response.Items)
        //    {
        //        // Условие отсечения каналов youtube. Выводятся только названия и ссылки на видео
        //        if (result.Id.VideoId != null)
        //        {
        //            //Добавление названий и ссылок на видео с youtube в конечную коллекцию с результатами
        //            MainWindow.Self.lsbVideos.Dispatcher.Invoke(() => videos.Add(new Tbot()
        //            {
        //                Name = result.Snippet.Title,
        //                Link = "https://www.youtube.com/embed/" + result.Id.VideoId,
        //                Id = result.Id.VideoId
        //            }));

        //            // Отправка сообщений с результатом
        //            await MainWindow.tBot.SendTextMessageAsync(userId, string.Format("Название: {0}, \nАдрес: {1}", result.Snippet.Title, "https://www.youtube.com/watch?v=" + result.Id.VideoId));
        //        }
        //    }
        //}

        //public async Task SearchMethod(string msg)
        //{
        //    // Инициализация экземпляра сервисов Google
        //    YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        // Ключ для использования Api Google
        //        ApiKey = "AIzaSyAsja9tn63gQb6GNXVHHtqTBeUQW2zqceE",
        //    });

        //    // Коллекция с параметрами результата поиска 
        //    var search = youtube.Search.List("snippet");

        //    // Ввод параметра поиска
        //    search.Q = msg;
        //    // Максимальное количество выводимых результатов поиска
        //    search.MaxResults = 20;

        //    // Коллекция содержит результаты поска по введенному параметру
        //    var response = await search.ExecuteAsync();

        //    // Цикл вывода результата поиска
        //    foreach (var result in response.Items)
        //    {
        //        // Условие отсечения каналов youtube. Выводятся только названия и ссылки на видео
        //        if (result.Id.VideoId != null)
        //        {
        //            // Добавление названий и ссылок на видео с youtube в конечную коллекцию с результатами
        //            videos.Add(new Tbot()
        //            {
        //                Name = result.Snippet.Title,
        //                Link = "https://www.youtube.com/embed/" + result.Id.VideoId,
        //                Id = result.Id.VideoId
        //            });
        //        }
        //    }
        //}
        //#endregion
    }
}
