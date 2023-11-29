using MazeGame.Player;
using UnityEngine;

namespace MazeGame.GamePlayObjects
{
    public class CodeInVase : MonoBehaviour
    {
        [SerializeField] private float treshold;
        [SerializeField] private GameObject brokenVersion, Echo, CodeNote;
        private int _codePiece = -1;
    
        public void SetCode(int code)
        {
            _codePiece = code;
        }
    
        private void OnCollisionEnter(Collision other) 
        {
            if (other.relativeVelocity.magnitude > treshold)
            {
                int loud = Mathf.Clamp((int)other.relativeVelocity.magnitude ,3, 24);
                Instantiate(Echo, transform.position, Quaternion.identity).GetComponent<Echo>().SetSize(loud);
                Instantiate(brokenVersion, transform.position, Quaternion.identity);
                if (_codePiece != -1)
                {
                    Instantiate(CodeNote, transform.position, Quaternion.identity).GetComponent<Note>().SetDigit(_codePiece);
                }
                Destroy(this.gameObject);
            }
        }
   
    }
}
