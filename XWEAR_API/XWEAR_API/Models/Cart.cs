using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Cart
{
    public string UserEmail { get; set; } = null!;

    public string GoodArticle { get; set; } = null!;

    public int? GoodCount { get; set; }

    public virtual Good GoodArticleNavigation { get; set; } = null!;

    public virtual User UserEmailNavigation { get; set; } = null!;
}
