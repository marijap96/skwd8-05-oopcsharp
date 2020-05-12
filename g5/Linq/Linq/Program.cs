﻿using Models;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = MoviesHelper.GetAllActionMovies();

            // 1. Find ALL movies with Title starting with 'T'

            // 2. Find the FIRST Movie with Title starting with 'G' 

            // 3. Find the LAST Movie with Title starting with 'G'

            // 4. Find ALL movies TITLES that plays for more than 2 hours

            // 5. Find ALL movies TITLES produced before 2010 and ORDER by Title

            // 6. Find ALL movies TITLES produced after 2010, with Rating over 8.5 and ORDER by Title (Descending Order)

            // 7. What is the AVERAGE RANKING of ALL movies

            // 8. Find the BEST RANKING movie (get only the TITLE and RANKING)

            // 9. Find the WORST RANKING movie (get only the TITLE and DURATION)

            // 10. Get FIRST five movies

            // 11. Get LAST five movies
        }
    }
}
