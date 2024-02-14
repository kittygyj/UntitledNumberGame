using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    Start,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public TMP_Text dialogueText;//TEST
    public Unit playerUnit;
    public Unit enemyUnit;
//Custom Attributes
    public GameObject PlayerHPTrigger;
    public GameObject PlayerATKTrigger;
    public GameObject PlayerDEFTrigger;
    public GameObject EnemyHPTrigger;
    public GameObject EnemyATKTrigger;
    public GameObject EnemyDEFTrigger;

    public GameObject playerGO;
    public GameObject enemyGO ;
    public int enemyindex=0;


    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        if(playerGO==null)
        {
            playerGO = Instantiate(playerPrefab,playerBattleStation);
            playerUnit = playerGO.GetComponent<Unit>();  
        }
        if(enemyGO==null)
        {
            enemyGO = Instantiate(enemyPrefabs[enemyindex],enemyBattleStation);
            enemyUnit = enemyGO.GetComponent<Unit>();    
            enemyindex++;
            if(enemyPrefabs.Length-1<=enemyindex)  
            {
                enemyindex = enemyPrefabs.Length-1;
            }
        }

        InitializeAttributes();

        dialogueText.text = "Enemy Draw His Sword!";

        yield return new WaitForSeconds(2f); //wait for 2 seconds and player turn

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //FindFirstObjectByType<AudioManager>().Play("EnemyAttack3");
        yield return new WaitForSeconds(1f);
        bool isDead = enemyUnit.takeDamage(playerUnit.damage);
        // Damage enemy
        yield return new WaitForSeconds(1f);
        // Check if enemy is dead
        // Change state based on what happened;
        if(isDead)
        {
            state = BattleState.WON;
            Destroy(enemyGO);
            enemyGO=null;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Enemy Attack!";
        //FindFirstObjectByType<AudioManager>().Play("EnemyAttack3");
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.takeDamage(enemyUnit.damage);
        // Damage enemy
        yield return new WaitForSeconds(1f);
        // Check if enemy is dead
        // Change state based on what happened;
        if(isDead)
        {
            state = BattleState.LOST;
            Destroy(playerGO);
            playerGO=null;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }




    void PlayerTurn()
    {
        dialogueText.text = "Player Attack!";
        OnAttackButton();//player auto attack
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            AudioManager.instance.Play("Win");
            dialogueText.text = "you won!";
            state = BattleState.Start;
            StartCoroutine(SetupBattle());
        }
        else if (state == BattleState.LOST)
        {
            AudioManager.instance.Play("Lose");
            dialogueText.text = "you lose!";
            //TODO:Retry Menu
        }
    }

    public void OnAttackButton()
    {
        if(state!=BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    void InitializeAttributes()
    {
        PlayerHPTrigger.GetComponent<AttributeTrigger>().initializeValue(playerUnit.currentHP,playerUnit);
        PlayerATKTrigger.GetComponent<AttributeTrigger>().initializeValue(playerUnit.damage,playerUnit);
        PlayerDEFTrigger.GetComponent<AttributeTrigger>().initializeValue(playerUnit.defense,playerUnit);
        EnemyHPTrigger.GetComponent<AttributeTrigger>().initializeValue(enemyUnit.currentHP,enemyUnit);
        EnemyATKTrigger.GetComponent<AttributeTrigger>().initializeValue(enemyUnit.damage,enemyUnit);
        EnemyDEFTrigger.GetComponent<AttributeTrigger>().initializeValue(enemyUnit.defense,enemyUnit);
    }
}
