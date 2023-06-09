﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetAllReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int id);

        bool CreateReviewer(Reviewer reviewer);
        bool Save();
        bool UpdateReviewer(Reviewer reviewer);
    }
}
