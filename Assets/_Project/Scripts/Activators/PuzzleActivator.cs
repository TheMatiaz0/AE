using UnityEngine;

namespace AE
{
    public class PuzzleActivator : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICondition conditions;
        [SerializeReference, SubclassSelector] private IActivable result;

        private PuzzleContext context;

        private void Awake()
        {
            context = new PuzzleContext(PuzzleSystem.Instance);
            PuzzleSystem.Instance.OnPuzzleStepCompleted += OnPuzzleCompleted;
        }

        private void OnPuzzleCompleted(PuzzleStep step)
        {
            if (conditions != null && !conditions.IsConditionMet(context))
            {
                return;
            }

            result?.Activate(context);
            PuzzleSystem.Instance.OnPuzzleStepCompleted -= OnPuzzleCompleted;
        }

        private void OnDestroy()
        {
            PuzzleSystem.Instance.OnPuzzleStepCompleted -= OnPuzzleCompleted;
        }
    }
}
