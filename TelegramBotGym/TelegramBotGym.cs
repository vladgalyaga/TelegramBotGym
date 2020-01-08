using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TelegramBotGym
{
    public class TelegramBotGym : IDisposable
    {
        private TelegramBotClient client;
        private string token = "943512519:AAHbaut38lCV9XkVABM_R5ZzEEwVJmLHitw";
        public TelegramBotGym()
        {
            // token, который вернул BotFather
            client = new TelegramBotClient(token);
            client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.StartReceiving();
        }

        public void Dispose()
        {
            client.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;


            var handlers =
                this.GetType().Assembly.GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.GetInterface(nameof(IMessageHandler)) != null)
                .Select(x => (IMessageHandler)Activator.CreateInstance(x)).ToList();

            var validhandlers = handlers.Where(x => x.MessageSelector(message)).ToList();


            foreach (var handler in validhandlers)
            {
                await handler.Handle(message, client);
            }


            //var message = messageEventArgs.Message;

            //if (message?.Type == MessageType.Text)
            //{
            //    await client.SendTextMessageAsync(message.Chat.Id, message.Text);
            //}
        }
    }
}
