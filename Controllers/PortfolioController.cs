using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stocks.Extensions;
using stocks.Interfaces;
using stocks.Models;

namespace stocks.Controllers;

[Route("api/portfolio")]
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
        var userPortfolio = await _portfolioRepository.GetPortfolioAsync(appUser.Id);
        
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepository.GetBySymbolAsync(symbol);

        if (stock == null)
        {
            return BadRequest("Stock not found");
        }

        var userPortfolio = await _portfolioRepository.GetPortfolioAsync(appUser.Id);

        if (userPortfolio.Any(e => symbol.ToLower() == symbol.ToLower()))
        {
            return BadRequest("Stock already exists");  
        }

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id,
        };
        
        var portfolio =_portfolioRepository.CreatePortfolioAsync(portfolioModel);

        if (portfolio == null)
        {
            return StatusCode(500, "Portfolio could not be created");
        }
        
        return Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetPortfolioAsync(appUser.Id);
        var stock =  userPortfolio.FirstOrDefault(x => x.Symbol.ToLower() == symbol.ToLower());
        
        var deletedStock = await _portfolioRepository.DeletePortfolioAsync(stock.Id);

        if (deletedStock == null)
        {
            return StatusCode(500, "Portfolio could not be deleted");
        }
        
        return NoContent();
    }
}