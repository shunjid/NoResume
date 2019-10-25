using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoResume.Models;

namespace NoResume.Controllers
{
    public class ShortBiosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShortBiosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        private string _getCurrentlyLoggedInUser()
        {
            return _userManager.GetUserId(HttpContext.User);
        }

        public JsonResult createOTP(string phonenumber)
        {
            return Json(phonenumber);
        }
        
        // GET: ShortBios/Edit/5
        [Authorize]
        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id !=_getCurrentlyLoggedInUser())
            {
                // Route user to authorized page if request id is invalid
                id = _getCurrentlyLoggedInUser();
            }
            
            var shortBio = await _context.ShortBios.FindAsync(id);
            var subscription = await _context.Subscriptions.FindAsync(id);
            
            TextInfo caseTitle = new CultureInfo("en-US",false).TextInfo;
            ViewBag.loggedInUserName = caseTitle.ToTitleCase(_userManager.GetUserName(HttpContext.User));
            ViewBag.loggedInUserId = _userManager.GetUserId(HttpContext.User);
            if (subscription != null)
            {
                ViewBag.subscriptionStatus = "Yes";
            }
            else
            {
                ViewBag.subscriptionStatus = "No";
            }
            
            
            if (shortBio == null)
            {
                return NotFound();
            }

            return View(shortBio);
        }

        // POST: ShortBios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(string id, [Bind("DeveloperId,DeveloperName,DeveloperPhoneNumber,ShortDescription,CurrentCity,IsAvailableForJob")] ShortBio shortBio)
        {
            if (id != shortBio.DeveloperId)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortBio);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortBioExists(shortBio.DeveloperId))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(shortBio);
            }
            return null;
        }
        
        private bool ShortBioExists(string id)
        {
            return _context.ShortBios.Any(e => e.DeveloperId == id);
        }
    }
}
