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
        public async Task<List<Localuser>> Get()
        {
            var dbuser =  await _db.Users.ToListAsync();
            var localuser = dbuser.Select(e => new Localuser
            {
                Id = e.Id,
                Firstname = e.Firstname,
                Lastname = e.Lastname,
                Email = e.Email,
                LastUpdated = e.LastUpdated,
                IsDeleted = false,
                Updated = false
            }).ToList();
            return localuser;
        }
        [HttpPost]
        public async Task<OkResult> Post(Localuser user)
        {
            var dbuser = new User();
            dbuser.Firstname = user.Firstname;
            dbuser.Lastname = user.Lastname;
            dbuser.Email = user.Email;
            dbuser.LastUpdated = DateTime.Now;
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<User>> ChangedUsers([FromQuery] DateTime since)
        {
            var dbuser = _db.Users.Where(v => v.LastUpdated >= since);
            var localuser = dbuser.Select(e => new Localuser
            {
                Id = e.Id,
                Firstname = e.Firstname,
                Lastname = e.Lastname,
                Email = e.Email,
                LastUpdated = e.LastUpdated,
                IsDeleted = false,
                Updated = false
            }).ToList();
            return localuser;
        }

        [HttpPut]
        public async Task<IActionResult> Details(User User)
        {
           
            return Ok();
        }
    }
}
