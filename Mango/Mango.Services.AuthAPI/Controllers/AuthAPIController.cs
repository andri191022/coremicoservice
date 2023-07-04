using Mango.Services.AuthAPI.Model.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Messages = errorMessage;
                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginRespone = await _authService.Login(model);
            if (loginRespone.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Messages = "Username or Password is Incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.IsSuccess= true;
            _responseDto.Result = loginRespone;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Messages = "Error encounterer";
                return BadRequest(_responseDto);
            }
         
            return Ok(_responseDto);
        }

    }

}
