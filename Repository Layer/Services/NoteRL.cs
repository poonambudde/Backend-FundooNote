using Database_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Entity;
using Repository_Layer.FundooNotesContext;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        public IConfiguration configuration { get; }
        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public async Task AddNote(NotePostModel notePostModel, int UserId)
        {
            try
            {
                var user = fundooContext.Users.FirstOrDefault(u => u.userID == UserId);
                Note note = new Note
                {
                    User = user
                };
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BGColour = notePostModel.BGColour;
                fundooContext.Add(note);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Note> GetNote(int noteId, int userId)
        {
            try
            {
                var user = fundooContext.Users.FirstOrDefault(u => u.userID == userId);
                return await fundooContext.Note.Where(u => u.NoteId == noteId && u.UserId == userId)

                    .Include(u => u.User).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateNotes(int noteId, NotePostModel notePostModel)
        {
            Note note = fundooContext.Note.Where(e => e.NoteId == noteId).FirstOrDefault();
            note.Title = notePostModel.Title;
            note.Description = notePostModel.Description;
            note.BGColour = notePostModel.BGColour;
            fundooContext.Note.Update(note);
            var result = fundooContext.SaveChangesAsync();
            if (result != null)
                return true;
            else
                return false;
        }

        public bool DeleteNote(int noteId)
        {
            Note notes = fundooContext.Note.Where(e => e.NoteId == noteId).FirstOrDefault();
            if (notes != null)
            {
                fundooContext.Note.Remove(notes);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
