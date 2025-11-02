using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class LayerMaskUtility
    {
        public static LayerMask GetCollisionLayerMask(int collisionLayer)
        {
            var mask = new LayerMask();
            for (int layer = 0; layer < 32; layer++)
            {
                if (!Physics.GetIgnoreLayerCollision(collisionLayer, layer))
                {
                    mask = mask.Add(layer);
                }
            }

            return mask;
        }
    }
}
