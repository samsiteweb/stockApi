using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testApi.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [Range(1, 100, ErrorMessage = "Purchase must be within the range of 1 to 100")]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.01, 100, ErrorMessage = "LastDiv must be within the range of 0.01 to 100")]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Industry cannot be over 20 characters")]
        public string Industry {get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000, ErrorMessage = "MarketCap must be within the range of 1 to 5000000000000")]
        public long MarketCap { get; set; }
    }
}