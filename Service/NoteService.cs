using Domain.DataAccess.Repository;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class NoteService : INoteService
    {
        private IFolderRepository _folderRepository;
        private IUserRepository _userRepository;
        private INoteRepository _noteRepository;
        public NoteService(IFolderRepository folderRepository, IUserRepository userRepository, INoteRepository noteRepository)
        {
            _folderRepository = folderRepository;
            _userRepository = userRepository;
            _noteRepository = noteRepository;
        }
        public int AddNote(string Title, string Text, int UserId, int FolderId)
        {
            _noteRepository.AddNote(Title, Text, UserId, FolderId);
           int GetNoteId =  _noteRepository.GetNoteID(UserId, FolderId, Title);
            return GetNoteId;
        }

        public bool CheckNoteCount(int userID)
        {
            int count =  _noteRepository.GetNoteCount(userID);
            return count > 9;
        }

        public void DeleteNote(int userId, int folderId, int noteId)
        {
            _noteRepository.DeleteNote(userId, folderId, noteId);
        }

        public string GetFoldersNoteTitles(int folderid, int userid)
        {
            List<string> namesList = _noteRepository.GetFoldersNoteTitles(folderid, userid);
            if (namesList == null || namesList.Count == 0) return "There is not Note in here.";
            string notes = "";
            foreach (var note in namesList)
            {
                notes += note + "\n";
            }
            return notes;
        }

        public string GetNoteDetails(int noteid)
        {
           var note =  _noteRepository.GetNoteDetails(noteid);
            string result = $"NoteTitle : {note.NoteTitle} \nNoteText : {note.NoteText}";
            return result;
        }
       
        public bool IsNoteIdExist(int userId, int folderId, int noteId)
        {
            return _noteRepository.IsNoteIdExist(userId, folderId, noteId);
        }
        public bool IsNoteIdExist(int userId, int noteId)
        {
            return _noteRepository.IsNoteIdExist(userId,noteId);
        }
        //public int GetNoteID(int UserID, int FolderID, string NoteTitle)
        //{
        //    int GetNoteId  = _noteRepository.GetNoteID(UserID, FolderID, NoteTitle);
        //    return GetNoteId;
        //}

        public bool IsNoteTitleExist(string title, int userid, int folderid)
        {
            return _noteRepository.IsNoteTitleExist(title, userid, folderid);
        }
        public void ChangeNoteFolderId(int noteId, int newFolderId)
        {
            var note = _noteRepository.GetNote(noteId);
            _noteRepository.DeleteNote(note.UserId, note.FolderId, note.NoteId);
            _noteRepository.AddNote(note.NoteTitle, note.NoteText,note.UserId, newFolderId);
        }

        public string GetNoteTitle(int noteId)
        {
            return _noteRepository.GetNoteTitle(noteId);
        }
        ///////////////////////////////////////////////////
        public void SetNoteTitle(int noteId, string newTitle)
        {
            _noteRepository.SetNoteTitle(noteId, newTitle);
        }

        public void SetNoteText(int noteId, string newText)
        {
            _noteRepository.SetNoteText(noteId, newText);
        }

        public int GetNotesFolderId(int noteId)
        {
            return _noteRepository.GetNotesFolderId(noteId);
        }
    }
}
