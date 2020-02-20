using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoshenJimenez.WhereNaYou.API.Infrastructure.Domain.Data;
using GoshenJimenez.WhereNaYou.API.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoshenJimenez.WhereNaYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly DefaultDbContext _context;

        public GroupsController(DefaultDbContext context)
        {
            _context = context;
        }

       //get all
       [HttpGet("/api/groups")]
        [HttpGet("/api/groups/search")]
        public IActionResult Index()
        {
            return Ok("OK");
        }

        [HttpGet("/api/groups/{id?}")]
        public IActionResult GetGroup(Guid? id)
        {

            var group = this._context.Groups.FirstOrDefault(g => g.Id == id);

            if (group != null)
            {
                return Ok(group);
            }

            return BadRequest();
        }
    }
}