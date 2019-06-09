using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager {

    //--------------------------scene-------------------------------------------------------

    public const string LEVEL_level1 = "level1";
    public const string LEVEL_level2 = "level2";
    public const string LEVEL_level3 = "level3";


    //--------------------------go-------------------------------------------------------

    public const string GO_GameManager = "GameManager";


    //--------------------------resource-------------------------------------------------------
    public const string BACKPACK_Path = "Art/";
    public const string DIALOG_Path = "Dialogue/";

    //--------------------------ui-------------------------------------------------------
    public const string UI_DIALOG_Path = "Canvas/DialogPanel";


    //--------------------------tag-------------------------------------------------------
    public const string TAG_PLAYER = "Player";

    

    //--------------------------save key-------------------------------------------------------
    public const string Save_Level1DoorOpen = "Level1DoorOpen";

    public const string Save_PlayerPosX = "PlayerPosX";
    public const string Save_PlayerPosY = "PlayerPosY";
    public const string Save_PlayerPosZ = "PlayerPosZ";

}



public class GlobalVar {
    public static Vector3 defaultPosLevel1 = new Vector3(130f, -25f, 0f);
    public static Vector3 defaultPosLevel2 = new Vector3(-54.3f, 1.9f,0f);
    public static Vector3 defaultPosLevel3 = new Vector3(-179.5f, -2.57f, 0f);


}