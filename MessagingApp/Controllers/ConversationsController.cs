using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessagingApp.Models;
using RestSharp;

namespace MessagingApp.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly MessagingAppContext _context;

        public ConversationsController(MessagingAppContext context)
        {
            _context = context;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
              return View(await _context.TblConversations.ToListAsync());
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblConversations == null)
            {
                return NotFound();
            }

            var conversation = await _context.TblConversations
                .FirstOrDefaultAsync(m => m.PkTblConversation == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkTblConversation,TblConversationHeader,FkTblCreator")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblConversations == null)
            {
                return NotFound();
            }

            var conversation = await _context.TblConversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkTblConversation,TblConversationHeader,FkTblCreator")] Conversation conversation)
        {
            if (id != conversation.PkTblConversation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.PkTblConversation))
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
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblConversations == null)
            {
                return NotFound();
            }

            var conversation = await _context.TblConversations
                .FirstOrDefaultAsync(m => m.PkTblConversation == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblConversations == null)
            {
                return Problem("Entity set 'MessagingAppContext.TblConversations'  is null.");
            }
            var conversation = await _context.TblConversations.FindAsync(id);
            if (conversation != null)
            {
                _context.TblConversations.Remove(conversation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
          return _context.TblConversations.Any(e => e.PkTblConversation == id);
        }

        public async Task<IActionResult> JoinConversation(int? id,string? ConversationName)
        {
            HttpContext.Response.Cookies.Append("ConversationID", id.ToString());
            HttpContext.Response.Cookies.Append("ConversationName", ConversationName);

            return RedirectToAction("Index", "Home");

            //return View("Details");
        }

    }
}
