using System;
using System.Collections.Generic;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;

using WishingWell.Models;

namespace WishingWell.Repositories
{
    public class CoinsTableRepository : ICoinsTableRepository
    {
        private readonly ILogger<CoinsTableRepository> logger;

        private readonly TableClient tableClient;

        public CoinsTableRepository(ILogger<CoinsTableRepository> logger, TableClient tableClient)
        {
            this.logger = logger;
            this.tableClient = tableClient;
        }

        public IEnumerable<Coin> GetAll()
        {
            try
            {
                return this.tableClient.Query<Coin>();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return null;
            }
        }

        public Coin GetById(string id)
        {
            try
            {
                return this.tableClient.GetEntity<Coin>(id, id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return null;
            }
        }

        public void Insert(Coin coin)
        {
            try
            {
                this.tableClient.AddEntity(coin);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
        }

        public void Update(Coin coin)
        {
            try
            {
                this.tableClient.UpdateEntityAsync(coin, coin.ETag);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
        }

        public void Upsert(Coin coin)
        {
            try
            {
                this.tableClient.UpsertEntity(coin);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
        }

        public void Delete(Coin coin)
        {
            try
            {
                this.tableClient.DeleteEntity(coin.PartitionKey, coin.RowKey);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
        }
    }
}