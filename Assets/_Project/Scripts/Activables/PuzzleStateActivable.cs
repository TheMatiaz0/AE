using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class PuzzleStateActivable : IActivable
    {
        [SerializeField]
        private PuzzleReference puzzleReference;

        public void Activate(IContext context)
        {
            if (context is IPuzzleContext puzzleContext)
            {
                puzzleContext.PuzzleSystem.MarkPuzzleAsComplete(puzzleReference);
            }
        }

        public void Deactivate(IContext context)
        {
        }
    }
}
