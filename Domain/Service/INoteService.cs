using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public interface INoteService
    {
        int AddNote(string Title, string Text, int UserId, int FolderId);
        //int GetNoteID(int UserID, int FolderID,string NoteTitle);
        bool CheckNoteCount(int userID);
        bool IsNoteTitleExist(string title, int userid, int folderid);
        string GetFoldersNoteTitles(int folderid, int userid);
        void DeleteNote(int userId, int folderId, int noteId);
        bool IsNoteIdExist(int userId, int folderId, int noteId);
        bool IsNoteIdExist(int userId, int noteId);
        string GetNoteDetails(int noteid);
        void ChangeNoteFolderId(int noteId, int newFolderId);
        string GetNoteTitle(int noteId);
        //////////////////////////////////////////////////////
        void SetNoteTitle(int noteId, string newTitle);
        void SetNoteText(int noteId, string newText);
        int GetNotesFolderId(int noteId);
    }
}
