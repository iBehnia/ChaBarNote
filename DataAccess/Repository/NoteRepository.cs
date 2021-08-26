using Domain.DataAccess.Repository;
using Domain.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class NoteRepository : INoteRepository
    {
        private ChaBarNoteContext _context;
      
        public NoteRepository(ChaBarNoteContext context)
        {
            _context = context;
        }
        public void AddNote(string Title, string Text, int UserId, int FolderId)
        {
            Note note = new Note() { NoteTitle = Title,NoteText = Text,UserId = UserId,FolderId = FolderId};

            _context.Note.Add(note);
            _context.SaveChanges();

        }
        public void DeleteNote(int userId, int folderId, int noteId)
        {
            Note n = new Note() { 
            UserId = userId,
            NoteId = noteId,
            FolderId = folderId,
            };
            _context.Note.Remove(n);
            _context.SaveChanges();
        }
        public List<string> GetFoldersNoteTitles(int folderid, int userid)
        {
            return _context.Note
             .Where(x => x.UserId == userid && x.FolderId == folderid)
             .Select(x => x.NoteTitle)?.ToList();
        }
        public int GetNoteCount(int userID)
        {
            return  _context.Note
               .Count(x => x.UserId == userID);
        }

        public Note GetNoteDetails(int noteid)
        {
            var c = _context.Note
               .Where(x => x.NoteId == noteid)
               .Select(x => new Note {NoteTitle = x.NoteTitle ,NoteText = x.NoteText }).SingleOrDefault();
            return c;
        }

        public int GetNoteID(int UserID, int FolderID, string NoteTitle)
        {
            return _context.Note.Where(x => x.UserId == UserID && x.FolderId == FolderID && x.NoteTitle == NoteTitle)
                .Select(x => x.NoteId).FirstOrDefault();
        }

        public string GetNoteTitle(int noteId)
        {
            return _context.Note.Where(x => x.NoteId == noteId )
                 .Select(x => x.NoteTitle).FirstOrDefault();
        }

        public bool IsNoteIdExist(int userId, int folderId, int noteId)
        {
            return _context.Note.Any(x => x.UserId == userId && x.FolderId == folderId && x.NoteId == noteId);
        }
        public bool IsNoteIdExist(int userId, int noteId)
        {
            return _context.Note.Any(x => x.UserId == userId && x.NoteId == noteId);
        }
        public bool IsNoteTitleExist(string title, int userid, int folderid)
        {
            return _context.Note.Any(x => x.NoteTitle == title && x.UserId == userid && x.FolderId == folderid);
        }
        public Note GetNote(int noteId)
        {
            return _context.Note.AsNoTracking()
                .Where(x => x.NoteId == noteId)
                .FirstOrDefault();
        }
        /////////////////////////////////////////////////////////////////
        public void SetNoteTitle(int noteId, string newTitle)
        {

            var f = _context.Note.Where(x => x.NoteId == noteId).FirstOrDefault();
            f.NoteTitle = newTitle;

            _context.Update(f);
            _context.SaveChanges();
        }

        public void SetNoteText(int noteId, string newText)
        {
            var f = _context.Note.Where(x => x.NoteId == noteId).FirstOrDefault();
            f.NoteText = newText;
            _context.Update(f);
            _context.SaveChanges();
        }

        public int GetNotesFolderId(int noteId)
        {
            var foderId = _context.Note.Where(x => x.NoteId == noteId).Select(x => x.FolderId).FirstOrDefault();
            return foderId;
        }
    }
}
