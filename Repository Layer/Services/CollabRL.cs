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
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public CollabRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = configuration;
        }

        public async Task<Collaborator> AddCollaborator(int userId, int NoteId, CollabValidation collab)
        {
            try
            {
                var user = fundooContext.Users.FirstOrDefault(u => u.userID == userId);
                var note = fundooContext.Note.FirstOrDefault(b => b.NoteId == NoteId);
                Collaborator collaborator = new Collaborator
                {
                    User = user,
                    Note = note
                };
                collaborator.CollabEmail = collab.email;
                fundooContext.Collaborators.Add(collaborator);
                await fundooContext.SaveChangesAsync();
                return collaborator;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByUserId(int userId)
        {
            try
            {
                List<Collaborator> result = await fundooContext.Collaborators.Where(u => u.userId == userId).Include(u => u.User).Include(U => U.Note).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId)
        {
            try
            {
                List<Collaborator> result = await fundooContext.Collaborators.Where(u => u.userId == userId && u.NoteId == NoteId).Include(u => u.User).Include(U => U.Note).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCollaborator(int userId, int NoteId, int CollabId)
        {
            try
            {
                var result = fundooContext.Collaborators.FirstOrDefault(u => u.userId == userId && u.NoteId == NoteId && u.CollabId == CollabId);
                if (result != null)
                {
                    fundooContext.Collaborators.Remove(result);
                    await fundooContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
