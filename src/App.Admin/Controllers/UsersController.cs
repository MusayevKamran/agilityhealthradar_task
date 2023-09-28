using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Models;
using App.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        
        public UsersController(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        // GET: UsersViewModel
        public async Task<IActionResult> Index()
        {
            return View(await Task.FromResult(_userService.Select(x => x)));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _userService == null)
            {
                return NotFound();
            }

            var contacts = await _userService.FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.InsertAsync(user);

                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _userService == null)
            {
                return NotFound();
            }

            var user = await _userService.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Firstname,Lastname,Email,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.Id = id;
                    _userService.Update(user);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _userService == null)
            {
                return NotFound();
            }

            var user = await _userService.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_userService == null)
            {
                return Problem("Entity set 'AppDbContext.Contacts'  is null.");
            }

            var contacts = await _userService.FirstOrDefaultAsync(x => x.Id == id);
            if (contacts != null)
            {
                _userService.Delete(contacts);
            }

            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int? id)
        {
            return (_userService?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
