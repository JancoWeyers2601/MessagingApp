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
    public class ConversationAccessesController : Controller
    {
        private readonly MessagingAppContext _context;

        public ConversationAccessesController(MessagingAppContext context)
        {
            _context = context;
        }

        // GET: ConversationAccesses
        public async Task<IActionResult> Index()
        {
              return View(await _context.TblConversationAccesses.ToListAsync());
        }

        // GET: ConversationAccesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblConversationAccesses == null)
            {
                return NotFound();
            }

            var conversationAccess = await _context.TblConversationAccesses
                .FirstOrDefaultAsync(m => m.PkTblConversationAccess == id);
            if (conversationAccess == null)
            {
                return NotFound();
            }

            return View(conversationAccess);
        }

        // GET: ConversationAccesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConversationAccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkTblConversationAccess,FkTblUser,FkTblConversation")] ConversationAccess conversationAccess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversationAccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversationAccess);
        }

        // GET: ConversationAccesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblConversationAccesses == null)
            {
                return NotFound();
            }

            var conversationAccess = await _context.TblConversationAccesses.FindAsync(id);
            if (conversationAccess == null)
            {
                return NotFound();
            }
            return View(conversationAccess);
        }

        // POST: ConversationAccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkTblConversationAccess,FkTblUser,FkTblConversation")] ConversationAccess conversationAccess)
        {
            if (id != conversationAccess.PkTblConversationAccess)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversationAccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationAccessExists(conversationAccess.PkTblConversationAccess))
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
            return View(conversationAccess);
        }

        // GET: ConversationAccesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblConversationAccesses == null)
            {
                return NotFound();
            }

            var conversationAccess = await _context.TblConversationAccesses
                .FirstOrDefaultAsync(m => m.PkTblConversationAccess == id);
            if (conversationAccess == null)
            {
                return NotFound();
            }

            return View(conversationAccess);
        }

        // POST: ConversationAccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblConversationAccesses == null)
            {
                return Problem("Entity set 'MessagingAppContext.TblConversationAccesses'  is null.");
            }
            var conversationAccess = await _context.TblConversationAccesses.FindAsync(id);
            if (conversationAccess != null)
            {
                _context.TblConversationAccesses.Remove(conversationAccess);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationAccessExists(int id)
        {
          return _context.TblConversationAccesses.Any(e => e.PkTblConversationAccess == id);
        }

    }
}
