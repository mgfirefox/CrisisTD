using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementActionUi : AbstractActionUi, ITowerPlacementActionUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button firstTowerButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button secondTowerButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button thirdTowerButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button fourthTowerButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button fifthTowerButton;
        private readonly IDictionary<Button, UnityAction> towerButtonActions =
            new Dictionary<Button, UnityAction>();

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI countText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI limitText;

        public event Action<int> TowerButtonClicked;

        public int Count
        {
            set
            {
                if (IsDestroyed)
                {
                    return;
                }

                countText.text = value.ToString();
            }
        }
        public int Limit
        {
            set
            {
                if (IsDestroyed)
                {
                    return;
                }

                limitText.text = value.ToString();
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            AddTowerButtonListener(firstTowerButton, 0);
            AddTowerButtonListener(secondTowerButton, 1);
            AddTowerButtonListener(thirdTowerButton, 2);
            AddTowerButtonListener(fourthTowerButton, 3);
            AddTowerButtonListener(fifthTowerButton, 4);
        }

        private void AddTowerButtonListener(Button button, int index)
        {
            UnityAction buttonAction = () => OnTowerButtonClicked(index);

            button.onClick.AddListener(buttonAction);
            towerButtonActions.Add(button, buttonAction);
        }


        protected override void OnDestroying()
        {
            RemoveTowerButtonListener(firstTowerButton);
            RemoveTowerButtonListener(secondTowerButton);
            RemoveTowerButtonListener(thirdTowerButton);
            RemoveTowerButtonListener(fourthTowerButton);
            RemoveTowerButtonListener(fifthTowerButton);

            base.OnDestroying();
        }

        private void RemoveTowerButtonListener(Button button)
        {
            UnityAction buttonAction = towerButtonActions[button];

            button.onClick.RemoveListener(buttonAction);
            towerButtonActions.Remove(button);
        }

        private void OnTowerButtonClicked(int index)
        {
            TowerButtonClicked?.Invoke(index);
        }
    }
}
