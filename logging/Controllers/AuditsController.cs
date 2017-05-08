using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using logging.Models;

namespace logging.Controllers
{
    public class AuditsController : ApiController
    {
        private readonly AuditModel _dbContextModel;

        public AuditsController(AuditModel model)
        {
            _dbContextModel = model;
        }

        // GET: api/Audits
        public IQueryable<IAudit> GetAudits()
        {
            return _dbContextModel.Audits;
        }

        // GET: api/Audits/5
        [ResponseType(typeof(IAudit))]
        public async Task<IHttpActionResult> GetAudit(Guid id)
        {
            IAudit audit = await _dbContextModel.Audits.FindAsync(id);
            if (audit == null)
            {
                return NotFound();
            }

            return Ok(audit);
        }

        // PUT: api/Audits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAudit(Guid id, IAudit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != audit.LogId)
            {
                return BadRequest();
            }

            _dbContextModel.Entry(audit).State = EntityState.Modified;

            try
            {
                await _dbContextModel.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Audits
        [ResponseType(typeof(Audit))]
        public async Task<IHttpActionResult> PostAudit(Audit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContextModel.Audits.Add(audit);

            try
            {
                await _dbContextModel.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuditExists(audit.LogId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = audit.LogId }, audit);
        }

        // DELETE: api/Audits/5
        [ResponseType(typeof(IAudit))]
        public async Task<IHttpActionResult> DeleteAudit(Guid id)
        {
            IAudit audit = await _dbContextModel.Audits.FindAsync(id);
            if (audit == null)
            {
                return NotFound();
            }

            _dbContextModel.Audits.Remove(audit);
            await _dbContextModel.SaveChangesAsync();

            return Ok(audit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContextModel.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditExists(Guid id)
        {
            return _dbContextModel.Audits.Count(e => e.LogId == id) > 0;
        }
    }
}