using System;
using System.ComponentModel.DataAnnotations;

namespace WishingWell.Options
{
    public class StorageOptions
    {
        [Required]
        public string QueueAccountName { get; set; }

        [Required]
        public string CoinsQueueName { get; set; }

        [Required]
        public string TableAccountName { get; set; }

        [Required]
        public string CoinsTableName { get; set; }

        [Required]
        public string DefaultEndpointsProtocol { get; set; } = "https";

        [Required]
        public string EndpointSuffix { get; set; } = "core.windows.net";

        public Uri TableEndpoint
            // TODO: String builder?
            => new Uri($"{this.DefaultEndpointsProtocol}://{this.TableAccountName}.table.{this.EndpointSuffix}");

        public Uri QueueEndpoint
            // TODO: String builder?
            => new Uri($"{this.DefaultEndpointsProtocol}://{this.QueueAccountName}.queue.core.windows.net/{this.CoinsQueueName}");
    }
}