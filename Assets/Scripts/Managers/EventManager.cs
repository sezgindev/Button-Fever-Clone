using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class  EventManager
{
   public static UnityAction OnButtonClick;
   public static UnityAction<float> OnGetMoney;
   public static UnityAction UpdateUIElements;
}