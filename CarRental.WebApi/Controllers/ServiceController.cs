using CarRental.Application.IServices;
using CarRental.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceService personelService, ILogger<ServiceController> logger)
        {
            _serviceService = personelService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceDto>> Get()
        {
            var result = _serviceService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich pracowników");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ServiceDto> Get(int id)
        {
            var result = _serviceService.GetById(id);
            _logger.LogDebug($"Pobrano pracownika o id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] ServiceDto dto)
        {
            var id = _serviceService.Create(dto);
            _logger.LogDebug($"Utworzono nowego pracownika z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _serviceService.Delete(id);
            _logger.LogDebug($"Usunięto pracownika z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] ServiceDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Id param is not valid");
            }

            _serviceService.Update(dto);
            _logger.LogDebug($"Zaktualizowano pracownika z id = {id}");
            return NoContent();
        }
    }
}
