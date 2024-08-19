using UnityEngine;
using UniverseLib;
using UniverseLib.UI;

namespace DofusMods
{
    public class SimpleMenu : MonoBehaviour
    {
        private UIBase uiBase;
        private DofusModsPanel modPanel;

        void Start()
        {
            Universe.Init(() =>
            {
                uiBase = UniversalUI.RegisterUI("com.mymod.dofusmods", null);
                CreateUI();
            });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                ToggleMenu();
            }
        }

        void CreateUI()
        {
            if (modPanel == null)
            {
                modPanel = new DofusModsPanel(uiBase);
            }
        }

        void ToggleMenu()
        {
            if (modPanel != null)
            {
                modPanel.SetActive(!modPanel.Enabled);
            }
        }
    }
}