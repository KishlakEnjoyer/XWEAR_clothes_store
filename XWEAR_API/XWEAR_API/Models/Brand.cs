using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();
}
