using Business_Layer.Interfaces;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundooContext fundooContext;
        INoteBL noteBL;
        public NoteController(INoteBL noteBL, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.noteBL.AddNote(notePostModel, UserId);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult> GetNote(int noteId, int userId)
        {
            try
            {
                await this.noteBL.GetNote(noteId, userId);
                return this.Ok(new { success = true, message = "Get Note Successfull " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
