using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly RoleManager<IdentityRole> roleManager;
		public RoleController(RoleManager<IdentityRole> _roleManager)
		{
			roleManager = _roleManager;
		}
		[HttpPost]
		public async Task<IActionResult> CreateRole(string roleName)
		{
			bool roleExist = await roleManager.RoleExistsAsync(roleName);

			if (!roleExist)
			{
				IdentityResult result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
				if (result.Succeeded)
				{
					return Ok($"Role {roleName} created successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}

			}
			else
			{
				return BadRequest($"Role {roleName} already exists");
			}

		}
		[HttpGet]
		public IActionResult GetRoles()
		{
			IQueryable<IdentityRole> roles = roleManager.Roles;
			return Ok(roles);
		}
		[HttpDelete]
		public async Task< IActionResult> DeleteRole(string roleName)
		{
			var role= await roleManager.FindByNameAsync(roleName);
			if(role != null)
			{
				IdentityResult result= await roleManager.DeleteAsync(role);
				if(result.Succeeded)
				{
					return Ok($"Role {roleName} deleted successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}

			}
			else
			{
				return BadRequest($"Role {roleName} not found");
			}
		}
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task <IActionResult>UpdateRole(string roleName,string newRoleName)
		{
			IdentityRole role = await roleManager.FindByNameAsync(roleName);
			if(role != null)
			{   role.Name = newRoleName;
				IdentityResult result=await roleManager.UpdateAsync(role);
				if(result.Succeeded)
				{
					return Ok($"Role {roleName} updated to {newRoleName} successfully");
				}
				else
				{
					return BadRequest(result.Errors);
				}

			}
			else
			{
				return NotFound($"Role {roleName} not found");
			}
		}
	}
}
