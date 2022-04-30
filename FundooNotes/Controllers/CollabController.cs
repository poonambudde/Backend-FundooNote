using Business_Layer.Interfaces;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using Repository_Layer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        //Initializing Class
        FundooContext fundooContext;
        ICollabBL collabBL;

        //Creating Constructor
        public CollaboratorController(ICollabBL collabBL, FundooContext fundooContext)
        {
            this.collabBL = collabBL;
            this.fundooContext = fundooContext;
        }

        //HTTP method to handle add collaborator request
        [Authorize]
        [HttpPost("AddCollaborator/{NoteId}")]
        public async Task<ActionResult> AddCollaborator(int NoteId, CollabValidation collab)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var Id = fundooContext.Note.Where(x => x.NoteId == NoteId && x.UserId == userId).FirstOrDefault();
                if (Id == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var result = await this.collabBL.AddCollaborator(userId, NoteId, collab);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborator added successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to add collaborator", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle get collaborator request
        [Authorize]
        [HttpGet("GetCollaboratorByUserId")]
        public async Task<ActionResult> GetCollaboratorByUserId()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var Id = fundooContext.Collaborators.Where(x => x.userId == userId).FirstOrDefault();
                if (Id == null)
                {
                    return this.BadRequest(new { success = false, message = $"User doesn't exists" });
                }
                List<Collaborator> result = await this.collabBL.GetCollaboratorByUserId(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborator got successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to get collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle get collaborator request
        [Authorize]
        [HttpGet("GetCollaboratorByNoteId/{NoteId}")]
        public async Task<ActionResult> GetCollaboratorByNoteId(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var Id = fundooContext.Collaborators.FirstOrDefault(x => x.NoteId == NoteId && x.userId == userId);
                if (Id == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                List<Collaborator> result = await this.collabBL.GetCollaboratorByUserId(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborator got successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to get collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle delete collaborator request
        [Authorize]
        [HttpDelete("DeleteCollaborator/{NoteId}")]
        public async Task<ActionResult> DeleteCollaborator(int NoteId, int CollabId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundooContext.Collaborators.Where(x => x.userId == userId && x.NoteId == NoteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                bool result = await this.collabBL.DeleteCollaborator(userId, NoteId, CollabId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = $"Collaborator is deleted successfully" });
                }
                return this.BadRequest(new { success = false, message = $"Failed to delete collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
