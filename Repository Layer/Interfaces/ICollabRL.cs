using Database_Layer;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interfaces
{
    public interface ICollabRL
    {
        Task<Collaborator> AddCollaborator(int userId, int NoteId, CollabValidation collab);
        Task<List<Collaborator>> GetCollaboratorByUserId(int userId);
        Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId);
        Task<bool> DeleteCollaborator(int userId, int NoteId, int CollabId);

    }
}
