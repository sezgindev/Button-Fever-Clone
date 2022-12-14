using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
   public static UnityAction OnButtonClick;
   public static UnityAction<float> OnGetMoney;
   public static UnityAction UpdateUIElements;
   public static UnityAction AddNewButtonRequest;
   public static UnityAction<MergeArea> NewButtonSpawn;
}
