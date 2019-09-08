using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    Tile_SO tile;

    TextMeshProUGUI text;

    [HideInInspector]
    public Button tileSelectable;

    Player player;


    public void SetTile(Tile_SO chosenTile)
    {
        tile = chosenTile;
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        switch (tile.tileType) //TEMP - Variate colours and write names of tiles
        {
            case TileType.Pins:
                text.text = tile.tileType.ToString() + "\n" + tile.numberValue;
                GetComponent<Image>().color = Color.gray;
                break;

            case TileType.Bamboos:
                text.text = tile.tileType.ToString() + "\n" + tile.numberValue;
                GetComponent<Image>().color = Color.green;
                break;

            case TileType.Characters:
                text.text = tile.tileType.ToString() + "\n" + tile.numberValue;
                GetComponent<Image>().color = Color.red;
                break;

            case TileType.Winds:
                text.text = tile.tileType.ToString() + "\n" + tile.windValue;
                GetComponent<Image>().color = Color.blue;
                break;

            case TileType.Dragons:
                text.text = tile.tileType.ToString() + "\n" + tile.dragonValue;
                GetComponent<Image>().color = Color.white;
                break;
        }
    }

    public void PlayerTile()
    {
        tileSelectable = GetComponent<Button>();
        tileSelectable.enabled = true; //*1-The player is alwasy able highlight tiles, but can't click until it'stheir turn;
        player = transform.parent.GetComponent<Player>();
    }

    public void TilePicked()
    {
        if (Mahjong.playerTurn)
        {
            Mahjong.playerTurn = false;
            player.TileDiscard(GetComponent<Tile>());
        }
    }

    public int TilesUniqueID()
    {
        return tile.uniqueTileID;
    }

}
