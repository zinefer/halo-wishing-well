using System.Threading;
using System.Threading.Tasks;

using WishingWell.Models;

namespace WishingWell.Services
{
    public interface IWishQueueService
    {
        Task Add(Coin coin);
    }
}