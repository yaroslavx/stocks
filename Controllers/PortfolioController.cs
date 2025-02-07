using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stocks.Extensions;
using stocks.Interfaces;
using stocks.Models;

namespace stocks.Controllers;

[Route("api/controller")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser.Id);
        
        return Ok(userPortfolio);
    }
}