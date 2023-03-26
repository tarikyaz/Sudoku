using UnityEngine;

public class Keyboard : MonoBehaviour
{
    internal void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void Show()
    {
        gameObject.SetActive(true);
    }
}
