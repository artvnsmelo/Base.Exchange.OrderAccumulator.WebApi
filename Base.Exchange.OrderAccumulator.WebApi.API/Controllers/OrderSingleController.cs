using System.ComponentModel.DataAnnotations;

using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Services;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Extensions;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Success;

namespace Base.Exchange.OrderAccumulator.WebApi.API.Controllers;

[ApiController]
[Route("OrderSingle")]
[ApiExplorerSettings(GroupName = "NewOrderSingle")]
[Consumes("application/json", "application/json")]
[Produces("application/json", "application/json")]
public class OrderSingleController : ControllerBase
{
    private readonly IOrderSingleService _orderSingleService;

    public OrderSingleController(IOrderSingleService orderSingleService)
    {
        _orderSingleService = orderSingleService;
    }

    [HttpPost("execute")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorListResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorListResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorListResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SuccessResponse<bool>>> ExecuteNewOrderSingleAsync
    (
        [FromBody] ExecutedNewOrderSingleRequest request,
        [FromQuery, Required] OrderSideEnum side
    )
    {
        var result = await _orderSingleService.ExecuteNewOrderSingleAsync(request, side);

        return Ok("Execution Report send with success!");
    }
}
