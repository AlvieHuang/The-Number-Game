using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
    private char CardValue;
    private int NumberValue;
    private string Symbols;

	void Start ()
    {
        NumberValue = (int)Random.Range(0, 101);
        Symbols = "/*-+";
        if ((int)Random.Range(0, 2) == 1)
            CardValue = Symbols[(int)Random.Range(0, 4)];
        else
            CardValue = (char)NumberValue;
	}
}
