using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Saving;
using ManagementFinanceApp.Service.Saving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SavingController : ControllerBase
  {
    private ISavingService _savingService;
    private ISavingRepository _savingRepository;
    private IMapper _mapper;
    private ILogger _logger;
    public SavingController(ISavingRepository savingRepository,
                            IMapper mapper,
                            ISavingService savingService, 
                            ILogger logger)
    {
      _savingRepository = savingRepository;
      _mapper = mapper;
      _savingService = savingService;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var savingEntities = await _savingService.GetAllAdaptAsync();
      return Ok(savingEntities);
    }

    [HttpGet("{savingId}")]
    public async Task<IActionResult> Get(int savingId)
    {
      try
      {
        var savingEntities = await _savingRepository.GetAsync(savingId);
        return Ok(savingEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {savingId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Saving saving)
    {
      if (saving == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto saving).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _savingService.AddSaving(saving);

      if (isCreated)
      {
        //TODO: Implement Realistic Implementation
        return Created("", null);
      }
      else
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      var saving = await _savingRepository.GetAsync(id);

      if (!await _savingRepository.RemoveAsync(saving))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Models.Saving savingRequest)
    {
      try
      {
        if (savingRequest == null)
        {
          return BadRequest("Object cannot be null");
        }

        // Update entity in repository
        var isUpdated = await _savingService.EditSaving(savingRequest, id);
        if (isUpdated)
        {
          return NoContent();
        }
        else
        {
          _logger.LogError($"Edit saving a problem happend. Error in updateSaving. When accessing to SavingController/Edit");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        // Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutStockItemAsync), ex);
      }

      return NoContent();
    }


  }
}