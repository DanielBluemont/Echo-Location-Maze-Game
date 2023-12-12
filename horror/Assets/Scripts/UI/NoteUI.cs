using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGame.UI
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _noteColor;
        public Color GetColor()
        {
            return _noteColor.color;
        }
        public void SetNote(string num, Color color)
        {
            _text.text = num;
            _noteColor.color = color;
        }
    }
}
