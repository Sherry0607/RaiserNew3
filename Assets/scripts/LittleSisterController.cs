using UnityEngine;

public class LittleSisterController : MonoBehaviour {

    public static bool isPlayerNear = false;//player是否靠近 
	// Use this for initialization
	void Start () {
        isPlayerNear = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerNear = true;

    }

}
