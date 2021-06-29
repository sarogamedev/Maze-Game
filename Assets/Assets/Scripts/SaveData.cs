using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
   public int level;

   public SaveData (GameManager gameManager)
   {
       level = gameManager.levelCount;
   }
}
