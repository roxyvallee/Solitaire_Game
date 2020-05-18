﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject cardEmpty;
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    
    public static string[] suits = new string[] {"C", "D", "H", "S"};
    public static string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "V", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] tops;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();    
    private List<string> bottom2 = new List<string>();    
    private List<string> bottom3 = new List<string>();    
    private List<string> bottom4 = new List<string>();    
    private List<string> bottom5 = new List<string>();    
    private List<string> bottom6 = new List<string>();        
    public List<string> deck;
    // Start is called before the first frame update
    void Start()
    {
       bottoms = new List<string>[] {bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6}; 
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        foreach(string card in deck)
        {
            print(card);
        }

        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        //NumberBottom();
    }

    // Création de la pioche avec toutes les cartes
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                newDeck.Add(s+v);
            }
        }
        return newDeck;
    }


    void Shuffle<T>(List<T> list) // create random here
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void SolitaireSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = i; j < 7; j++ )
            {
                bottoms[j].Add(deck.Last <string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    IEnumerator SolitaireDeal()
    {
        for(int i = 0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 0.03f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(cardPrefab,new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;

                if(card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                yOffset = yOffset + 0.3f;
                zOffset = zOffset + 0.03f;
                
            }
        }
        
    }
/*
    void NumberBottom()
    {
        float xOffset = 0;
        float yOffset = 0;
        float xposition = -8.0f;
        int random = 17;
        for(int i=0; i < random; i++)
        {
            if( i == 9)
            {
                yOffset = 3.0f;
                xOffset = 0.0f;
            }
            GameObject newEmpty = Instantiate(cardEmpty, new Vector3(xposition + xOffset, -0.1f - yOffset, transform.position.z), Quaternion.identity );
            xOffset += 2.0f; 
        }
    }
*/
}