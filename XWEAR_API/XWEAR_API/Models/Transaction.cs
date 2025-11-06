using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    public string? CompanyName { get; set; }

    public string? UserCountry { get; set; }

    public string? UserStreet { get; set; }

    public string? UserNumHouse { get; set; }

    public string? UserCity { get; set; }

    public string? UserState { get; set; }

    public string? UserIndex { get; set; }

    public float? TransactionTotalAmount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? TransactionsStatus { get; set; }

    public virtual ICollection<TransactionsDetail> TransactionsDetails { get; set; } = new List<TransactionsDetail>();

    public virtual User UserEmailNavigation { get; set; } = null!;
}
