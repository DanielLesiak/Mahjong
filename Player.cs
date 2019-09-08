using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int playerID;

    public Transform discardPile;

    public List<Tile> tilesOnHand = new List<Tile>();

    public void DiscardRandomTile()
    {
        //Destroy(transform.GetChild(Random.Range(0, transform.childCount - 1)).gameObject);
        TileDiscard(tilesOnHand[Random.Range(0, tilesOnHand.Count - 1)]);

    }

    public void RecieveTile(Tile tile)
    {
        tilesOnHand.Add(tile);
    }

    public void TileDiscard(Tile tileToDiscard)
    {
        tileToDiscard.transform.SetParent(discardPile);
        tilesOnHand.Remove(tileToDiscard);

        OrganiseTiles();
    }

    public void MakeTilesSelectable(bool selectable)
    {
        foreach(Tile tile in tilesOnHand)
        {
            tile.GetComponent<Button>().enabled = selectable;
        }
    }

    void OrganiseTiles()//MESSED UP
    {
        Tile temp;

        for (int write = 0; write < tilesOnHand.Count; write++)
        {
            for (int sort = 0; sort < tilesOnHand.Count - 1; sort++)
            {
                if (tilesOnHand[sort].GetInstanceID() > tilesOnHand[sort + 1].GetInstanceID())
                {
                    temp = tilesOnHand[sort + 1];
                    tilesOnHand[sort + 1] = tilesOnHand[sort];
                    tilesOnHand[sort] = temp;
                }
            }
        }

        foreach(Tile tile in tilesOnHand)
        {
            tile.transform.SetAsLastSibling();
        }
    }
}
