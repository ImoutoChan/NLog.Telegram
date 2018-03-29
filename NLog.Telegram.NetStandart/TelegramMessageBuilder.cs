using NLog.Telegram.NetStandart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLog.Telegram.NetStandart
{
    public class TelegramMessageBuilder
    {
        private readonly string _baseUrl;
        private readonly TelegramClient _client;
        private readonly MessageRequest _request;

        /// <summary>
        /// Telegram message length limit.
        /// </summary>
        private static readonly int MaxTextLength = 4096;


        public TelegramMessageBuilder(string baseUrl, string text)
        {
            _baseUrl = baseUrl;
            _client = new TelegramClient();
            _request = new MessageRequest { Text = text };
        }

        public static TelegramMessageBuilder Build(string baseUrl, string text)
        {
            return new TelegramMessageBuilder(baseUrl, text);
        }

        public TelegramMessageBuilder OnError(Action<Exception> error)
        {
            _client.Error += error;

            return this;
        }

        public TelegramMessageBuilder ToChat(string chatId)
        {
            _request.ChatId = chatId;
            return this;
        }

        public void Send()
        {
            var dic = new Dictionary<string, string>
            {
                {"chat_id", _request.ChatId},
                {"text", _request.Text?.Substring(0, Math.Min(MaxTextLength, _request.Text.Length))}
            };

            var array = dic
                .Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}")
                .ToArray();

            var url = $"{_baseUrl}?{String.Join("&", array)}";

            _client.Send(url);
        }
    }
}