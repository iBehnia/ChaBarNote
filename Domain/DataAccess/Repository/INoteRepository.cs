using Domain.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess.Repository
{
    public interface INoteRepository
    {
        //TODO: Refactor All
        void AddNote(string Title, string Text, int UserId, int FolderId);
        int GetNoteID(int UserID, int FolderID,string NoteTitle);
        string GetNoteTitle(int noteId);
        int GetNoteCount(int userID);
        bool IsNoteTitleExist(string title, int userid, int folderid);
        List<string> GetFoldersNoteTitles(int folderid, int userid);
        void DeleteNote(int userId, int folderId, int noteId);
        bool IsNoteIdExist(int userId, int folderId, int noteId);
        bool IsNoteIdExist(int userId,int noteId);
        Note GetNoteDetails(int noteid);
        // void ChangeNotesFolderId(int noteId, int newfolderId);
        Note GetNote(int noteId);

        ////////////////////////////////////////////////////////
        void SetNoteTitle(int noteId, string newTitle);
        void SetNoteText(int noteId, string newText);
        int GetNotesFolderId(int noteId);


    }
}
