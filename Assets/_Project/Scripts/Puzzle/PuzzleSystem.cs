using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class PuzzleSystem
    {
        public event Action<PuzzleStep> OnPuzzleStepCompleted;

        public static PuzzleSystem Instance { get; private set; }

        private static Dictionary<PuzzleReference, PuzzleStep> puzzleSteps;

        static PuzzleSystem()
        {
            Instance ??= new PuzzleSystem();
        }

        private PuzzleSystem()
        {
            puzzleSteps = new();
            foreach (var puzzleRef in Resources.LoadAll<PuzzleReference>("PuzzleSteps"))
            {
                puzzleSteps.Add(puzzleRef, new PuzzleStep(puzzleRef));
            }
        }

        public void MarkPuzzleAsComplete(PuzzleReference puzzle)
        {
            var puzzleStep = puzzleSteps[puzzle];
            if (puzzleStep != null)
            {
                puzzleStep.IsCompleted = true;
                OnPuzzleStepCompleted?.Invoke(puzzleStep);
            }
        }

        public bool IsPuzzleComplete(PuzzleReference puzzle)
        {
            var puzzleStep = puzzleSteps[puzzle];
            if (puzzleStep == null)
            {
                return false;
            }

            return puzzleStep.IsCompleted;
        } 
    }
}
