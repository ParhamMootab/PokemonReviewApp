using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext context;
        public CategoryRepository(DataContext context)
        {
            this.context = context;


        }
        public bool CategoryExists(int id)
        {
            return context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return context.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }
    }
}
