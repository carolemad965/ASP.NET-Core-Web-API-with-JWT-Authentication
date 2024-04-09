using Lab1.DTO;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;
		public UserController(UserManager<ApplicationUser> _userManager)
		{
			userManager = _userManager;
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateUser(RegisterUserDTO userDTO)
		{
			if(ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser
				{
					UserName = userDTO.UserName,
					Email = userDTO.Email,
					PasswordHash=userDTO.Password,
				};
				
				IdentityResult result=	await userManager.CreateAsync(user,userDTO.Password);
				if(result.Succeeded)
				{
					return Ok($"User {user.UserName} created successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult GetUsers()
         {
            var users = userManager.Users.ToList();
            return Ok(users);
         }
		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult>DeleteUser(string Name)
		{
		 ApplicationUser user=	await userManager.FindByNameAsync(Name);
			if(user!=null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
				if(result.Succeeded)
				{
					return Ok($"User {Name} deleted successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			return NotFound($"User with Name {Name} not found");
		}
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult>UpdateUser(string userName, RegisterUserDTO userDTO)
		{
			ApplicationUser user=await userManager.FindByNameAsync(userName);
			if (user != null)
			{
				user.UserName = userDTO.UserName;
				user.PasswordHash = userDTO.Password;
				user.Email = userDTO.Email;

				IdentityResult result = await userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return Ok($"User {userName} updated successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			return NotFound($"User with Name {userName} not found");
		}


	}
}
