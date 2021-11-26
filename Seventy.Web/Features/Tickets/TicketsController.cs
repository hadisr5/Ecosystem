using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seventy.Service.Tickets;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Features.Tickets
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: Tickets
        [UserAccess(Common.Enums.eAccessControl.TicketsIndex , Common.Enums.eAccessType.None, 1)]
        public IActionResult Index()
        {
            return View(_ticketService.TableNoTracking());
        }

        // GET: Tickets/Details/5
        [UserAccess(Common.Enums.eAccessControl.TicketsDetails, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Details(CancellationToken cancellationToken, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _ticketService.GetByIDAsync(cancellationToken, id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        [UserAccess(Common.Enums.eAccessControl.TicketsCreate, Common.Enums.eAccessType.None, 1)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.TicketsCreate2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Create(CancellationToken cancellationToken, [Bind("UserID,Title,Section,Priority,Status,Actions,ResponderUserID,Response,ID,Description,RegDate")] DomainClass.Core.Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                if (await _ticketService.InsertAsync(tickets, cancellationToken) != null)
                    return RedirectToAction(nameof(Index));
            }
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        [UserAccess(Common.Enums.eAccessControl.TicketsEdit, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Edit(CancellationToken cancellationToken, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _ticketService.GetByIDAsync(cancellationToken, id);
            if (tickets == null)
            {
                return NotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.TicketsEdit2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Edit(CancellationToken cancellationToken, int id, [Bind("UserID,Title,Section,Priority,Status,Actions,ResponderUserID,Response,ID,Description,RegDate")] DomainClass.Core.Tickets tickets)
        {
            if (id != tickets.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketService.UpdateAsync(tickets, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TicketsExists(cancellationToken, (int)tickets.ID))
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
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        [UserAccess(Common.Enums.eAccessControl.TicketsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _ticketService.GetByIDAsync(cancellationToken, id);

            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.TicketsDeleteConfirmed, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> DeleteConfirmed(CancellationToken cancellationToken, int id)
        {
            var tickets = await _ticketService.GetByIDAsync(cancellationToken, id);
            await _ticketService.DeleteAsync(tickets, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        [UserAccess(Common.Enums.eAccessControl.TicketsTicketsExists, Common.Enums.eAccessType.None, 1)]
        private async Task<bool> TicketsExists(CancellationToken cancellationToken, int id)
        {
            var t = await _ticketService.GetByIDAsync(cancellationToken, id);

            return (t != null);
        }
    }
}
