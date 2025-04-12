using UnityEngine;

namespace AE
{
    public class PuzzleContext : IPuzzleContext
    {    
        public PuzzleSystem PuzzleSystem { get; }

        public PuzzleContext(PuzzleSystem puzzleSystem)
        {
            PuzzleSystem = puzzleSystem;
        }
    }
}
