using MazeGame.EchoParticleSystem;
using MazeGame.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace MazeGame.GamePlayObjects
{
    public class VaseEncounter : MonoBehaviour
    {
        [SerializeField] private float treshold;
        [SerializeField] private GameObject brokenVersion, Echo;
        private void OnCollisionEnter(Collision other) 
        {
            if (other.relativeVelocity.magnitude > treshold)
            {
                int loud = Mathf.Clamp((int)other.relativeVelocity.magnitude ,3, 24);
                //Instantiate(Echo, transform.position, Quaternion.identity).GetComponent<Echo>().SetSize(loud);
                ParticleManager.Instance.EmitHere(transform.position, loud);
                Instantiate(brokenVersion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    
    }
}
