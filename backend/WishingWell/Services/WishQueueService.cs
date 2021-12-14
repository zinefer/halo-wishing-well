using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;

using WishingWell.Models;
using WishingWell.Services;

namespace WishingWell.Services
{
    public class WishQueueService : IWishQueueService
    {
        private readonly ILogger<WishQueueService> logger;
        private readonly QueueClient queueClient;

        public WishQueueService(ILogger<WishQueueService> logger, QueueClient queueClient)
        {
            this.logger = logger;
            this.queueClient = queueClient;
        }

        public async Task Add(Coin coin)
        {
            try
            {
                // Create the coin queue if it does not yet exist
                await this.queueClient.CreateIfNotExistsAsync();

                // Convert our coin to binary and put it in the queue
                var data = new BinaryData(coin.ToBinary());
                await this.queueClient.SendMessageAsync(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
        }
    }
}














