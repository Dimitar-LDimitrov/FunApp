namespace FunApp.Data.Models
{
    public class UserJokes
    {
        public int JokeId { get; set; }
        public Joke Joke { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
