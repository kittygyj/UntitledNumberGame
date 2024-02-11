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
    public GameObject enemyPrefab;
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


    void Start()
    {
        //FindFirstObjectByType<AudioManager>().Play("EnemyAttack3");
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        InitializeAttributes();

        dialogueText.text = "Enemy Draw His Sword!";

        yield return new WaitForSeconds(2f); //wait for 2 seconds and player turn

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        FindFirstObjectByType<AudioManager>().Play("EnemyAttack3");
        bool isDead = enemyUnit.takeDamage(playerUnit.damage);
        // Damage enemy
        yield return new WaitForSeconds(2f);
        // Check if enemy is dead
        // Change state based on what happened;
        if(isDead)
        {
            state = BattleState.WON;
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
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.takeDamage(enemyUnit.damage);
        // Damage enemy
        yield return new WaitForSeconds(1f);
        // Check if enemy is dead
        // Change state based on what happened;
        if(isDead)
        {
            state = BattleState.LOST;
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
            dialogueText.text = "you won!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "you lose!";
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
