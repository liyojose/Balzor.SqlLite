using DemoApp.Server.Data;
using DemoApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsersDataController : ControllerBase
    {
        private readonly ILogger<UsersDataController> _logger;
        UserDbContext _db;
        public UsersDataController(ILogger<UsersDataController> logger, UserDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _db.Users.ToListAsync();
        }
        [HttpPost]
        public async Task<OkResult> Post(User user)
        {
            user.LastUpdated = DateTime.Now;
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<User>> ChangedUsers([FromQuery] DateTime since)
        {
            return _db.Users.Where(v => v.LastUpdated >= since);
        }

        [HttpPut]
        public async Task<IActionResult> Details(User User)
        {
           
            return Ok();
        }
    }
}
