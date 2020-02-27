using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoshenJimenez.WhereNaYou.API.Infrastructure.Domain.Data;
using GoshenJimenez.WhereNaYou.API.Infrastructure.Domain.Models;
using GoshenJimenez.WhereNaYou.DataTransferObjects.Groups;
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
        public IActionResult Index(string keyword = "", int pageIndex = 1, int pageSize = 3)
        {
            Page<Group> result = new Page<Group>();
            if (pageSize < 1)
            {
                pageSize = 1;
            }

            IQueryable<Group> groupQuery = (IQueryable<Group>)this._context.Groups;

            if (string.IsNullOrEmpty(keyword) == false)
            {
                groupQuery = groupQuery.Where(g => g.Name.Contains(keyword));
            }

            long queryCount = groupQuery.Count();

            int pageCount = (int)Math.Ceiling((decimal)(queryCount / pageSize));
            long mod = (queryCount % pageSize);

            if (mod > 0)
            {
                pageCount = pageCount + 1;
            }

            int skip = (int)(pageSize * (pageIndex - 1));
            List<Group> groups = groupQuery.ToList();

            result.Items = groups.Skip(skip).Take((int)pageSize).ToList();
            result.PageCount = pageCount;
            result.PageSize = pageSize;
            result.QueryCount = queryCount;
            result.PageIndex = pageIndex;


            return Ok(result);
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