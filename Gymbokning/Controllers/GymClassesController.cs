using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gymbokning.Data;
using Gymbokning.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Gymbokning.Models.ViewModels;

namespace Gymbokning.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
       

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

       
        public async Task<IActionResult> BookingToogle(int? id,ApplicationUserGymclass applicationUserGymclass)
        {
            if (id == null)
                return NotFound();
           // string userId = userManager.GetUserId(User);
            //if (userId == null)
            if(!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Identity/Account/Login");
            }

            string userId = userManager.GetUserId(User);

            var model = await _context.ApplicationUserGymclasses.Where(g => g.GymClassId == id && g.ApplicationUserId == userId).FirstOrDefaultAsync();
           
            if (model!=null)
            {
                _context.Remove(model);
            }
            else
            {
                applicationUserGymclass.ApplicationUserId = userId;
                applicationUserGymclass.GymClassId = (int)id;
                _context.Add(applicationUserGymclass);
               
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           

        }

        // GET: GymClasses
        //public async Task<IActionResult> Index()
        //{
           
        //    return View(await _context.GymClasses.ToListAsync());
        //}

        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            string userId;

            if (!User.Identity.IsAuthenticated)
            {
                
                var model = await _context.GymClasses.Include(m => m.AttendingMember)
                                           .Select(g => new NewIndexViewModel
                                           {
                                               Id = g.Id,
                                               Name = g.Name,
                                               StartTime = g.StartTime,
                                               Duration = g.Duration,
                                               Description = g.Description,

                                           }).ToListAsync();
                var modell = new IndexViewModel
                {
                    GymClasses=model,
                    ShowHistory=false
                };
                return View(modell);
            }

                userId = userManager.GetUserId(User);

                var ischecked = vm.ShowHistory;
                
               
                if (ischecked)
                {
                    var model2 = await _context.GymClasses.Include(m => m.AttendingMember)
                                          .IgnoreQueryFilters()
                                          .Select(g => new NewIndexViewModel
                                          {
                                              Id = g.Id,
                                              Name = g.Name,
                                              StartTime = g.StartTime,
                                              Duration = g.Duration,
                                              Description = g.Description,
                                              UserBookGym = g.AttendingMember.Any(k => k.ApplicationUserId == userId)
                                          }).ToListAsync();
                    var modell2 = new IndexViewModel
                    {
                        GymClasses = model2,
                        ShowHistory = vm.ShowHistory
                    };

                    return View(modell2);
                }

            var model1 = await _context.GymClasses.Include(m => m.AttendingMember)
                                     .Select(g => new NewIndexViewModel
                                     {
                                         Id = g.Id,
                                         Name = g.Name,
                                         StartTime = g.StartTime,
                                         Duration = g.Duration,
                                         Description = g.Description,
                                         UserBookGym = g.AttendingMember.Any(k => k.ApplicationUserId == userId)
                                     }).ToListAsync();

            var modell1 = new IndexViewModel
            {
                GymClasses = model1,
                ShowHistory = vm.ShowHistory
            };
            return View(modell1);




        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var userId = userManager.GetUserId(User);
            var model = await _context.GymClasses.IgnoreQueryFilters().Include(g => g.AttendingMember).Where(g => g.StartTime < DateTime.Now ).ToListAsync();
            model =model.Where(g => g.AttendingMember.Any(m => m.ApplicationUserId == userId)).ToList();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> BookedGymClasses()
        {
            var userId = userManager.GetUserId(User);
            var model = await _context.GymClasses.IgnoreQueryFilters().Include(g => g.AttendingMember).Where(g => g.StartTime > DateTime.Now).ToListAsync();
            model = model.Where(g => g.AttendingMember.Any(m => m.ApplicationUserId == userId)).ToList();
            return View(model);
        }
        [Authorize]
        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.Include(g=>g.AttendingMember).ThenInclude(g=>g.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }
        [Authorize (Roles ="Admin")]
        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            _context.GymClasses.Remove(gymClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return _context.GymClasses.Any(e => e.Id == id);
        }
    }
}
