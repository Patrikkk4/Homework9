using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using Telegram.Bot;
using System.Net;
using System.Net.Http;


namespace UknownApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region переменные
        public static MainWindow Self { get; set; }

        // Инициализация класса бота
        public static TelegramBotClient tBot;

        // Поле токена
        static string token = "Ваш Telegram Token";

        // Пустая переменная для ссылки на вмидео
        public string linkVideo = string.Empty;
        #endregion

        #region Главное окно
        public MainWindow()
        {
            Self = this;

            InitializeComponent();

            #region Прокси
            // Содержит параметры HTTP-прокси для System.Net.WebRequest класса.
            var proxy = new WebProxy()
            {
                Address = new Uri($"http://151.80.199.89:3128"),
                UseDefaultCredentials = false,
            };

            // Создает экземпляр класса System.Net.Http.HttpClientHandler.
            var httpClientHandler = new HttpClientHandler() { Proxy = proxy };

            // Предоставляет базовый класс для отправки HTTP-запросов и получения HTTP-ответов 
            // от ресурса с заданным URI.
            HttpClient hc = new HttpClient(httpClientHandler);
            #endregion

            // Инициализация нового экземпляра бота
            //tBot = new TelegramBotClient(token);

            // Слежение за событием входящих сообщений
            //tBot.OnMessage += Bot;
            // Запуск проверки обновлений
            //tBot.StartReceiving();
        }
        #endregion

        #region События
        /// <summary>
        /// Событие двойного клика на результат поиска в listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LsbVideos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            if (linkVideo != null)
            {
                Process.Start(linkVideo);
            }
        }

        /// <summary>
        /// Событие при получении сообщения ботом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static async void Bot(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            // Переменная, получающая значение сообщения пользователя
            string text = $"{e.Message.Text}";
            // Переменная, получающая значение ID Чата
            string userId = $"{e.Message.Chat.Id}";

            if (text == "/start" || text == "/Start")
            {
                // Сообщение при старте бота
                await tBot.SendTextMessageAsync(userId, $"{"Привет! Я ищу видео с youtube по ключевым словам. Просто отправь мне сообщение и я пришлю тебе 5 видео :)"}");
            }
            // Условие отправки обратного сообщения
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                // Присваивание текста запроса пользователя полю поиска
                Self.txbSearch.Dispatcher.Invoke(() => { Self.txbSearch.Text = text; });

                try
                {
                    // Запуск метода запроса поиска видео на youtube
                    Task.Run(() => new Search().SearchMethod(text, userId));
                }
                // Отлов ошибок API Google
                catch (Exception ex)
                {
                    // Вывод ошибки на экран
                    await tBot.SendTextMessageAsync(userId, $"{"какая то ошибка :("}, {ex.ToString()}");
                }
            }
            else
            {
                // Сообщение о несоответствии типа запроса
                await tBot.SendTextMessageAsync(userId, $"{"Это не похоже на слова :)"}");
            }
        }
        
        /// <summary>
        /// Событие нажатия кнопки Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Условие при котором происходит событие нажатия кнопки Enter
            if(e.Key == Key.Enter)
            {
                try
                {
                    // Запуск метода поиска видео по ключивым словам
                    Task.Run(() => new Search().SearchMethod(Self.txbSearch.Dispatcher.Invoke(() => Self.txbSearch.Text))); 
                    //await new Search().SearchMethod(Self.txbSearch.Text);
                }
                // Отлов ошибок API Google
                catch (Exception i)
                {
                    // Вывод ошибки на экран
                    MessageBox.Show(i.Message);
                }
            }
        }

        /// <summary>
        /// Событие изменения выбора Item ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LsbVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                try
                {
                    // получение объекта Item из ListView
                    var link = (Tbot)e.AddedItems[0];

                    // Присвоение ссылки на видео переменной
                    linkVideo = link.Link;

                    // При смене item включить видимость WebView
                    browser.Visibility = Visibility.Visible;

                    // Обращение по ссылке через WebView
                    browser.Navigate(new Uri(linkVideo));
                }
                catch
                {
                    // Обновление списка ListView
                    lsbVideos.Items.Refresh();
                }
            }
            catch
            {
                // Обновление списка ListView
                lsbVideos.Items.Refresh();
            }
        }
        
        /// <summary>
        /// Событие нажатия кнопки скачивания видео
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Запуск метода скачивания видео в оновом потоке
          Task.Run(() => SaveVideo.SaveVideoYt(linkVideo));
        }

        /// <summary>
        /// Событиенажатия кнопки сохранения Items ListView в файл json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnJson_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода сохранени списка Items ListView в файл json
            SaveVideo.SaveJson();
        }

        /// <summary>
        /// Событие нажатия кнопки открытия файла json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnJsonOpen_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода десериализации файла json
            loadFile.loadJsonFile();
        }

        /// <summary>
        /// Событие нажатия кнопки обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очистка ListView
            Search.videos.Clear();
            // Скрытие браузера
            browser.Visibility = Visibility.Hidden;
            // Очистка TextBox запроса
            txbSearch.Text = null;
        }
        #endregion
    }
}
