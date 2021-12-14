using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using WishingWell.Models;
using WishingWell.Repositories;

namespace WishingWell.Services
{
    public class WishCountService : IWishCountService
    {
        private readonly ICoinsTableRepository coins;
        private readonly ILogger<WishCountService> logger;

        public WishCountService(ILogger<WishCountService> logger, ICoinsTableRepository coins)
        {
            this.logger = logger;
            this.coins = coins;
        }

        public Task<int> Count()
        {
            // TODO: Probably not this
            return Task.Run(() => Enumerable.Count<Coin>(this.coins.GetAll()));
        }
    }
}