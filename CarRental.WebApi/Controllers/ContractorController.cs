using CarRental.Application.IServices;
using CarRental.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    public class ContractorController : Controller
    {
        private readonly IContractorService _contractorService;
        private readonly ILogger<ContractorController> _logger;

        public ContractorController(IContractorService productService, ILogger<ContractorController> logger)
        {
            this._contractorService = productService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContractorDto>> Get()
        {
            var result = _contractorService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich produktów");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetContractor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContractorDto> Get(int id)
        {
            var result = _contractorService.GetById(id);
            _logger.LogDebug($"Pobrano produkt o id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] ContractorDto dto)
        {
            var id = _contractorService.Create(dto);

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
            _contractorService.Delete(id);
            _logger.LogDebug($"Usunieto produkt z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] ContractorDto dto)
        {
            if (id != dto.Id)
            {
                //throw new BadRequestException("Id param is not valid");
            }

            _contractorService.Update(dto);
            _logger.LogDebug($"Zaktualizowano produkt z id = {id}");
            return NoContent();
        }
    }
}
