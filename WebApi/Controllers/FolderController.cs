using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Service;
using WebApi.ViewModel;
using System.Text.RegularExpressions;
using WebApi.ViewModel.Folder;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private IUserService _userService;
        private IFolderService _folderService;
        public FolderController(IFolderService folderService, IUserService userService)
        {
            _folderService = folderService;
            _userService = userService;
        }

        [HttpPost("GetFolderNames")]
        public IActionResult GetFolderNames([FromBody] GetFolderNamesModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");

            string folderNames = _folderService.GetSubFolderNames(model.FolderId, userid);
            return Ok(folderNames);
        }

        [HttpPost("CreateFolder")]
        public IActionResult CreateFolder([FromBody] CreateFolderModel CFM)
        {
            if (!ModelState.IsValid || CFM.FolderName.Trim() == "") return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(CFM.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(CFM.Token);
            UserState userstate = _userService.GetUserState(userid);

            if (UserState.Normal == userstate && _folderService.CheckFolderCount(userid))
            {
                return BadRequest("You Are Limited For 10 Folders \n for more access please call MR Reza Taati ! ");
            }

            bool isParentIDExist = _folderService.IsFolderIDExist(CFM.ParentID, userid);
            if (!isParentIDExist /*&& CFM.ParentID !=0*/) return NotFound("Parent is not Exist !");

            bool isfolderexist = _folderService.IsFolderNameExist(CFM.FolderName, userid, CFM.ParentID);
            if (isfolderexist) return NotFound("Folder is Already Exist !");

            int folderid = _folderService.CreateFolder(CFM.FolderName, userid, CFM.ParentID);
            return Ok($" Successful ! folder ID is {folderid}");

        }
        // POST: api/Folder
        [HttpPost("EditFolderName")]
        public IActionResult EditFolderName([FromBody] EditFolderNameModel model)
        {
            if (!ModelState.IsValid || model.name.Trim() == "") return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("Folder is not Exist !");


            bool isCheckedRootFolder = _folderService.isRootFolder(userid, model.FolderId);
            if (isCheckedRootFolder) return BadRequest("Root Folder can not be Edited !");

            _folderService.SetFolderName(model.FolderId, userid, model.name);

            return Ok($" Successful \n Your folder name changed to {model.name}");

        }
        //====================================================================
        [HttpPost("DeleteFolder")]
        public IActionResult DeleteFolder([FromBody] DeleteFolderModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");

            bool isCheckedRootFolder = _folderService.isRootFolder(userid, model.FolderId);
            if (isCheckedRootFolder) return BadRequest("Root Folder can not deleted !");//Error

            _folderService.DeleteFolder(userid, model.FolderId);
            return Ok($" Successful \n FolderId : {model.FolderId} deleted !");
        }
    }
}
