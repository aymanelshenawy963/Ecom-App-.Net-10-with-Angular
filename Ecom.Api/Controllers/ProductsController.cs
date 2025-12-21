using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers;


public class ProductsController : BaseController
{
    public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
    {
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await work.ProductRepository
                .GetAllAsync(x=>x.Category, x=>x.Photos );

            var result = mapper.Map<List<ProductDTO>>(products);

            if (products is null || !products.Any())
                return BadRequest(new ResponseAPI(400));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id) 
    {
        try
        {
            var product = await work.ProductRepository
                .GetByIdAsync(id,x => x.Category, x => x.Photos);

            var result = mapper.Map<ProductDTO>(product);

            if (product is null )
                return BadRequest(new ResponseAPI(400));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromForm] AddProductDTO productDTO)
    {
        try
        {
            await work.ProductRepository.AddAsync(productDTO);
            return Ok(new ResponseAPI(200));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromForm] UpdateProductDTO productDTO)
    {
        try
        {
            await work.ProductRepository.UpdateAsync(productDTO);
            return Ok(new ResponseAPI(200));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var product = await work.ProductRepository.GetByIdAsync(id);
            if (product is null)
                return BadRequest(new ResponseAPI(400, "Product not found"));

            await work.ProductRepository.DeleteAsync(product);
            return Ok(new ResponseAPI(200));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAPI(400, ex.Message));
        }
    }

}
