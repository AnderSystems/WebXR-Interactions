using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class memoryGame : MonoBehaviour
{
    public Animator cards;
    public List<Transform> cardsPositions = new List<Transform>();
    public List<Image> cardsImg = new List<Image>();
    public GameObject cardPrefab;
    public bool Get;
    public bool Generate;

    public void OnValidate()
    {
        if (!Get)
        {
            cardsPositions = new List<Transform>();
            cardsImg = new List<Image>();

            foreach (var t in GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name.Contains("Card_Visuals"))
                {
                    cardsPositions.Add(t);
                }
            }

            foreach (var t in GetComponentsInChildren<Image>())
            {
                if (t.gameObject.name.Contains("Card"))
                {
                    cardsImg.Add(t);
                }
            }
            Get = false;
        }

        if (Generate)
        {
            for (int i = 0; i < cardsPositions.Count; i++)
            {
                cardsImg.Add(((GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(cardPrefab)).GetComponent<Image>());
            }
            Generate = false;
        }
    }

    public void Play()
    {
        cards.SetBool("Start", true);
    }

    public void Stop()
    {
        cards.SetBool("Start", false);
    }
}
