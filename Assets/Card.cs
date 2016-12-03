using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
    [SerializeField]
	public string CardValue;
    private int NumberValue;
    private string Symbols;

	void Start ()
    {
        NumberValue = (int)Random.Range(0, 10);
        Symbols = "/*-+";
        if ((int)Random.Range(0, 2) == 1)
            CardValue = Symbols[(int)Random.Range(0, 4)].ToString();
        else
            CardValue = NumberValue.ToString();
	}
}
