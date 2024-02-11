using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//该脚本控制游戏的基本流程，规则，等等
public class GameController : MonoBehaviour
{

    [SerializeField] GameObject RetryMenu;
    BattleSystem battleSystem; 
    // Start is called before the first frame update
    void Start()
    {
        battleSystem = FindFirstObjectByType<BattleSystem>();
        RetryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(battleSystem.state == BattleState.LOST)
        {
            RetryMenu.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
