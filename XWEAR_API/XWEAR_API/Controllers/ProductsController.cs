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
    public async Task<IActionResult> GetGoodByArticle(string article)
    {
        var goodDto = await _context.Goods
            .Include(g => g.Brand)
            .Include(g => g.Category)
            .Include(g => g.Model)
            .Include(g => g.Image)
            .Include(g => g.GoodSizes)
            .Where(g => g.GoodArticle == article) 
            .Select(g => new
            {
                Article = g.GoodArticle,
                BrandName = g.Brand.BrandName,
                ModelName = g.Model.ModelName,
                CategoryName = g.Category.CategoryName,
                ImagePath = g.Image.Patrh, 
                Sizes = g.GoodSizes.Select(gs => new 
                {
                    Size = gs.Size,
                    Price = (float)gs.Price
                }).ToList()
            })
            .FirstOrDefaultAsync(); 

        if (goodDto == null)
        {
            return NotFound(new { message = "Товар не найден." });
        }

        return Ok(goodDto); 
    }

    [HttpGet("GetAllGoods")]
    public async Task<IActionResult> GetAllGoods()
    {
        var goodsDtos = await _context.Goods
            .Include(g => g.Brand)
            .Include(g => g.Category)
            .Include(g => g.Model)
            .Include(g => g.Image)
            .Include(g => g.GoodSizes)
            .Select(g => new 
            {
                Article = g.GoodArticle,

                Brand = g.Brand != null ? new
                {
                    Id = g.Brand.BrandId,
                    Name = g.Brand.BrandName
                } : null,
                Category = g.Category != null ? new
                {
                    Id = g.Category.CategoryId,
                    Name = g.Category.CategoryName
                } : null,
                Model = g.Model != null ? new
                {
                    Id = g.Model.ModelId,
                    Name = g.Model.ModelName
                } : null,
                Image = g.Image != null ? new
                {
                    Article = g.Image.GoodArticle,
                    Path = g.Image.Patrh, 
                    Main = g.Image.Main
                } : null,
                GoodSizes = g.GoodSizes.Select(gs => new
                {
                    Article = gs.GoodArticle,
                    Size = gs.Size,
                    Price = (float)gs.Price
                }).ToList()
            })
            .ToListAsync();

        return Ok(goodsDtos);
    }

    [HttpGet("GetGoodById")]
    public async Task<IActionResult> getGoodById(string goodId)
    {
        var goodFullInfo = await _context.Goods
            .Include(g => g.Brand)
            .Include(g => g.Category)
            .Include(g => g.Model)
            .Include(g => g.Image)
            .Include(g => g.GoodSizes)
            .Include(g => g.Subcat)
            .Select(g => new
            {
                Article = g.GoodArticle,

                Brand = g.Brand != null ? new
                {
                    Id = g.Brand.BrandId,
                    Name = g.Brand.BrandName
                } : null,
                Category = g.Category != null ? new
                {
                    Id = g.Category.CategoryId,
                    Name = g.Category.CategoryName
                } : null,
                Model = g.Model != null ? new
                {
                    Id = g.Model.ModelId,
                    Name = g.Model.ModelName
                } : null,
                Image = g.Image != null ? new
                {
                    Article = g.Image.GoodArticle,
                    Path = g.Image.Patrh,
                    Main = g.Image.Main
                } : null,
                GoodSizes = g.GoodSizes.Select(gs => new
                {
                    Article = gs.GoodArticle,
                    Size = gs.Size,
                    Price = (float)gs.Price
                }).ToList(),
                Subcategory = g.Subcat != null ? new
                {
                    Id = g.Subcat.SubcatId,
                    Name = g.Subcat.SubcatName
                } : null
            })
            .FirstOrDefaultAsync(g => g.Article == goodId);
        return Ok(goodFullInfo);
    }
}