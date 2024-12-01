using Alistirma.Caching;
using Microsoft.AspNetCore.Mvc;

namespace Alistirma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly ICacheManager _redisCacheService;

        public RedisController(ICacheManager redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }
        [HttpPost("cache/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            throw new Exception();
            return Ok(await _redisCacheService.GetValueAsync(key));
        }

        [HttpPost("cache/set")]
        public async Task<IActionResult> Set([FromBody] RedisCacheRequestModel redisCacheRequestModel)
        {
            await _redisCacheService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
            return Ok();
        }


    }
}
