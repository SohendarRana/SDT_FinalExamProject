using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalExamProject.Data;
using FinalExamProject.Models;
using System.Net.WebSockets;

namespace FinalExamProject.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly FinalExamProjectContext _context;

        public ReviewsController(FinalExamProjectContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(string searchString)
        { 
            if (_context.Reviews == null)
            {
                return Problem("Entity ser 'MvcMovieContext.Movie' is null.");
            }

            var movies = from m in _context.Reviews
                         select m;

            if (!System.String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s=> s.Restaurant!.Contains(searchString));
            }

            return View( await movies.ToListAsync()); 
        }

        [HttpPost]
        public string Index(string searchString, bool notused)
        {
            return "From [httpPost]Index: filter on" + searchString;
        }


        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(ReviewViewModel review)
        {
            if (review.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await review.Image.CopyToAsync(memoryStream);

                    Reviews MainReview = new Reviews()
                    {
                        ID = review.id,
                        Restaurant = review.Restaurant,
                        Food = review.Food,
                        prices = review.prices,
                        Date = review.Date,
                        Image = memoryStream.ToArray()

                    };
                    _context.Add(MainReview);
                    await _context.SaveChangesAsync();

                    ViewBag.success = "record added";
                    return RedirectToAction(nameof(Index));
   
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            var book = await _context.Reviews.FindAsync(id);
            if (book.Image != null)
            {
                return File(book.Image, "image/jpeg");
            }
            return NotFound();
        }

        // GET: Reviews/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            var viewModel = new ReviewViewModel
            {
                id = review.ID,
                Restaurant = review.Restaurant,
                Food = review.Food,
                prices = review.prices,
                Date = review.Date,
            };
            return View(viewModel); 
            {   
            }
        }
        

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ReviewViewModel review)
        {
            if (review.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await review.Image.CopyToAsync(memoryStream);

                    Reviews MainReview = new Reviews()
                    {
                     ID= review.id,
                     Restaurant = review.Restaurant,
                     Food = review.Food,
                     prices=review.prices,
                     Date = review.Date,
                     Image = memoryStream.ToArray()
                    };
                    _context.Update(MainReview);
                    await _context.SaveChangesAsync();

                    ViewBag.sucess = "record added";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
       

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews != null)
            {
                _context.Reviews.Remove(reviews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
    }
}
