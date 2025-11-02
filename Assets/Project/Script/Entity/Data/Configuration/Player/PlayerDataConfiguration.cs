using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "PlayerDataConfiguration", menuName = "DataConfiguration/Player")]
    public class PlayerDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Transform")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float movementSpeed;

        [SerializeField]
        [BoxGroup("Loadout")]
        private List<LoadoutItem> towerLoadout = new();

        [SerializeField]
        [BoxGroup("TowerPlacement")]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private int limit;

        public float MovementSpeed => movementSpeed;

        public IReadOnlyList<LoadoutItem> TowerLoadout
        {
            get
            {
                if (towerLoadout.Count > Constant.towerLoadoutSize)
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        "Player Loadout cannot have more than 5 towers. Redundant towers have been ignored.",
                        this);

                    towerLoadout = towerLoadout.Take(Constant.towerLoadoutSize).ToList();
                }
                else if (towerLoadout.Count < Constant.towerLoadoutSize)
                {
                    for (int i = 0; i < Constant.towerLoadoutSize - towerLoadout.Count; i++)
                    {
                        var loadoutItem = new LoadoutItem();

                        towerLoadout.Add(loadoutItem);
                    }
                }

                return towerLoadout.ToList();
            }
        }

        public int Limit => limit;
    }
}
