using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
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
        [HttpGet("{ownerGuid}", Name ="Owni")]
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

        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerForCreationDTO owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = _mapper.Map<Owner>(owner);
                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Save();
                var createdOwner = _mapper.Map<OwnerDTO>(ownerEntity);

                //result header localtion will be formed according to "Owni" Get-request above
                return CreatedAtRoute("Owni", new { ownerGuid = createdOwner.OwnerId }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{ownerGuid}")]
        public IActionResult UpdateOwner(Guid ownerGuid, [FromBody] OwnerForUpdateDTO owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = _repository.Owner.GetById(ownerGuid);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {ownerGuid}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);

                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Save();

                //return NoContent();

                //Alternative 1

                //string locationUrl = Url.Action("GetOwnerById", new { ownerGuid = ownerGuid });

                //var responseForUpdate = new ObjectResult(new
                //{
                //    message = "Owner updated successfully",
                //    updatedOwner = ownerEntity,
                //    localtionUrl = locationUrl
                //})
                //{ StatusCode = 204 };

                //Response.Headers["Location"] = locationUrl;
                //return responseForUpdate;

                //Alternative 2

                string locationUrl = Url.Action("GetOwnerById", new { ownerGuid = ownerGuid });
                return Accepted(locationUrl, new
                {
                    message = "update accepted !",
                    UpdateOwner = ownerEntity,
                    locationUrl
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}

