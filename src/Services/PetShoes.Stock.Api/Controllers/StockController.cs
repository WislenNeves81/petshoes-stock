using Microsoft.AspNetCore.Mvc;
using PetShoes.Stock.Api.Core.Application.AppStock.Input;
using PetShoes.Stock.Api.Core.Application.AppStock.Interface;

namespace PetShoes.Stock.Api.Controllers
{
    [Route("petshoes/api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockAppService _stockAppService;

        public StockController(IStockAppService stockAppService)
        {
            _stockAppService = stockAppService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAsync(Guid itemStockId)
        {
            var itemStock = await _stockAppService
                                            .GetStockByIdAsync(itemStockId)
                                            .ConfigureAwait(false);
            return Ok(itemStock);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync([FromBody] StockInput stockInput)
        {
            var itemStock = await _stockAppService
                                            .InsertAsync(stockInput)
                                            .ConfigureAwait(false);

            return Ok(itemStock);
        }
        [HttpPut]
        [Route("{itemStockId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutAsync([FromRoute]Guid itemStockId, [FromBody] StockInput stockInput)
        {
            var stockItem = await _stockAppService
                                    .UpdateAsync(itemStockId, stockInput)
                                    .ConfigureAwait(false);

            return Ok(stockItem);
        }
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(Guid itemStockId)
        {
            await _stockAppService
                        .DeleteAsync(itemStockId)
                        .ConfigureAwait(false);

            return Ok();
        }
    }
}
