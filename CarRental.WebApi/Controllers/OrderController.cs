using CarRental.Application.IServices;
using CarRental.Application.Services;
using CarRental.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this._orderService = orderService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> Get()
        {
            var result = _orderService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich produktów");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderDto> Get(int id)
        {
            var result = _orderService.GetById(id);
            _logger.LogDebug($"Pobrano produkt o id = {id}");
            return Ok(result);
        }

        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] OrderDto dto)
        {
            var id = _orderService.Create(dto);

            _logger.LogDebug($"Utworzono nowy produkt z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);
            _logger.LogDebug($"Usunieto produkt z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] OrderDto dto)
        {
            if (id != dto.Id)
            {
                //throw new BadRequestException("Id param is not valid");
            }

            _orderService.Update(dto);
            _logger.LogDebug($"Zaktualizowano produkt z id = {id}");
            return NoContent();
        }

    }
}
