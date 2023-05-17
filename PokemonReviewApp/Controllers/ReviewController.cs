using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IPokemonRepository pokemonRepository;
        private readonly IReviewerRepository reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, 
            IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.pokemonRepository = pokemonRepository;
            this.reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetAllReviews()
        {
            var reviews = mapper.Map<List<ReviewDTO>>(reviewRepository.GetAllReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!reviewRepository.ReviewExist(reviewId))
                return NotFound();
            var review = mapper.Map<ReviewDTO>(reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("review/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAPokemon(int pokemonId)
        {
            var reviews = mapper.Map<List<ReviewDTO>>(reviewRepository.GetReviewsOfAPokemon(pokemonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int pokemonId, [FromQuery] int reviewerId, 
            [FromBody] ReviewDTO reviewCreate)
        {
            if (reviewCreate == null)
                BadRequest(ModelState);

            var reviews = reviewRepository.GetAllReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = mapper.Map<Review>(reviewCreate);

            // To be reviewed: Tutorial put the object creation in repository for Pokemon
            reviewMap.Pokemon = pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = reviewerRepository.GetReviewer(reviewerId);



            if (!reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDTO reviewUpdate)
        {
            if (reviewUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (reviewId != reviewUpdate.Id)
                return BadRequest(ModelState);

            if (!reviewRepository.ReviewExist(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = mapper.Map<Review>(reviewUpdate);

            if (!reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
