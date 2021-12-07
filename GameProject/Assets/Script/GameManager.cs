using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MenuPanel;
    // Start is called before the first frame update
    public void GameStart(){
        MenuPanel.SetActive(false);
    }
}
