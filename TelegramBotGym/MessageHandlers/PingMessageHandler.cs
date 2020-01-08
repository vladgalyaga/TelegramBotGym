using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotGym.MessageHandlers
{

    public class PingMessageHandler : IMessageHandler
    {

        public async Task Handle(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, "pong");
        }

        public bool MessageSelector(Message message)
        {
            return message?.Text?.ToLower() == "ping";
        }
    }
}

