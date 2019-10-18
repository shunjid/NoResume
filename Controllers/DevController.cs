using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoResume.Models;

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
            var developerId = _userManager.FindByNameAsync(username).Result.Id;
            
            var dev = new Dev
            {
                ShortBio = await _context.ShortBios.FindAsync(developerId),
                WorkingProfile = await _context.WorkingProfiles.FindAsync(developerId)
            };
            
            return View(dev);
        }
    }
}