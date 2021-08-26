using Domain.DataModel;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModel.Admin;

namespace WebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            this.adminService = adminService;
            this._userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginRequestModel model)
        {
            var serviceResult = adminService.LoginAdmin(model.UserName, model.Password);
            return Ok(serviceResult);
        }

        [HttpPost("PostAddUser")]
        public async Task<IActionResult> PostAddUser([FromBody] AddUserModel user)
        {
            var identityuser = adminService.IdentityUser(user.UserName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (await adminService.IsExistsUser(user.UserName.Trim()))
            {
                return Content("UserName Is Exist Before");
            }
            else if (!await adminService.IdentityTokenAdmin(user.Token.Trim()))
            {
                return Content("It Is Not Token Admin");
            }
            else if (identityuser != null)
            {
                return Content(identityuser.ToString());
            }
            else
            {
                int folderid = await adminService.AddUser(user.UserName.Trim(), user.Password.Trim(), user.State);

                return Ok($"User Add Successfully / RootID = {folderid}");
            }
        }

        [HttpPost("ChangeUserState")]
        public async Task<IActionResult> ChangeUserState([FromBody] ChangeUserStateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await adminService.IdentityTokenAdmin(model.Token.Trim())) return Content("It Is Not Token Admin");
            if (!adminService.IsUserIdExists(model.UserId)) return Content("UserId Is Not Exist! ");
            adminService.SetUserState(model.UserId, model.NewState);
            return Ok($"UserState Changed Successfully ");
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers(GetUsersViewModel model)
        {
            if (!await adminService.IdentityTokenAdmin(model.Token.Trim())) return Content("It Is Not Token Admin");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var res = adminService.GetAllUsers();

            if (res.Count() == 0)
            {
                return Ok("Not Found User!");
            }

            return Ok(res);
        }

        [HttpPost("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordModel model)
        {
            if (!await adminService.IdentityTokenAdmin(model.AdminToken.Trim())) return Content("It Is Not Token Admin");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!adminService.IsUserIdExists(model.UserId)) return Content("UserId Is Not Exist! ");

            string oldPassword = _userService.GetUserPassword(model.UserId);
            if (oldPassword == model.NewPassword) return BadRequest("Password must be Different, Are You Have ALZHEIMER ?!");

            adminService.SetUserPassword(model.UserId, model.NewPassword);
            return Ok("Password have been Changed! ");
        }
        [HttpPost("Logout")]
        public IActionResult LogOut(LogOutAdminRequestedModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool isLogoutValid = adminService.IsLogoutValid(model.Token);

            if (!isLogoutValid) return NotFound(" Token is Invalid ");
            adminService.LogOutAdmin(model.Token);
            return Ok("Logout Successfully");

        }
    }
}
