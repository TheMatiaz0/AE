using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AE
{
    [Serializable]
    public class LoadSceneActivable : IActivable
    {
        [SerializeField]
        private string nextSceneName;

        public void Activate(IContext context)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        public void Deactivate(IContext context)
        {
        }
    }
}
