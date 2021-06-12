using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Infrastructure.Persistence.Data;
using Core.Application.Services;
using System.IO;
using Newtonsoft.Json;

namespace WebTemplate.NetCore.Controllers
{
    public class SalvasController : Controller
    {
        private Models.Message ResponseMessage { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly IBackUpService _backupService;

        public SalvasController(ApplicationDbContext context, IBackUpService backupService)
        {
            _context = context;
            _backupService = backupService;
        }

        // GET: Salvas1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salvas.Include(s => s.Conexion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Salvas1/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

            var salvas = await _context.Salvas
                .Include(s => s.Conexion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salvas == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.warning,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }

            return View(salvas);
        }

        // GET: Salvas1/Create
        public IActionResult Create()
        {
            ViewData["ConexionId"] = new SelectList(_context.Conexiones, "Id", "Nombre");
            return View(new Salvas { Fecha = DateTime.Now });
        }

        // POST: Salvas1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Fecha,ConexionId,Id")] Salvas salvas)
        {
            if (ModelState.IsValid)
            {
                salvas.Id = Guid.NewGuid();
                _context.Add(salvas);
                await _context.SaveChangesAsync();
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.success,
                    Content = Core.Language.Resources.SuccessfullyCreated
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConexionId"] = new SelectList(_context.Conexiones, "Id", "Nombre", salvas.ConexionId);
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.warning,
                Content = Core.Language.Resources.ValidationErrors
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return View(salvas);
        }

        // GET: Salvas1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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

            var salvas = await _context.Salvas.FindAsync(id);
            if (salvas == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.danger,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }
            ViewData["ConexionId"] = new SelectList(_context.Conexiones, "Id", "Nombre", salvas.ConexionId);
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.warning,
                Content = Core.Language.Resources.ValidationErrors
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return View(salvas);
        }

        // POST: Salvas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nombre,Fecha,ConexionId,Id")] Salvas salvas)
        {
            if (id != salvas.Id)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.danger,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salvas);

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
                    if (!SalvasExists(salvas.Id))
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
            ViewData["ConexionId"] = new SelectList(_context.Conexiones, "Id", "Nombre", salvas.ConexionId);
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.warning,
                Content = Core.Language.Resources.ValidationErrors
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return View(salvas);
        }

        // GET: Salvas1/Delete/5
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

            var salvas = await _context.Salvas
                .Include(s => s.Conexion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salvas == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.danger,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }

            return View(salvas);
        }

        // POST: Salvas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var salva = await _context.Salvas
                .Include(s => s.Conexion)
                .FirstOrDefaultAsync(a => a.Id == id);

            var path = Path.Combine($@"\\{salva.Conexion.Servidor}\{salva.Conexion.Path.Replace(':', '$')}", $"{salva.FullName}");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Salvas.Remove(salva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SalvasExists(Guid id)
        {
            return _context.Salvas.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> PerformBackup(Guid Id)
        {
            var salva = await _context.Salvas
                .Include(s => s.Conexion)
                .FirstOrDefaultAsync(a => a.Id == Id);
            if (salva == null)
            {
                ResponseMessage = new Models.Message
                {
                    MessageSeverity = Enums.EnumMessageSeverity.danger,
                    Content = Core.Language.Resources.ConcurrencyException
                };
                TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
                return NotFound();
            }
            _backupService.FullBackUpAsync(salva);
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.success,
                Content = Core.Language.Resources.BackUpSuccess
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DownloadBackup(Guid Id)
        {
            var salva = await _context.Salvas
                .Include(s => s.Conexion)
                .FirstOrDefaultAsync(a => a.Id == Id);

            var path = Path.Combine($@"\\{salva.Conexion.Servidor}\{salva.Conexion.Path.Replace(':', '$')}", $"{salva.FullName}");
            if (System.IO.File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open);
                return File(fs, "application/octet-stream", salva.FullName);
            }
            
            ResponseMessage = new Models.Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.danger,
                Content = Core.Language.Resources.BackUpNotFound
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}
