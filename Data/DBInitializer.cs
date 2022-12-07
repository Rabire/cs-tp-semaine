using CsApi.Models;

namespace CsApi.Data
{
    public class DBInitializer
    {
        public static void Initialize(GroupContext context)
        {
            context.Database.EnsureCreated();

            if (context.Group.Any())
            {
                // DB already seeded
                return;
            }

            var groups = new Group[]{
                new Group{Id=1, Name="B3 D", Year=2021},
                new Group{Id=1, Name="B3 R", Year=2021},
                new Group{Id=1, Name="ESI4 D", Year=2022},
            };

            // seeding
            foreach (Group group in groups)
            {
                context.Group.Add(group);
            }
            context.SaveChanges();
        }
    }
}