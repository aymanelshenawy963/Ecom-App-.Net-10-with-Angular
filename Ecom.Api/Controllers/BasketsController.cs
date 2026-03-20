using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Entites;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers;

public class BasketsController : BaseController
{
    public BasketsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
    {
    }

    [HttpGet("get-basket/{id}")]
    public async Task<IActionResult> GetBasket(string id)
    {
        var basket = await work.CustomerBasket.GetBasketAsync(id);
        if (basket == null) return Ok(new CustomerBasket());
        return Ok(basket);
    }

    [HttpPost("update-basket")]
    public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasket basket)
    {
        var updatedBasket = await work.CustomerBasket.UpdateBasketAsync(basket);
        if (updatedBasket == null) return BadRequest();
        return Ok(updatedBasket);
    }
     [HttpDelete("delete-basket/{id}")]
     public async Task<IActionResult> DeleteBasket(string id)
     {
       var deleted = await work.CustomerBasket.DeleteBasketAsync(id);
        return deleted ? Ok(new ResponseAPI(200,"item deleted")) : BadRequest(new ResponseAPI(400));
    }
}