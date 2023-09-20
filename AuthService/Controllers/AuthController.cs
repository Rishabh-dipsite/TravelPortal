using AuthService.Model;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {

		private readonly IJWTManagerRepository _jWTManager;
		private readonly ILogger<AuthController> _logger;


		public AuthController(IJWTManagerRepository jWTManager, ILogger<AuthController> logger)
		{
			this._jWTManager = jWTManager;
			_logger = logger;
		}

		[HttpGet]
		[Route("users")]
		public List<string> Get()
		{
			var users = new List<string>
		{
			"user1",
			"user2",
			"user3"
		};

			return users;
		}

		[HttpPost]
		public IActionResult Authenticate(Users usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				_logger.LogInformation("Username or password incorrect, please contact owner of this repository to add new user.");
				return Unauthorized();
			}

			return Ok(token);
		}

    }
}