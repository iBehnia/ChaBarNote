using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModel.Note;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {

        private IUserService _userService;
        private IFolderService _folderService;
        private INoteService _noteService;
        public NoteController(IFolderService folderService, IUserService userService, INoteService noteService)
        {
            _folderService = folderService;
            _userService = userService;
            _noteService = noteService;
        }



        [HttpPost("CreateNote")]
        public IActionResult CreateNote([FromBody] CreateNoteModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Text == null) model.Text = "";

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);


            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");


            UserState userstate = _userService.GetUserState(userid);

            if (UserState.Normal == userstate && _noteService.CheckNoteCount(userid))
            {
                return BadRequest("You Are Limited For 10 Notes \n for more access please call MR Reza Taati ! ");
            }


            bool isFolderNameExist = _folderService.IsFolderNameExist(model.Title, userid, model.FolderId);
            bool isNoteTitleExist = _noteService.IsNoteTitleExist(model.Title, userid, model.FolderId);
            if (isFolderNameExist || isNoteTitleExist) return NotFound("NoteTitle is Repeated !");



            int NoteId = _noteService.AddNote(model.Title, model.Text, userid, model.FolderId);
            return Ok($" Successful ! Note ID is {NoteId}");

        }

        [HttpPost("DeleteNote")]
        public IActionResult DeleteNote([FromBody] DeleteNoteModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");

            bool isNoteIDExist = _noteService.IsNoteIdExist(userid, model.FolderId, model.NoteId);
            if (!isNoteIDExist || model.NoteId == 0) return NotFound("NoteId is not Exist !");//Refactore

            _noteService.DeleteNote(userid, model.FolderId, model.NoteId);
            return Ok($" Successful ! Noteid = {model.NoteId}  Deleted.");
        }
        [HttpPost("GetNoteDetails")]
        public IActionResult GetNoteDetails([FromBody] GetNoteDetailsModel model)
        {
            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            int userid = _userService.GetUserID(model.Token);
            bool isNoteIDExist = _noteService.IsNoteIdExist(userid,model.NoteID);
            if (!isNoteIDExist || model.NoteID == 0) return NotFound("NoteId is not Exist !");//Refactore

            var Result =_noteService.GetNoteDetails(model.NoteID);
            
            return Ok(Result);
        }
        [HttpPost("ChangeNoteDirectory")]
        public IActionResult ChangeNoteDirectory([FromBody] ChangeNoteDirectoryModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
          
            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isNoteIDExist = _noteService.IsNoteIdExist(userid, model.NoteId);
            if (!isNoteIDExist || model.NoteId == 0) return NotFound("NoteId is not Exist !");

            bool isFolderIDExist = _folderService.IsFolderIDExist(model.FolderId, userid);
            if (!isFolderIDExist || model.FolderId == 0) return NotFound("FolderId is not Exist !");

            var noteTitle = _noteService.GetNoteTitle(model.NoteId);

            bool isFolderNameExist = _folderService.IsFolderNameExist(noteTitle, userid, model.FolderId);
            bool isNoteTitleExist = _noteService.IsNoteTitleExist(noteTitle, userid, model.FolderId);
            if (isFolderNameExist || isNoteTitleExist) return NotFound("NoteTitle is Exist in Destination Folder !");

            _noteService.ChangeNoteFolderId(model.NoteId, model.FolderId);

            return Ok($" Successful ! ");

        }
        ///////////////////////////////////////////////////
        [HttpPost("EditNote")]
        public IActionResult EditNote([FromBody] EditNoteModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsAuthenticated = _userService.IdentityTokenUser(model.Token);
            if (!IsAuthenticated) return NotFound("Token is Invalid");

            int userid = _userService.GetUserID(model.Token);

            bool isNoteIDExist = _noteService.IsNoteIdExist(userid, model.NoteId);
            if (!isNoteIDExist || model.NoteId == 0) return NotFound("NoteId is not Exist !");

            string result = " Successful ! \n";

            var folderId = _noteService.GetNotesFolderId(model.NoteId);
            bool isFolderNameExist = _folderService.IsFolderNameExist(model.Title, userid, folderId);


            bool isNoteTitleExist = _noteService.IsNoteTitleExist(model.Title, userid, folderId);
            if (isFolderNameExist || isNoteTitleExist)
            {
                string noteTitle = _noteService.GetNoteTitle(model.NoteId);
                if(noteTitle != model.Title) return NotFound("NoteTitle is Repeated !");
            }

            _noteService.SetNoteTitle(model.NoteId, model.Title);
            result += $"Note Title changed to: {model.Title}\n";

           if (String.IsNullOrEmpty(model.Text))
            {
                model.Text = ""; 
            }
            _noteService.SetNoteText(model.NoteId, model.Text);
            result += $"Note Text changed to: {model.Text}\n";
            return Ok(result);

        }
    }
}
