using UniverseLib.UI;
using UniverseLib.UI.Panels;

namespace DofusMods
{
    public abstract class UEPanel : PanelBase
    {
        protected UEPanel(UIBase owner) : base(owner) { }

        public virtual bool ShowByDefault => false;

        public override void ConstructUI()
        {
            base.ConstructUI();
        }

        protected override void LateConstructUI()
        {
            base.LateConstructUI();
            if (ShowByDefault)
            {
                SetActive(true);
            }
        }
    }
}