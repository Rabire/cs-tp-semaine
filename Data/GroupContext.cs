using Microsoft.EntityFrameworkCore;
using CsApi.Models;

namespace CsApi.Data
{
    public class GroupContext : DbContext
    {
        public GroupContext(DbContextOptions<GroupContext> options) : base(options)
        {
        }

        public DbSet<Group> Group { get; set; }
    }
}