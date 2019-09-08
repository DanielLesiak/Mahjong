using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mahjong : MonoBehaviour
{
    [HideInInspector]
    public List<Tile_SO> allTiles;

    Queue<Tile_SO> tiles_shuffled = new Queue<Tile_SO>();
    int doraIndicators = 0;

    [HideInInspector]
    public Player[] players;
    int currentPlayerID;

    [HideInInspector]
    public static bool playerTurn = false;

    public GameObject tester_tile, tester_holder ;


    private void Start()
    {
        allTiles.AddRange(allTiles);
        allTiles.AddRange(allTiles);

       while (allTiles.Count > 0) //shuffle
        {
            int i = Random.Range(0, allTiles.Count);

            tiles_shuffled.Enqueue(allTiles[i]);
            allTiles.RemoveAt(i);
        }

        SelectDealer();
        //StartingHands();
        StartCoroutine(StartingHandsIE());
    }
    private void Update()
    {
        doraIndicators = Mathf.Clamp(doraIndicators, 0, 5);//No more than 5 dora indicators allowed 

        if(Input.GetMouseButtonDown(0))
        {
            print(playerTurn);
        }
    }

    private Transform CurrentPlayer()
    {
        return null;
    }

    /*void StartingHands()
    {
        for (int i = 1; i < 14; i++)
        {
            GiveTile(tiles_shuffled.Dequeue(),playerHand);
            GiveTile(tiles_shuffled.Dequeue(),op1Hand);
            GiveTile(tiles_shuffled.Dequeue(),op2Hand);
            GiveTile(tiles_shuffled.Dequeue(),op3Hand);
        }
    }*/

    IEnumerator StartingHandsIE()
    {
        for (int i = 1; i < 14; i++)
        {
            for (int n = 0; n < 4; n++)
            {
                yield return new WaitForSeconds(0.1f);
                GiveTile(tiles_shuffled.Dequeue(), players[currentPlayerID].transform);
                NextPlayer();
            }
        }
        print("Dealing's Done");
        yield return new WaitForSeconds(1f);
        print("Player " + currentPlayerID + 1 + "Start");
        StartCoroutine(PlayerTurn());


    }

    IEnumerator PlayerTurn()
    {
        while (tiles_shuffled.Count > (14 - doraIndicators))
        {
            GiveTile(tiles_shuffled.Dequeue(), players[currentPlayerID].transform);
            print("Tiles Left: " + (tiles_shuffled.Count - (14-doraIndicators)));

            if (currentPlayerID == 0)
            {
                playerTurn = true;
                //players[currentPlayerID].MakeTilesSelectable(true); //*1-Player can only highlight tiles if it's their turn 
                yield return new WaitUntil(() => playerTurn == false);
                //players[currentPlayerID].MakeTilesSelectable(false); //*1-Player can only highlight tiles if it's their turn 
                NextPlayer();
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                players[currentPlayerID].DiscardRandomTile();
                NextPlayer();
            }
            yield return new WaitForSeconds(0.5f);

        }

    }

    void GiveTile(Tile_SO currentTile, Transform hand)
    {
        GameObject newTile = Instantiate(tester_tile);
        newTile.transform.SetParent(hand);
        newTile.transform.localRotation = Quaternion.identity;

        Tile tileScript = newTile.GetComponent<Tile>();
        tileScript.SetTile(currentTile);

        if(hand.transform == players[0].transform)
        {
            tileScript.PlayerTile();
        }
        hand.GetComponent<Player>().RecieveTile(tileScript);
    }

    void NextPlayer()
    {
        currentPlayerID++;

        if (currentPlayerID > 3) currentPlayerID = 0;
    }

    void SelectDealer()
    {
        currentPlayerID = Random.Range(0, 3);
    }

    void Testing()
    {
        foreach (Tile_SO currentTile in tiles_shuffled) //Display all tiles for testing
        {
            GameObject newTile = Instantiate(tester_tile);
            newTile.transform.SetParent(tester_holder.transform);
            newTile.GetComponent<Tile>().SetTile(currentTile);
        }
    }
}
