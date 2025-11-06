using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Subcategory
{
    public int SubcatId { get; set; }

    public string? SubcatName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();

    public virtual ICollection<Category> IdCategories { get; set; } = new List<Category>();
}
