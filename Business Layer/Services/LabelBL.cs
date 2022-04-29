using Business_Layer.Interfaces;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL; ;
        }

        public async Task AddLabel(int userId, int noteId, string LabelName)
        {
            try
            {
                await this.labelRL.AddLabel(userId, noteId, LabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Repository_Layer.Entity.Label>> GetLabelByuserId(int userId)
        {
            try
            {
                return await this.labelRL.GetLabelByuserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Repository_Layer.Entity.Label>> GetLabelByNoteId(int noteId)
        {
            try
            {
                return await this.labelRL.GetlabelByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Repository_Layer.Entity.Label> UpdateLabel(int userId, int labelId, string labelName)
        {
            try
            {
                return await this.labelRL.UpdateLabel(userId, labelId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLabel(int labelId, int userId)
        {
            try
            {
                await this.labelRL.DeleteLabel(labelId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
