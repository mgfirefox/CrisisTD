using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class Tracer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        
        private float elapsedTime;

        public Vector3 StartPosition
        {
            set => lineRenderer.SetPosition(0, value);
        }

        public Vector3 EndPosition
        {
            set => lineRenderer.SetPosition(1, value);
        }

        public void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        public void Update()
        {
            if (!lineRenderer.enabled)
            {
                return;
            }
            
            if (elapsedTime < 0.5f)
            {
                elapsedTime += Time.deltaTime;

                return;
            }

            Destroy(gameObject);
        }

        public void Enable()
        {
            lineRenderer.enabled = true;
        }
    }
}
