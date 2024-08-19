using UnityEngine;
using QFSW.QC;

namespace DofusMods
{
    public class TacticalManager : MonoBehaviour
    {
        private QuantumConsole _quantumConsole;
        private bool _tacticalInFightEnabled = false;
        private bool _isTacticalModeActive = false;

        private void Awake()
        {
            CreateQuantumConsole();
        }

        private void Update()
        {
            if (_tacticalInFightEnabled)
            {
                bool isInFight = Player.IsInFight();
                if (isInFight && !_isTacticalModeActive)
                {
                    ActivateTacticalMode();
                }
                else if (!isInFight && _isTacticalModeActive)
                {
                    DeactivateTacticalMode();
                }
            }
        }

        private void CreateQuantumConsole()
        {
            GameObject qcObject = new GameObject("QuantumConsole");
            if (qcObject == null)
            {
                Debug.LogError("Failed to create QuantumConsole GameObject.");
                return;
            }
            _quantumConsole = qcObject.AddComponent<QuantumConsole>();
            if (_quantumConsole != null)
            {
                _quantumConsole.Awake();
                _quantumConsole.gameObject.SetActive(true);
                Debug.Log("QuantumConsole created and activated.");
            }
            else
            {
                Debug.LogError("Failed to create QuantumConsole.");
            }
        }

        public void ToggleTactical()
        {
            if (_quantumConsole != null)
            {
                _quantumConsole.InvokeCommand("/toggleTactical");
                _isTacticalModeActive = !_isTacticalModeActive;
                Debug.Log($"Tactical mode {(_isTacticalModeActive ? "activated" : "deactivated")}.");
            }
            else
            {
                Debug.LogError("QuantumConsole is not available or not active. Unable to toggle tactical mode.");
                CreateQuantumConsole();
            }
        }

        public void SetTacticalInFight(bool enabled)
        {
            _tacticalInFightEnabled = enabled;
            Debug.Log($"Tactical in Fight is now {(enabled ? "enabled" : "disabled")}");

            if (!enabled && _isTacticalModeActive)
            {
                DeactivateTacticalMode();
            }
        }

        private void ActivateTacticalMode()
        {
            if (_quantumConsole != null && !_isTacticalModeActive)
            {
                _quantumConsole.InvokeCommand("/toggleTactical");
                _isTacticalModeActive = true;
                Debug.Log("Tactical mode activated in fight.");
            }
        }

        private void DeactivateTacticalMode()
        {
            if (_quantumConsole != null && _isTacticalModeActive)
            {
                _quantumConsole.InvokeCommand("/toggleTactical");
                _isTacticalModeActive = false;
                Debug.Log("Tactical mode deactivated after fight.");
            }
        }
    }
}