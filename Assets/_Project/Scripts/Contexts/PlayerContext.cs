using UnityEngine;

namespace AE
{
    public class PlayerContext : MonoBehaviour, IPlayerContext
    {
        [SerializeField]
        private InteractionController interactionController;

        public InteractionController InteractionController => interactionController;
    }
}
