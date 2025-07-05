using System.Diagnostics;
using E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;
using E_commerceSystem.DataAccessLayer.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;
using E_commerceSystem.Models;
using E_commerceSystem.Models.Products;

namespace E_commerceSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepository;
    public readonly IProductRepository _productRepository;
    public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<ActionResult> Index()
    {
        var user = await _userRepository.GetUserByEmailAsync("maheratef600@gmail.com");
        var products = await _productRepository.GetAllProductsAsync();
        
        var productVM = new HomeModelVM();
        productVM.User = user;
        productVM.Products = products;
        return View(productVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}