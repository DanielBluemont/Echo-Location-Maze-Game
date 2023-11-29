using TMPro;
using UnityEngine;

namespace MazeGame.UI
{
    public class VolumeSliderValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_tmp;

        public void ChangeValue(int v)
        {
            m_tmp.text = v.ToString();
        }
    }
}
