using Business_Layer.Interfaces;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
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
                return this.Ok(new { success = true, message = "Get Note Successful " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("updatenote/{noteId}")]
        public IActionResult UpdateNotes(int noteId, NotePostModel notePostModel)
        {
            try
            {
                if (noteBL.UpdateNotes(noteId, notePostModel))
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully", response = notePostModel, noteId });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("deleteNote/{noteId}")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                if (noteBL.DeleteNote(noteId))
                {
                    return this.Ok(new { Success = true, message = "Notes deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
