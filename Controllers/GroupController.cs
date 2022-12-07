using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsApi.Data;
using CsApi.Models;

namespace CsApi.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupContext _context;

        public GroupsController(GroupContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            return await _context.Group.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var Group = await _context.Group.FindAsync(id);

            if (Group == null)
            {
                return NotFound();
            }

            return Group;
        }
    }
}
