using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly IRepositoryWrapper _repository;

        public OwnerController(ILogger<OwnerController> logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var allOwners = _repository.Owner.GetAll();
                _logger.LogInformation("Returned all owners from database.");
                return Ok(allOwners);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
