using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XWEAR_API.Models;

namespace XWEAR_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoodController : ControllerBase
{
    private readonly XwearDbContext _context;

    public GoodController(XwearDbContext context)
    {
        _context = context;
    }

    // GET: api/Good/ABC123
    [HttpGet("{article}")]
    public async Task<ActionResult<GoodDetailsDto>> GetGoodByArticle(string article)
    {
        var good = await _context.Goods
            .Include(g => g.Brand)
            .Include(g => g.Category)
            .Include(g => g.Model)
            .Include(g => g.Image) 
            .Include(g => g.GoodSizes)
            .FirstOrDefaultAsync(g => g.GoodArticle == article);

        if (good == null)
        {
            return NotFound(new { message = "Товар не найден." });
        }

        var dto = new GoodDetailsDto
        {
            Article = good.GoodArticle,
            BrandName = good.Brand.BrandName,
            ModelName = good.Model.ModelName,
            CategoryName = good.Category.CategoryName,
            ImagePath = good.Image?.Patrh, 
            Sizes = good.GoodSizes.Select(gs => new GoodSizeDto
            {
                Size = gs.Size, 
                Price = gs.Price 
            }).ToList()
        };

        return Ok(dto);
    }
}


public class GoodDetailsDto
{
    public string Article { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
    public string? CategoryName { get; set; }
    public string? ImagePath { get; set; } 
    public List<GoodSizeDto> Sizes { get; set; } = new List<GoodSizeDto>();
}

public class GoodSizeDto
{
    public float Size { get; set; }
    public float? Price { get; set; }
}