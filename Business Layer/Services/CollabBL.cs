using Business_Layer.Interfaces;
using Database_Layer;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public async Task<Collaborator> AddCollaborator(int userId, int Noteid, CollabValidation collab)
        {
            try
            {
                return await this.collabRL.AddCollaborator(userId, Noteid, collab);
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
                return await this.collabRL.GetCollaboratorByUserId(userId);
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
                return await this.collabRL.GetCollaboratorByNoteId(userId, NoteId);
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
                return await this.collabRL.DeleteCollaborator(userId, NoteId, CollabId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

