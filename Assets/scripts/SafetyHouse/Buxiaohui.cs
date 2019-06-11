using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buxiaohui : MonoBehaviour {


    public bool b_Xiaojiangshi;
    public bool b_Wuyaren;
    public bool b_Xiaojiejie;
  //  public bool b_Xiaotrigger;
  //  public bool b_Wutrigger;
  //  public bool b_Zhitrigger;

    public static Buxiaohui Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
