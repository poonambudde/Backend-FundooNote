using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int userId, int noteId, string LabelName);

        Task<List<Entity.Label>> GetLabelByuserId(int userId);
        Task<List<Entity.Label>> GetlabelByNoteId(int NoteId);
        Task<Entity.Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);
    }
}
