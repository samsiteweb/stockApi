using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApi.Dtos.Stock
{
    public class UpdateStockDto
    {
        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry {get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }
}