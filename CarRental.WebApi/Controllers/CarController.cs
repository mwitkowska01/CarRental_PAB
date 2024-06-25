using Microsoft.AspNetCore.Mvc;
using CarRental.SharedKernel.Dto;
using CarRental.Application.IServices;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService productService, ILogger<CarController> logger)
        {
            this._carService = productService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> Get()
        {
            var result = _carService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich produktów");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDto> Get(int id)
        {
            var result = _carService.GetById(id);
            _logger.LogDebug($"Pobrano produkt o id = {id}");
            return Ok(result);
        }

        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CarDto dto)
        {
            var id = _carService.Create(dto);

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
            _carService.Delete(id);
            _logger.LogDebug($"Usunieto produkt z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] CarDto dto)
        {
            if (id != dto.Id)
            {
                //throw new BadRequestException("Id param is not valid");
            }

            _carService.Update(dto);
            _logger.LogDebug($"Zaktualizowano produkt z id = {id}");
            return NoContent();
        }
    }
}
