using UnityEngine;

namespace DofusMods
{
    public class SpeedBoostComponent : MonoBehaviour
    {
        private const float NormalTimeScale = 1f;
        private const float BoostedTimeScale = 1.24f;
        private const float HighlyBoostedTimeScale = 10f;
        private const string FightEntitiesName = "fightEntities";

        private bool _isSpeedBoostActive;

        public void SetSpeedHackActive(bool isActive)
        {
            _isSpeedBoostActive = isActive;
            UpdateTimeScale();
        }


        private void Update()
        {
            UpdateTimeScale();
        }

        private void UpdateTimeScale()
        {
            float targetTimeScale = GetTargetTimeScale();
            if (!Mathf.Approximately(Time.timeScale, targetTimeScale))
            {
                Time.timeScale = targetTimeScale;
                Debug.Log($"Time scale set to: {targetTimeScale}x");
            }
        }

        private float GetTargetTimeScale()
        {
            if (!_isSpeedBoostActive)
            {
                return NormalTimeScale;
            }
            return Player.IsInFight() ? HighlyBoostedTimeScale : BoostedTimeScale;
        }

    }
}