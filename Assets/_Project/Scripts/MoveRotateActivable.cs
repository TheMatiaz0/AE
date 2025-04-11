using DG.Tweening;
using UnityEngine;

namespace AE
{
    public class MoveRotateActivable : MonoBehaviour, IActivable
    {
        [SerializeField]
        private Transform element;

        [Header("Start Settings")]
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 startRotation;

        [Header("Destination Settings")]
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float endDuration;
        [SerializeField] private Ease endEase;

        private void Awake()
        {
            Deactivate();
        }

        public void Activate()
        {
            element.DOLocalMove(endPosition, endDuration)
                .SetEase(endEase);
            element.DOLocalRotate(endRotation, endDuration)
                .SetEase(endEase);
        }

        public void Deactivate()
        {
            element.localPosition = startPosition;
            element.localEulerAngles = startRotation;
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (element == null)
            {
                element = this.transform;
            }
        }

#endif
    }
}
