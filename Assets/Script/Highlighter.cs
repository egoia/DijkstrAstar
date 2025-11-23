using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Material glow;
    public Material normal;

    public void Highlight(bool on_off)
    {
        if(on_off)GetComponent<SpriteRenderer>().material = glow;
        else GetComponent<SpriteRenderer>().material = normal;
    }
}
