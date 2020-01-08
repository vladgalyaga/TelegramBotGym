using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotGym
{
    public interface IMessageHandler
    {
        public bool MessageSelector(Message message);
        public Task Handle(Message message, TelegramBotClient client);
    }
}
