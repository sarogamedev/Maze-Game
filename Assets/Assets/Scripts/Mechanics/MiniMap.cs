using UnityEngine;

namespace Assets.Scripts
{
    public class MiniMap : MonoBehaviour
    {
        private float storedShadowDistance;

        private void OnPreRender()
        {
            storedShadowDistance = QualitySettings.shadowDistance;
            QualitySettings.shadowDistance = 0f;
        }

        private void OnPostRender()
        {
            QualitySettings.shadowDistance = storedShadowDistance;
        }
    }
}
