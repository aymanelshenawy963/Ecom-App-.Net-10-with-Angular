using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecom.Core.interfaces;
using AutoMapper;

namespace Ecom.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IUnitOfWork work;
    protected readonly IMapper mapper;

    protected BaseController(IUnitOfWork work,IMapper mapper)
    {
        this.work = work;
        this.mapper = mapper;
    }
}
