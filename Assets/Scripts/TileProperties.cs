using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TileProperties : MonoBehaviour
{
    //relevant tile properties to change, set defaults
    [SerializeField] int tileNumber = 0;
    [SerializeField] Color tileColor = Color.white;
    [SerializeField] Color tileNumberColor = Color.black;
    [SerializeField] float tileFont = 10f;

    private void OnValidate()
    {
        gameObject.GetComponentInChildren<TMP_Text>().text = tileNumber.ToString();
        gameObject.GetComponentInChildren<SpriteRenderer>().color = tileColor;
        gameObject.GetComponentInChildren<TMP_Text>().color = tileNumberColor;
        gameObject.GetComponentInChildren<TMP_Text>().fontSize = tileFont;
    }
}
