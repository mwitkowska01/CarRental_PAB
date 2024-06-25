using CarRental.Application.IServices;
using CarRental.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelService _personelService;
        private readonly ILogger<PersonelController> _logger;

        public PersonelController(IPersonelService personelService, ILogger<PersonelController> logger)
        {
            _personelService = personelService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonelDto>> Get()
        {
            var result = _personelService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich pracowników");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPersonel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonelDto> Get(int id)
        {
            var result = _personelService.GetById(id);
            _logger.LogDebug($"Pobrano pracownika o id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] PersonelDto dto)
        {
            var id = _personelService.Create(dto);
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
            _personelService.Delete(id);
            _logger.LogDebug($"Usunięto pracownika z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] PersonelDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Id param is not valid");
            }

            _personelService.Update(dto);
            _logger.LogDebug($"Zaktualizowano pracownika z id = {id}");
            return NoContent();
        }
    }
}