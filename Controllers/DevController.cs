using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoResume.Models;
using System.Linq;

namespace NoResume.Controllers
{
    public class DevController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DevController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Dev/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            try
            {
                var developerId = _userManager.FindByNameAsync(username).Result.Id;  
                
                var dev = new Dev
                {
                    ShortBio = await _context.ShortBios.FindAsync(developerId),
                    WorkingProfile = await _context.WorkingProfiles.FindAsync(developerId)
                };
            
                var subscriptions = _context.Subscriptions.FirstOrDefault(s => s.DevId == developerId);
                if(subscriptions == null)
                {
                    return View("NotFound404");
                }
                else
                {
                    return View(dev);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult NotFound404()
        {
            return View();
        }


    }
}