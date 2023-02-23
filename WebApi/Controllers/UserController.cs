using Application.DTO;
using Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.OData;

namespace ApiCourseTwo.Controllers
{
   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]   
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _userService.GenerateToken(loginDTO);
            if (result.IsSuccess) return Ok(result.Data);
            return BadRequest(result.Errors);
        }
        [HttpPost("logingoogle")]
        public async Task<ActionResult> LoginWithGoogle([FromBody] UserGoogleDTO userGoogleDTO)
        {
            var result = await _userService.LoginWithGoogle(userGoogleDTO);
            return Ok(result.Data);
          
        }

        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody] UserDTO userDTO)
        {
            var result = await _userService.Create(userDTO);
            if (result.IsSuccess) return Ok(result.Data);
            return BadRequest(result);
        }
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> FindByAll()
        {
            var result = await _userService.FindByAll();
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var result = await _userService.FindById(id);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch([FromRoute]int id,string attribute, string field)
        {
            var result = await _userService.Patch(id,attribute,field);
            return Ok(result);
          

        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserDTO userDTO)
        {
            var result = await _userService.Update(userDTO);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }


}

