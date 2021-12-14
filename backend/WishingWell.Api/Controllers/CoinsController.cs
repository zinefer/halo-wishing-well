using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WishingWell.Api.Responses;
using WishingWell.Models;
using WishingWell.Services;

namespace WishingWell.Api.Controllers
{
    [Route("api/coins")]
    [ApiController]
    [Authorize]
    public class CoinsController : Controller
    {
        private readonly ILogger<CoinsController> logger;
        private readonly IWishCountService iWishCount;
        private readonly IWishQueueService iWishQueue;

        public CoinsController(
            IWishCountService iWishCount,
            IWishQueueService iWishQueue,
            ILogger<CoinsController> logger)
        {
            this.iWishCount = iWishCount;
            this.iWishQueue = iWishQueue;
            this.logger = logger;
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var response = new CoinCountResponse
            {
                Count = await this.iWishCount.Count(),
            };
            return this.Json(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            var coin = new Coin();
            await this.iWishQueue.Add(coin);
            return new OkObjectResult("success");
        }
    }
}