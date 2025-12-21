using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using AutoMapper;
using Ecom.Api.Helper;

namespace Ecom.Api.Controllers;

public class CategoriesController : BaseController
{
    public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
    {
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await work.CategoryRepository.GetAllAsync();

            if (categories is null || !categories.Any())
                return BadRequest(new ResponseAPI(400));
            return Ok(categories);
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
            var category = await work.CategoryRepository.GetByIdAsync(id);

            if (category is null)
                return BadRequest(new ResponseAPI(400,$"not found category id={id}"));
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = mapper.Map<Category>(categoryDTO);
            await work.CategoryRepository.AddAsync(category);
            if (category is null)
                return BadRequest();
            return Ok(new ResponseAPI(200, "Item has been added"));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO categoryDTO)
    {
        try
        {
            var category = mapper.Map<Category>(categoryDTO);
            await work.CategoryRepository.UpdateAsync(category);
            if (category is null)
                return BadRequest();
            return Ok(new ResponseAPI(200, "Item has been updated " ));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id) 
    {
        try
        {
            await work.CategoryRepository.DeleteAsync(id);
            return Ok(new ResponseAPI(200, message: "Item has been deleted "));
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }

    }
}
