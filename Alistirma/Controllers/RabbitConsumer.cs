using Alistirma.Data;
using Alistirma.Infrastructure.Repository;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Alistirma.Controllers
{

    public class RabbitConsumerController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public RabbitConsumerController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            User user = new User();
            user.Name = "Teasdasdsadst";
            user.Email = "asdasdsada";
            user.Password = "asdasdsadasdadsda";
            _userRepository.Add(user);
            return Ok();
        }
    }
}
