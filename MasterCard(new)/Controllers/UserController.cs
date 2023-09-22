using MasterCard.Services;
using MasterCard_new_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterCard_new_.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService;
        public UserController(IUserService userService) {
            this.userService = userService;
            }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var result = await userService.LogIn(model.Username, model.Password);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
