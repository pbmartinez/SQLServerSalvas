using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Infrastructure.Persistence.Data;

namespace DefaultBlazor.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiBaseController<TEntity> : ControllerBase where TEntity:Entity
    {
        private readonly ApplicationDbContext _context;

        public ApiBaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiBase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetEntity()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        // GET: api/ApiBase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetEntity(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // PUT: api/ApiBase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(Guid id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiBase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entity>> PostEntity(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntity", new { id = entity.Id }, entity);
        }

        // DELETE: api/ApiBase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityExists(Guid id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}
