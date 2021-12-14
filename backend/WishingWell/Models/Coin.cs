using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Azure;
using Azure.Data.Tables;

namespace WishingWell.Models
{
    public class Coin : ITableEntity
    {
        public Coin()
        {
            this.PartitionKey = DateTime.Now.ToString("d-mm-yyyy");
            this.RowKey = Guid.NewGuid().ToString();
        }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public byte[] ToBinary()
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] output = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Position = 0;
                bf.Serialize(ms, this);
                output = ms.GetBuffer();
            }
            return output;
        }
    }
}