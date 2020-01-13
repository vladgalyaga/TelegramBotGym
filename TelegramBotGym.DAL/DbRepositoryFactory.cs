using Haliaha.DAL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotGym.DAL
{
	public class DbRepositoryFactory : BaseUnitOfWork
	{
		public DbRepositoryFactory(DataContext dbContext)
			: base(dbContext)
		{
		}

	}
}
