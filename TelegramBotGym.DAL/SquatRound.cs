using Haliaha.DAL.Core.Interfaces;
using System;

namespace TelegramBotGym.DAL
{
    public class SquatRound : IKeyable<int>
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }

        public int SquatCount { get; set; }
    }
}
