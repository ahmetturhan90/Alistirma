using Alistirma.Caching;
using Alistirma.Data;
using Alistirma.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Alistirma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly ICacheManager _redisCacheService;
        private readonly IUserRepository _userRepository;

        public RedisController(ICacheManager redisCacheService, IUserRepository userRepository)
        {
            _redisCacheService = redisCacheService;
            _userRepository = userRepository;
        }
        [HttpPost("cache/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            User user = new User();
            user.Name = "Teasdasdsadstasdasdas";
            user.Email = "asdasdsada";
            user.Password = "asdasdsadasdadsda";
            _userRepository.Add(user);
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
