using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private RectTransform HealthUI;

        private void OnEnable()
        {
            GetComponent<NetworkHealthState>().health.OnValueChanged += HealthChanged;
        }
        private void OnDisable()
        {
            GetComponent<NetworkHealthState>().health.OnValueChanged -= HealthChanged;
        }
        private void HealthChanged(int previousValue, int newValue)
        {
            if (newValue > 0)
            {
                HealthUI.transform.localScale = new Vector3(newValue / 100f, 1, 1);
            }
        }
    }
}