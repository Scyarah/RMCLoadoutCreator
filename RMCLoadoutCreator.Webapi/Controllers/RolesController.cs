using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMCLoadoutCreator.Definitions;
using RMCLoadoutCreator.Definitions.Models;

namespace RMCLoadoutCreator.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly LoadoutCreatorContext _context;

        public RolesController(LoadoutCreatorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.RMCRoles.ToListAsync();
        }
    }
}