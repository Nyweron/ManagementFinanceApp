using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.TransferHistory;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TransferHistoryController : ControllerBase
  {
    private ITransferHistoryRepository _transferHistoryRepository;
    private IMapper _mapper;
    public TransferHistoryController(ITransferHistoryRepository transferHistoryRepository,
      IMapper mapper
    )
    {
      _transferHistoryRepository = transferHistoryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var transferHistoryEntities = await _transferHistoryRepository.GetAllAsync();
      return Ok(transferHistoryEntities);
    }

    [HttpGet("{transferHistoryId}")]
    public async Task<IActionResult> Get(int transferHistoryId)
    {
      try
      {
        var transferHistoryEntities = await _transferHistoryRepository.GetAsync(transferHistoryId);
        return Ok(transferHistoryEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {transferHistoryId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.TransferHistory transferHistory)
    {
      if (transferHistory == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto transferHistory).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var transferHistoryEntity = _mapper.Map<Entities.TransferHistory>(transferHistory);
      await _transferHistoryRepository.AddAsync(transferHistoryEntity);

      if (!await _transferHistoryRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var transferHistory = await _transferHistoryRepository.GetAsync(id);

      if (!await _transferHistoryRepository.RemoveAsync(transferHistory))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}