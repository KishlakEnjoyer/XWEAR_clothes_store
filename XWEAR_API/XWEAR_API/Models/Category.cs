using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();

    public virtual ICollection<Subcategory> IdSubcategories { get; set; } = new List<Subcategory>();
}
