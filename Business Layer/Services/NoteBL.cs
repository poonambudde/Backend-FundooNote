using Business_Layer.Interfaces;
using Database_Layer;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using Repository_Layer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL userRL)
        {
            this.noteRL = userRL;
        }
        public async Task AddNote(NotePostModel notepostModel, int UserId)
        {
            try
            {
                await this.noteRL.AddNote(notepostModel, UserId);
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
                return await this.noteRL.GetNote(noteId, userId);
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
                return await this.noteRL.UpdateNote(notePostModel, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task DeleteNote(int noteId, int userId)
        {
            try
            {
                return this.noteRL.DeleteNote(noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Note>> GetAllNotes(int userId)
        {
            try
            {
                return await this.noteRL.GetAllNote(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



