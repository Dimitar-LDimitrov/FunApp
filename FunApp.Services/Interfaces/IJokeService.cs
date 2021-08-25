namespace FunApp.Services.Interfaces
{
    using Models.Joke;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJokeService
    {
        Task<ICollection<BasicJokeModel>> All();
    }
}
