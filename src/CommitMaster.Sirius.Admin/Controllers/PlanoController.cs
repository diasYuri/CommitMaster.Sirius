using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommitMaster.Sirius.Domain.Entities;
using CommitMaster.Sirius.Infra.Data;

namespace CommitMaster.Sirius.Admin.Controllers
{
    public class PlanoController : Controller
    {
        private readonly AdminContext _context;

        public PlanoController(AdminContext context)
        {
            _context = context;
        }

        // GET: Plano
        public async Task<IActionResult> Index()
        {
            return View(await _context.Planos.ToListAsync());
        }

        // GET: Plano/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plano == null)
            {
                return NotFound();
            }

            return View(plano);
        }

        // GET: Plano/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Duracao,Preco,Descricao,Id,CreatedAt")] Plano plano)
        {
            if (ModelState.IsValid)
            {
                plano.Id = Guid.NewGuid();
                _context.Add(plano);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plano);
        }

        // GET: Plano/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }
            return View(plano);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Duracao,Preco,Descricao,Id,CreatedAt")] Plano plano)
        {
            if (id != plano.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoExists(plano.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plano);
        }

        // GET: Plano/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plano == null)
            {
                return NotFound();
            }

            return View(plano);
        }

        // POST: Plano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var plano = await _context.Planos.FindAsync(id);
            _context.Planos.Remove(plano);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoExists(Guid id)
        {
            return _context.Planos.Any(e => e.Id == id);
        }
    }
}
