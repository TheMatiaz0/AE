using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class PlaySoundActivable : IActivable
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clip;
        [SerializeField] private bool oneShot = true;
        [SerializeField] private bool loop = false;

        public void Activate(IContext context)
        {
            if (oneShot)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                source.clip = clip;
                source.loop = loop;
                source.Play();
            }
        }

        public void Deactivate(IContext context)
        {
            source.Stop();
        }
    }
}
