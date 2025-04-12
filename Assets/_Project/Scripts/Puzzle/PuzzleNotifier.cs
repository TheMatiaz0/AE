using UnityEngine;

namespace AE
{
    public class PuzzleNotifier : MonoBehaviour
    {
        [SerializeField] private PuzzleReference puzzle;

        private IInteractable interactable;

        private void Awake()
        {
            interactable = GetComponent<IInteractable>();
            interactable.OnUpdate += OnUpdate;
            interactable.OnComplete += OnComplete;
        }

        private void OnDestroy()
        {
            interactable.OnUpdate -= OnUpdate;
            interactable.OnComplete -= OnComplete;
        }

        private void OnUpdate()
        {
        }

        private void OnComplete()
        {
            PuzzleSystem.Instance.MarkPuzzleAsComplete(puzzle);
        }
    }
}
