using Database_Layer;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface INoteBL
    {
        Task AddNote(NotePostModel notePostModel, int UserId);
        Task<Note> GetNote(int noteId, int UserId);
    }
}
