using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace UknownApp
{
    /// <summary>
    /// Клас принимает три параметра полученых видео
    /// </summary>
    public class Tbot
    {
        #region Свойства
        /// <summary>
        /// Свойство названия видео
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// Свойство ссылки видео
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Свойство ID видео
        /// </summary>
        public string Id { get; set; }
        #endregion
    }
}
