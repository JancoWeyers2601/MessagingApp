using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessagingApp.Models;

namespace MessagingApp.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MessagingAppContext _context;

        public MessagesController(MessagingAppContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
              return View(await _context.TblMessages.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblMessages == null)
            {
                return NotFound();
            }

            var message = await _context.TblMessages
                .FirstOrDefaultAsync(m => m.PkTblMessage == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkTblMessage,FkTblConversation,FkTblUser,TblMessageHeader,TblMessageBody")] MessageModel message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblMessages == null)
            {
                return NotFound();
            }

            var message = await _context.TblMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkTblMessage,FkTblConversation,FkTblUser,TblMessageHeader,TblMessageBody")] MessageModel message)
        {
            if (id != message.PkTblMessage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.PkTblMessage))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblMessages == null)
            {
                return NotFound();
            }

            var message = await _context.TblMessages
                .FirstOrDefaultAsync(m => m.PkTblMessage == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblMessages == null)
            {
                return Problem("Entity set 'MessagingAppContext.TblMessages'  is null.");
            }
            var message = await _context.TblMessages.FindAsync(id);
            if (message != null)
            {
                _context.TblMessages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
          return _context.TblMessages.Any(e => e.PkTblMessage == id);
        }
    }
}
