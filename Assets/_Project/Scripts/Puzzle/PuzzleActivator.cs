using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class PuzzleActivator : MonoBehaviour
    {
        [SerializeField] private List<PuzzleReference> requiredPuzzles;
        [SerializeReference, SubclassSelector] private IActivable result;

        private readonly HashSet<PuzzleReference> completedPuzzles = new();
        private PuzzleContext context;

        private void Awake()
        {
            context = new PuzzleContext(PuzzleSystem.Instance);
            PuzzleSystem.Instance.OnPuzzleStepCompleted += OnPuzzleCompleted;
        }

        private void OnPuzzleCompleted(PuzzleStep step)
        {
            if (!requiredPuzzles.Contains(step.Reference) || completedPuzzles.Contains(step.Reference))
            {
                return;
            }

            completedPuzzles.Add(step.Reference);

            if (completedPuzzles.Count == requiredPuzzles.Count)
            {
                result?.Activate(context);
            }
        }

        private void OnDestroy()
        {
            PuzzleSystem.Instance.OnPuzzleStepCompleted -= OnPuzzleCompleted;
        }
    }
}
