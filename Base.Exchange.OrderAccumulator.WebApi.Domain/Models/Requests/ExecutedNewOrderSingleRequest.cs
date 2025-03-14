using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;

public class ExecutedNewOrderSingleRequest
{
    /// <summary>
    /// Symbol
    /// </summary> 
    [Required]
    [BindRequired]
    [MinLength(5)]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of execution
    /// </summary>    
    [Required]
    [BindRequired]
    [Range(1, 100000)]
    public int Quantity { get; set; }

    /// <summary>
    /// Price of asset
    /// </summary>    

    [Range(0.01, 999.99, ErrorMessage = "The price must be between 0.01 and 999.99")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The amount must be a multiple of 0.01.")]
    [Required]
    [BindRequired]
    public decimal Price { get; set; }
}
