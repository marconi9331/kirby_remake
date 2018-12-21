using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirbyGame
{
    public class DoorController : MonoBehaviour
    {
       public void DoorOpen(GameObject Door)
        {
            RaycastHit2D[] DoorsNext = Physics2D.RaycastAll(Door.transform.position, Vector2.left / 2, 2f, 1 << 14);
            for (int i = 0; i < DoorsNext.Length; i++)
            {
                Debug.Log(DoorsNext);
            }
            Door.SetActive(false);
            Time.timeScale = 0;
        }
    }
}