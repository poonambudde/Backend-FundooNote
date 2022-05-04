using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository_Layer.FundooNotesContext;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class LabelRL : ILabelRL
    {
        FundooContext fundooContext;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = configuration;
        }

        public async Task AddLabel(int userId, int noteId, string LabelName)
        {
            try
            {
                var user = fundooContext.Users.FirstOrDefault(u => u.userID == userId);
                var note = fundooContext.Note.FirstOrDefault(b => b.NoteId == noteId);
                Entity.Label label = new Entity.Label
                {
                    User = user,
                    Note = note
                };
                label.LabelName = LabelName;
                fundooContext.Label.Add(label);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Entity.Label>> GetLabelByuserId(int userId)
        {
            try
            {
                List<Entity.Label> reuslt = await fundooContext.Label.Where(u => u.UserId == userId).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Entity.Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
                List<Entity.Label> reuslt = await fundooContext.Label.Where(u => u.NoteId == NoteId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entity.Label> UpdateLabel(int userId, int LabelId, string LabelName)
        {
            try
            {

                Entity.Label reuslt = fundooContext.Label.FirstOrDefault(u => u.LabelId == LabelId && u.UserId == userId);

                if (reuslt != null)
                {
                    reuslt.LabelName = LabelName;
                    await fundooContext.SaveChangesAsync();
                    var result = fundooContext.Label.Where(u => u.LabelId == LabelId).FirstOrDefaultAsync();
                    return reuslt;
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

        public async Task DeleteLabel(int LabelId, int userId)
        {
            try
            {
                var result = fundooContext.Label.FirstOrDefault(u => u.LabelId == LabelId && u.UserId == userId);
                fundooContext.Label.Remove(result);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}