using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class GoodSize
{
    public string GoodArticle { get; set; } = null!;

    public float Size { get; set; }

    public float? Price { get; set; }

    public virtual Good GoodArticleNavigation { get; set; } = null!;
}
