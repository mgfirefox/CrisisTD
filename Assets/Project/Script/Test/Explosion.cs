using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class Explosion : MonoBehaviour
    {
        private new ParticleSystem particleSystem;
        private AudioSource audioSource;

        private new bool enabled;
        
        public Vector3 Position { set => transform.position = value; }
        
        public float Radius { set => transform.localScale = new Vector3(value, value, value); }

        public void Awake()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
            audioSource = GetComponent<AudioSource>();
        }

        public void Update()
        {
            if (!enabled)
            {
                return;
            }

            if (particleSystem.IsAlive())
            {
                return;
            }
            
            Destroy(gameObject);
        }

        public void Enable()
        {
            particleSystem.Play();
            audioSource.PlayDelayed(0.2f);
            
            enabled = true;
        }
    }
}
