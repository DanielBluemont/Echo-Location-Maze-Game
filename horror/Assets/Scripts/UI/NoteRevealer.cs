using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGame.UI
{
    public class NoteRevealer : MonoBehaviour
    {
        [SerializeField] private List<NoteUI> _notesUI;
        [SerializeField] private GameObject _content;
        int index = 0;
        private void ToggleInfo(bool val)
        {
            _content.SetActive(val);
        }
        private void OnEnable()
        {
            Player.Player.OnTab += ToggleInfo;
        }
        private void OnDisable()
        {
            Player.Player.OnTab -= ToggleInfo;
        }
        public void CreateNoteUI(string num, Color color)
        {
            foreach (var note in _notesUI) 
            {
                if (note.GetColor() == color) return;
            }
            _notesUI[index].SetNote(num, color);
            index++;
        }
    }
}
