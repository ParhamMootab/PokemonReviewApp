using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext context;

        public ReviewRepository(DataContext context)
        {
            this.context = context;
        }

        public Review GetReview(int id)
        {
            return context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetAllReviews()
        {
            return context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExist(int id)
        {
            return context.Reviews.Any(r => r.Id == id);
        }

        public bool CreateReview(Review review)
        {
            context.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            context.Update(review);
            return Save();
        }
    }
}
