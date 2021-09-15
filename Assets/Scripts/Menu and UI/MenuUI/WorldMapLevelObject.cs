using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapLevelObject : MonoBehaviour
{
    public LvlInfo currentLvlInfo;
    
    private void OnMouseUpAsButton()
    {
        Select();
    }

    public void Select()
    {
        FindObjectOfType<LvlInfoManager>().ShowLvlInformation(this.currentLvlInfo);
    }
}
