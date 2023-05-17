using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext context;

        public ReviewerRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            context.Add(reviewer);
            return Save();
        }

        public ICollection<Reviewer> GetAllReviewers()
        {
            return context.reviewers.ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return context.reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return context.reviewers.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            context.Update(reviewer);
            return Save();
        }
    }
}
