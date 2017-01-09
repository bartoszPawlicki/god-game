using UnityEngine;

public class BallPitController : MonoBehaviour {
    void OnTriggerExit(Collider other)
    {
        if(other.name == "god_ball")
        {
            Debug.Log("Ball thrown out of pit");
        }
    }
}
