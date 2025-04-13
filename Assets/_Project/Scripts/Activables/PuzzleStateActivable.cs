using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class PuzzleStateActivable : IActivable
    {
        [Header("Activate - Mark Puzzle As Complete")]
        [SerializeField]
        private PuzzleReference puzzleReference;

        public void Activate(IContext context)
        {
            PuzzleSystem.Instance.MarkPuzzleAsComplete(puzzleReference);
        }

        public void Deactivate(IContext context)
        {
        }
    }
}
