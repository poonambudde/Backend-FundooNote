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

        public async Task AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {
                var user = fundooContext.Users.FirstOrDefault(u => u.userID == userId);
                Note note = new Note
                {
                    User = user
                };
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BGColour = notePostModel.BGColour;
                note.IsArchive = false;
                note.IsReminder = false;
                note.IsPin = false;
                note.IsTrash = false;
                note.CreatedAt = DateTime.Now;

                fundooContext.Add(note);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
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

        public async Task<Note> UpdateNote(NotePostModel notePostModel, int noteId, int userId)
        {
            try
            {
                var res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.Title = notePostModel.Title;
                    res.Description = notePostModel.Description;
                    res.BGColour = notePostModel.BGColour;
                    res.IsArchive = notePostModel.IsArchive;
                    res.IsReminder = notePostModel.IsReminder;
                    res.IsPin = notePostModel.IsPin;
                    res.IsTrash = notePostModel.IsTrash;
                    res.ModifiedAt = DateTime.Now;
                    await fundooContext.SaveChangesAsync();

                    return await fundooContext.Note.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                Note res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                fundooContext.Note.Remove(res);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Note>> GetAllNote(int userId)
        {
            try
            {
                return await fundooContext.Note.Where(u => u.UserId == userId).Include(u => u.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> ArchieveNote(int noteId, int userId)
        {
            try
            {
                var res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsArchive == false)
                    {
                        res.IsArchive = true;
                    }
                    else
                    {
                        res.IsArchive = false;
                    }
                    await fundooContext.SaveChangesAsync();
                    return await fundooContext.Note.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Note> PinNote(int noteId, int userId)
        {
            try
            {
                var res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsPin == false)
                    {
                        res.IsPin = true;
                    }
                    if (res.IsPin == true)
                    {
                        res.IsPin = false;
                    }
                    await fundooContext.SaveChangesAsync();
                    return await fundooContext.Note.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Note> TrashNote(int noteId, int userId)
        {
            try
            {
                var res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsTrash == false)
                    {
                        res.IsTrash = true;
                    }
                    if (res.IsTrash == true)
                    {
                        res.IsTrash = false;
                    }
                    await fundooContext.SaveChangesAsync();
                    return await fundooContext.Note.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Note> ChangeColor(int noteId, int userId, string newColor)
        {
            try
            {
                var res = fundooContext.Note.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.BGColour = newColor;
                    await fundooContext.SaveChangesAsync();
                    return await fundooContext.Note.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllNotes_ByRadisCache()
        {
            try
            {
                return await fundooContext.Note.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllNotes_ByRadisCache()
        {
            try
            {
                return await fundooContext.Note.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
