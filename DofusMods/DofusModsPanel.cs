using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using QFSW.QC;
using UniverseLib;

namespace DofusMods
{
    public class DofusModsPanel : UEPanel
    {
        private Toggle _speedHackToggle;
        private Toggle _tacticalInFightToggle;
        private SpeedBoostComponent _speedBoostComponent;
        private TacticalManager _tacticalManager;

        public override string Name => "Dofus Mods";
        public override int MinWidth => 200;
        public override int MinHeight => 200;
        public override Vector2 DefaultAnchorMin => new Vector2(0.5f, 0.5f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.5f, 0.5f);
        public override bool CanDragAndResize => true;
        public override bool ShowByDefault => true;

        public DofusModsPanel(UIBase owner) : base(owner) { }

        protected override void ConstructPanelContent()
        {
            SetupPanelBackground();
            CreateSeparator();
            CreateSpeedHackToggle();
            CreateTacticalToggleButton();
            CreateTacticalInFightToggle();
            FindComponents();
        }

        private void SetupPanelBackground()
        {
            if (UIRoot.TryGetComponent(out Image panelImage))
            {
                panelImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            }
        }

        private void CreateSeparator()
        {
            GameObject separator = UIFactory.CreateUIObject("Separator", ContentRoot);
            UIFactory.SetLayoutElement(separator, minHeight: 2, flexibleWidth: 9999);
            Image separatorImage = separator.AddComponent<Image>();
            separatorImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        private void CreateSpeedHackToggle()
        {
            GameObject toggleObj = UIFactory.CreateToggle(ContentRoot, "SpeedHackToggle", out _speedHackToggle, out Text toggleText);
            UIFactory.SetLayoutElement(toggleObj, minHeight: 30, flexibleWidth: 9999);
            toggleText.text = "Speed Hack";
            toggleText.color = Color.white;
            _speedHackToggle.isOn = false;
            _speedHackToggle.onValueChanged.AddListener(OnSpeedHackToggled);
            ColorBlock colors = _speedHackToggle.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.yellow;
            _speedHackToggle.colors = colors;
        }

        private void CreateTacticalToggleButton()
        {
            var buttonObj = UIFactory.CreateButton(ContentRoot, "TacticalToggleButton", "Toggle Tactical");
            UIFactory.SetLayoutElement(buttonObj.GameObject, minHeight: 30, flexibleWidth: 9999);
            Button button = buttonObj.GameObject.GetComponent<Button>();
            button.onClick.AddListener(ToggleTactical);
        }

        private void CreateTacticalInFightToggle()
        {
            GameObject toggleObj = UIFactory.CreateToggle(ContentRoot, "TacticalInFightToggle", out _tacticalInFightToggle, out Text toggleText);
            UIFactory.SetLayoutElement(toggleObj, minHeight: 30, flexibleWidth: 9999);
            toggleText.text = "Tactical in Fight";
            toggleText.color = Color.white;
            _tacticalInFightToggle.isOn = false;
            _tacticalInFightToggle.onValueChanged.AddListener(OnTacticalInFightToggled);
            ColorBlock colors = _tacticalInFightToggle.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.yellow;
            _tacticalInFightToggle.colors = colors;
        }

        private void FindComponents()
        {
            _speedBoostComponent = Resources.FindObjectsOfTypeAll<SpeedBoostComponent>().FirstOrDefault();
            if (_speedBoostComponent == null)
            {
                Debug.LogError("SpeedBoostComponent not found anywhere.");
            }

            _tacticalManager = Resources.FindObjectsOfTypeAll<TacticalManager>().FirstOrDefault();
            if (_tacticalManager == null)
            {
                Debug.LogError("TacticalManager not found anywhere.");
            }
        }

        private void ToggleTactical()
        {
            if (_tacticalManager != null)
            {
                _tacticalManager.ToggleTactical();
            }
            else
            {
                Debug.LogError("TacticalManager not found. Unable to toggle tactical mode.");
                FindComponents();
            }
        }

        private void OnTacticalInFightToggled(bool value)
        {
            if (_tacticalManager != null)
            {
                _tacticalManager.SetTacticalInFight(value);
            }
            else
            {
                Debug.LogError("TacticalManager not found. Tactical in Fight toggle ineffective.");
                FindComponents();
            }
        }

        private void OnSpeedHackToggled(bool value)
        {
            if (_speedBoostComponent != null)
            {
                _speedBoostComponent.SetSpeedHackActive(value);
                Debug.Log($"Speed Hack is now {(value ? "enabled" : "disabled")}");
            }
            else
            {
                Debug.LogError("SpeedBoostComponent not found. Speed Hack toggle ineffective.");
                FindComponents();
            }
        }
    }
}