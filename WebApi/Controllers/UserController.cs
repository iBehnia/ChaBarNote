using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModel;
using WebApi.ViewModel.Admin;
using WebApi.ViewModel.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private IUserService _userService;
        private IFolderService _folderService;
        private INoteService _noteService;
        public UserController(IFolderService folderService, IUserService userService, INoteService noteService)
        {
            _folderService = folderService;
            _userService = userService;
            _noteService = noteService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestModel model)
        {

            bool isLoginValid = _userService.IsLoginValid(model.UserName, model.Password);
            if (!isLoginValid) return NotFound("username or password is Invalid");
            var token = _userService.LoginUser(model.UserName);
            return Ok(token);

        }

        [HttpPost("Logout")]
        public IActionResult Logout(LogOutRequestModel model)
        {

            bool isLoginValid = _userService.IsLogoutValid(model.UserName, model.token);
            if (!isLoginValid) return NotFound("username or token is Invalid");
            _userService.LogoutUser(model.UserName);
            return Ok("Logout Successfully");

        }


        [HttpPost("GetFolderItems")]
        public IActionResult GetFolderItems(GetFolderItemsModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");

            string folderNames = _folderService.GetSubFolderNames(model.FolderId, userid);
            string noteTitles = _noteService.GetFoldersNoteTitles(model.FolderId, userid);

            return Ok($"***** Folders ***** \n{folderNames} \n***** Notes ***** \n{noteTitles}");
        }
        [HttpPost("EditPassword")]
        public IActionResult EditPassword(EditPasswordModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);
            string oldPassword = _userService.GetUserPassword(userid);

            if (oldPassword == model.NewPassword) return BadRequest("Password must be Different, Are You Have ALZHEIMER ?!");
            _userService.SetUserPassword(userid, model.NewPassword);
            return Ok("Your Password have been Changed! ");
        }

        [HttpPost("SearchFolders")]
        public IActionResult SearchFolders(SearchFoldersModel model)
        {


            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            int userid = _userService.GetUserID(model.Token);

            //if (String.IsNullOrEmpty(model.FolderName))
            //{
            //    model.FolderName = "";
            //}

            var res = _userService.SearchFolderDirectory(userid, model.FolderName);

            if (res.Count() == 0)
            {
                return Ok("Not Found AnyThing!");
            }



            return Ok(res);

        }

        [HttpPost("ShowDailyMessage")]
        public IActionResult ShowDailyNote(ShowDailyMessageModel model)
        {
            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Random rd = new Random();
            String[] texts = {
                "آقا سلام 9 رو 10 میدید؟",
                "عرض سلام و ادب خدمت مالک محصول عزیز",
                "ونک دو نفر",
                "میازار موری که دانه کش است",
                "آقا رحم کنید",
                "گروه همیشه پیروز",
                "بر لب جوی نشین و گذر عمر ببین",
                "دانشگاه باید دانشگاه باشد دانشگاهی که دانشگاه نباشد دانشگاه نیست",
                "در جاده موفقیت محدودیت سرعت وجود ندارد",
                "و شب آغاز تمام دلتنگیهاست",
                "کلاس نه گذاشتنیه نه داشتنی نه رفتنی کلاس فقط پیچوندنیه به افتخار همه دانشجوها",
                "تو کلاس ما هر کی دیر میاد کفاره داره کفارشم اینکه ردیف جلو بشینه",
                "امید من به شما دبستانی هاست"
            };
            int index = rd.Next(0, texts.Length);
            
            return Ok(texts[index]);
        }
    }
}
