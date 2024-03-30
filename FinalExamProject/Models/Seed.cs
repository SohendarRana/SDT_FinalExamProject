
using FinalExamProject.Models;
using FinalExamProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FinalExamProject.Models
{
    public class Seed
    {
        public static void Intialize(IServiceProvider serviceProvider)
        {
            using( var context = new FinalExamProjectContext(
                serviceProvider.GetRequiredService <
                    DbContextOptions<FinalExamProjectContext>>()))
            { 
                if (context.Reviews.Any())
                {
                    return;
                }
                context.Reviews.AddRange(
                    new Reviews
                    {
                        Restaurant = "adwadwa",
                        Food = "adwad",
                        prices = 3848,
                        Date = DateTime.Now,
                        Image = File.ReadAllBytes("wwwroot/Images/th.png")

                    }
                    );
                context.SaveChanges();
            }
        }


    }
}
