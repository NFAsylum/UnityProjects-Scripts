using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    public List<GameObject> allCards;
    List<GameObject> listedCards;

    public GameObject GetCardById(int incomingId)
    {
        return allCards[incomingId];
    }

    public List<GameObject> GetCardByType(string incomingType)
    {
        listedCards = null;

        for (int x = 0; x < allCards.Count; x++)
        {
            if (allCards[x].GetComponent<Card>().type == incomingType)
            {
                listedCards[listedCards.Count] = allCards[x];
            }
        }

        return listedCards;
    }
}
