using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Lab2Context _context;

        public TicketsController(Lab2Context context)
        {
            _context = context;
        }

        // -------------------------------------------------------------------
        // CRUD: READ (INDEX/SEARCH/FILTER)
        // -------------------------------------------------------------------
        public async Task<IActionResult> Index(int? selectedCategoryId, string? searchString)
        {
            var categories = from c in _context.Category orderby c.Name select c;
            var tickets = _context.Ticket
                                 .Include(t => t.Category) // Lấy thông tin Category
                                 .Include(t => t.Customer) // Lấy thông tin Customer
                                 .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Tìm kiếm theo Subject (Chủ đề)
                tickets = tickets.Where(t => t.Subject.Contains(searchString) ||
                                             t.Description.Contains(searchString));
            }
            if (selectedCategoryId.HasValue)
            {
                // Lọc theo CategoryId
                tickets = tickets.Where(t => t.CategoryId == selectedCategoryId.Value);
            }

            var ticketVM = new TicketViewModel
            {
                Tickets = await tickets.ToListAsync(),
                Categories = new SelectList(await categories.ToListAsync(), "Id", "Name"),
                SelectedCategoryId = selectedCategoryId,
                SearchString = searchString
            };

            return View(ticketVM);
        }

        // -------------------------------------------------------------------
        // CRUD: READ (DETAILS)
        // -------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Category)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // -------------------------------------------------------------------
        // CRUD: CREATE
        // -------------------------------------------------------------------
        public IActionResult Create()
        {
            // Sử dụng "Name" cho Customer thay vì "Email" để thân thiện hơn (Tùy chọn)
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,Status,CustomerId,CategoryId")] Ticket ticket)
        {
            // Bỏ CreatedDate khỏi Bind vì ta set thủ công hoặc trong Model

            if (ModelState.IsValid)
            {
                ticket.CreatedDate = DateTime.Now; // Đảm bảo ngày được thiết lập khi tạo
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, load lại SelectList với giá trị hiện tại
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", ticket.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", ticket.CustomerId);
            return View(ticket);
        }

        // -------------------------------------------------------------------
        // CRUD: UPDATE
        // -------------------------------------------------------------------
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Load SelectList với giá trị hiện tại được chọn
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", ticket.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", ticket.CustomerId);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Description,CreatedDate,Status,CustomerId,CategoryId")] Ticket ticket)
        {
            // Chú ý: CreatedDate được đưa vào Bind để đảm bảo nó không bị mất khi Update

            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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

            // Nếu ModelState không hợp lệ, load lại SelectList với giá trị hiện tại
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", ticket.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", ticket.CustomerId);
            return View(ticket);
        }

        // -------------------------------------------------------------------
        // CRUD: DELETE
        // -------------------------------------------------------------------
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Category)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}