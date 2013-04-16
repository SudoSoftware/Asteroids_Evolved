using System;

namespace AsteroidsEvolved
{
#if WINDOWS || XBOX
    static class AsteroidsEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (AsteroidsGame game = new AsteroidsGame())
            {
                game.Run();
            }
        }
    }
#endif
}

