namespace FunApp.Services.Implementations
{
    using Data;
    using Interfaces;
    using Models.Joke;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JokeService : IJokeService
    {
        private readonly FunAppDbContext db;

        public JokeService(FunAppDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<BasicJokeModel>> All()
        => await this.db
                .Jokes
                .Select(j => new BasicJokeModel
                {
                    Id = j.Id,
                    Name = j.Name,
                    Description = j.Description,
                    Likes = j.Likes
                })
                .ToListAsync();
    }
}
