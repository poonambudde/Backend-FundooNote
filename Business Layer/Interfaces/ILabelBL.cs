using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId, string LabelName);
        Task<List<Repository_Layer.Entity.Label>> GetLabelByuserId(int userId);
        Task<List<Repository_Layer.Entity.Label>> GetLabelByNoteId(int noteId);
        Task<Repository_Layer.Entity.Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);
    }
}
