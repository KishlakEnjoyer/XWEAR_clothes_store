using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class TransactionsDetail
{
    public int TransactionId { get; set; }

    public string GoodArticle { get; set; } = null!;

    public int? CountGood { get; set; }

    public float? GoodAmount { get; set; }

    public virtual Good GoodArticleNavigation { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}
