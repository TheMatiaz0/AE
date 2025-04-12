using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class SpecificPuzzleCondition : ICondition
    {
        [SerializeField] private PuzzleReference requiredPuzzle;

        public bool IsConditionMet(IContext context)
        {
            if (context is IPuzzleContext puzzle)
            {
                return puzzle.PuzzleSystem.IsPuzzleComplete(requiredPuzzle);
            }

            return false;
        }
    }
}
