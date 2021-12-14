using System;
using Xunit;

using WishingWell.Options;

namespace WishingWell.UnitTests.Options
{
    public class StorageOptionsTests
    {
        [Fact]
        public void TableEndpoint_Test()
        {
            var storageOptions = new StorageOptions
            {
                TableAccountName = "stunittest",
                CoinsTableName = "coinstesttable",
                EndpointSuffix = "unit.test.local",
                DefaultEndpointsProtocol = "unittest",
            };

            var actual = storageOptions.TableEndpoint;

            Assert.Equal(actual.Scheme, storageOptions.DefaultEndpointsProtocol);
            Assert.Contains(storageOptions.TableAccountName, actual.Host);
            Assert.Contains("table", actual.Host);
            Assert.EndsWith(storageOptions.EndpointSuffix, actual.Host);
        }

        [Fact]
        public void QueueEndpoint_Test()
        {
            var storageOptions = new StorageOptions
            {
                QueueAccountName = "stunittest",
                CoinsQueueName = "coinstestqueue",
                DefaultEndpointsProtocol = "unittest",
            };

            var actual = storageOptions.QueueEndpoint;

            Assert.Equal(actual.Scheme, storageOptions.DefaultEndpointsProtocol);
            Assert.Contains(storageOptions.QueueAccountName, actual.Host);
            Assert.Contains(".queue.core.windows.net", actual.Host);
            Assert.EndsWith(storageOptions.CoinsQueueName, actual.ToString());
        }
    }
}