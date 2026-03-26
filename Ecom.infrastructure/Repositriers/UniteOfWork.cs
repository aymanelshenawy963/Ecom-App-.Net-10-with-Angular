using AutoMapper;
using Ecom.Core.Entites;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;


namespace Ecom.infrastructure.Repositriers;

public class UniteOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageMangementService _imageMangementService;
    private readonly IConnectionMultiplexer _redis;
    private readonly UserManager<AppUser> _userManager; 
    private readonly IEmailService _emailService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IGenerateToken _token;
    private readonly IAuth _auth;


    public ICategoryRepository CategoryRepository{ get; }
    public IProductRepository ProductRepository{ get; }
    public IPhotoRepository PhotoRepository{ get; }
    public ICustomerBasketRepository CustomerBasket { get; }
    public IAuth Auth { get; }

    public UniteOfWork(AppDbContext context, IMapper mapper, IImageMangementService imageMangementService, IConnectionMultiplexer redis, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IGenerateToken token, IAuth auth)
    {
        _context = context;
        _imageMangementService = imageMangementService;
        _mapper = mapper;
        _redis = redis;
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _token = token;
        _auth = auth;

        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context, _mapper, _imageMangementService);
        PhotoRepository = new PhotoRepository(_context);
        CustomerBasket = new CustomerBasketRepository(_redis);
        Auth = new AuthRepository(_userManager, _emailService, _signInManager, _token,_context,_mapper);
    }
}
