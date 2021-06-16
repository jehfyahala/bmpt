using bmpt.Data;
using bmpt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bmpt.Controllers
{
    public class MusicController: Controller
    {
        private readonly ApplicationDbContext db;

        public MusicController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            //metodo controlador index
            return View(await db.Musics.ToListAsync());

        }

        //vista de departamentos en pocas palabras consultas
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var music = await db.Musics.FirstOrDefaultAsync(m => m.MusicId == id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }



        //creacion vista
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//metodo atraves de post
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Create(Music music)
        {
            if (ModelState.IsValid)
            {
                db.Add(music);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        //metodo de editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var music = await db.Musics.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        //guardar modificaciones en la dbf
        [HttpPost]//metodo atraves de post
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Edit(int id, Music music)
        {
            if (id != music.MusicId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(music);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }
        //fin editar
        //borrar metodo final
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var music = await db.Musics.FirstOrDefaultAsync(m => m.MusicId == id);
            //buscar en la tabla de departamentos y quedara en depart
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }
        //inicio borrar
        [HttpPost, ActionName("Delete")]//metodo atraves de post, el metodo hace el llamado
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Delete(int id)
        {
            var music = await db.Musics.FindAsync(id);
            db.Musics.Remove(music);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //fin borrar

    }
}
