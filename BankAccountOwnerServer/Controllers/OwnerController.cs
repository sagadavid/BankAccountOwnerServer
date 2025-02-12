using AutoMapper;
using Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOwnerServer.Controllers
{
    [Route("api/owners")]
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

                var ownersResult = _mapper.Map<IEnumerable<OwnerDTO>>(allOwners);//so we filter out the account details from the allOwners details
                //return Ok(allOwners);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{ownerGuid}")]
        public IActionResult GetOwnerById(Guid ownerGuid)
        {
            try
            {
                var allOwners = _repository.Owner.GetById(ownerGuid);
                if (allOwners == null)
                {
                    _logger.LogError($"Owner with id: {ownerGuid}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned allOwners with id: {ownerGuid}");
                    var ownerResult = _mapper.Map<OwnerDTO>(allOwners);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message};");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{ownerGuid}/account")]
        public IActionResult GetOwnerWithAccounts(Guid ownerGuid)
        {
            try
            {
                var allOwners = _repository.Owner.GetOwnerWithAccounts(ownerGuid);
                if (allOwners == null)
                {
                    _logger.LogError($"Owner with id: {ownerGuid}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned allOwners with accounts for id: {ownerGuid}");
                    var ownerResult = _mapper.Map<OwnerDTO>(allOwners);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithAccounts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}

