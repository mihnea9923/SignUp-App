using API.Data;
using API.Model;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context context;

        public UsersController(Context context)
        {
            this.context = context;
        }
        [HttpPost]
        public IActionResult Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            PublishMessageService.PublishMessage(user.Email);
            return Ok(new { data= "success" });
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.AsEnumerable();
        }
    }
}

