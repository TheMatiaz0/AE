using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AE
{
    public class BannerBloodTransformation : MonoBehaviour
    {
        [Header("Banner Settings")]
        [SerializeField] private GameObject bannerObject;
        [SerializeField] private Collider bannerCollider;
        [SerializeField] private Transform finalPosition;
        [SerializeField] private float dragDownDuration = 2f;
        [SerializeField] private float rotationDuration = 2f;
        [SerializeField] private Ease dragEase;
        [SerializeField] private Ease rotationEase;

        [Header("Blood Decal Settings")]
        [SerializeField] private DecalProjector bloodDecal;
        [SerializeField] private float flowDuration = 10f;
        [SerializeField] private Ease flowEase;

        private bool transformationTriggered = false;
        private Sequence transformationSequence;

        private float bloodScaleX;
        private float bloodScaleZ;

        private void Start()
        {
            bloodScaleX = bloodDecal.size.x;
            bloodScaleZ = bloodDecal.size.z;

            bloodDecal.size = new Vector3(0, bloodDecal.size.y, 0);

            TriggerBannerTransformation();
        }

        private void OnDestroy()
        {
            if (transformationSequence != null && transformationSequence.IsActive())
            {
                transformationSequence.Kill();
            }
        }

        private void TriggerBannerTransformation()
        {
            if (!transformationTriggered)
            {
                transformationTriggered = true;
                TransformBannerToBlood();
            }
        }

        private void TransformBannerToBlood()
        {
            bannerCollider.enabled = false;

            transformationSequence = DOTween.Sequence();

            transformationSequence.Append(bannerObject.transform.DOMove(finalPosition.position, dragDownDuration)
                .SetEase(dragEase));

            transformationSequence.Join(bannerObject.transform.DORotate(finalPosition.eulerAngles, rotationDuration)
                .SetEase(rotationEase));

            transformationSequence.Append(AnimateBloodFlow());

            transformationSequence.Join(bannerObject.transform.DOScaleY(0, flowDuration / 2))
                .OnComplete(() => bannerObject.SetActive(false));
        }

        private Sequence AnimateBloodFlow()
        {
            var flowSequence = DOTween.Sequence();

            flowSequence.Join(DOTween.To(
                () => bloodDecal.size,
                x => bloodDecal.size = x,
                new Vector3(bloodScaleX, bloodDecal.size.y, bloodScaleZ),
                flowDuration)
                .SetEase(flowEase));

            return flowSequence;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (bloodDecal == null)
            {
                bloodDecal = this.GetComponent<DecalProjector>();
            }
        }

#endif
    }
}
