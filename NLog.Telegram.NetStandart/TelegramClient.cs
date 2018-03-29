using System;
using System.Net.Http;

namespace NLog.Telegram.NetStandart
{
    public class TelegramClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        public void Send(string url)
        {
            try
            {
                var result = _httpClient.GetAsync(url).Result;
            }
            catch (Exception e)
            {
                OnError(e);
            }
        }


        public event Action<Exception> Error;


        private void OnError(Exception obj)
        {
            Error?.Invoke(obj);
        }
    }
}