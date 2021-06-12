using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;

using Infrastructure.Persistence.Data;
using Newtonsoft.Json;

namespace WebTemplate.NetCore.Controllers
{
    public class ConexionesController : Controller
    {
        private Models.Message ResponseMessage { get; set; }

        private readonly ApplicationDbContext _context;

        public ConexionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Conexiones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conexiones.ToListAsync());
        }

        // GET: Conexiones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conexion = await _context.Conexiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conexion == null)
            {
                return NotFound();
            }

            return View(conexion);
        }

        // GET: Conexiones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conexiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Servidor,Puerto,BaseDatos,Path,Id")] Conexion conexion)
        {

            if (ModelState.IsValid)
            {
                conexion.Id = Guid.NewGuid();
                _context.Add(conexion);
                await _context.SaveChangesAsync();
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.success,
                    Content = Core.Language.Resources.SuccessfullyCreated
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage) ;
                return RedirectToAction(nameof(Index));
            }
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.warning,
                Content = Core.Language.Resources.ValidationErrors
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return View(conexion);
        }

        // GET: Conexiones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conexion = await _context.Conexiones.FindAsync(id);
            if (conexion == null)
            {
                return NotFound();
            }
            return View(conexion);
        }

        // POST: Conexiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nombre,Servidor,Puerto,BaseDatos,Path,Id")] Conexion conexion)
        {
            if (id != conexion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conexion);
                    
                    await _context.SaveChangesAsync();
                    ResponseMessage = new Models.Message
                    {
                        MessageSeverity = Enums.EnumMessageSeverity.success,
                        Content = Core.Language.Resources.SuccessfullyEdited
                    };
                    TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConexionExists(conexion.Id))
                    {
                        ResponseMessage = new Models.Message
                        {
                            MessageSeverity = Enums.EnumMessageSeverity.danger,
                            Content = Core.Language.Resources.ConcurrencyException
                        };
                        TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.warning,
                Content = Core.Language.Resources.ValidationErrors
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return View(conexion);
        }

        // GET: Conexiones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.warning,
                    Content = Core.Language.Resources.BadRequest
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }

            var conexion = await _context.Conexiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conexion == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.danger,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }

            return View(conexion);
        }

        // POST: Conexiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var conexion = await _context.Conexiones.FindAsync(id);
            _context.Conexiones.Remove(conexion);
            await _context.SaveChangesAsync();
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.success,
                Content = Core.Language.Resources.SuccessfullyDeleted
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return RedirectToAction(nameof(Index));
        }

        private bool ConexionExists(Guid id)
        {
            return _context.Conexiones.Any(e => e.Id == id);
        }
    }
}
