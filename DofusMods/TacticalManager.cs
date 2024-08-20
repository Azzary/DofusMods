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
            FindQuantumConsole();
        }

        private void Update()
        {
            if(_quantumConsole == null)
            {
                FindQuantumConsole();
            }
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

        private void FindQuantumConsole()
        {
            _quantumConsole = FindObjectOfType<QuantumConsole>();
            if (_quantumConsole != null)
            {
                Debug.LogError("FindObjectOfType find object");
                _quantumConsole.Activate();
                return;
            }
            _quantumConsole = Resources.FindObjectsOfTypeAll<QuantumConsole>().FirstOrDefault();
            if (_quantumConsole != null)
            {
                Debug.LogError("Resources.FindObjectsOfTypeAll find object");
                _quantumConsole.Activate();
                return;
            }
            Debug.LogError("QuantumConsole not found anywhere.");
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