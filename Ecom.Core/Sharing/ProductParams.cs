using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Sharing;

public class ProductParams
{
    public string? Sort { get; set; }
    public int? CategoryId { get; set; }
     
    public string? Search {  get; set; }
    private int MaxPageSize { get; set; } = 6;
    private int _pageSize = 3;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int PageNumber { get; set; } = 1;
}
