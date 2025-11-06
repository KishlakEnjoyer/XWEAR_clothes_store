using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string? ModelName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();
}
