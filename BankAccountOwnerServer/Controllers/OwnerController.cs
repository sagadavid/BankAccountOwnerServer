using Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public OwnerController(ILogger<OwnerController> logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var allOwners = _repository.Owner.GetAll();
                _logger.LogInformation("Returned all owners from database.");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDTO>>(allOwners);//so we filter out the account details from the owner details
                //return Ok(allOwners);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
