using UnityEngine;
using UnityEngine.UI;

public class Button2Script : MonoBehaviour
{
    public Button button2;
    public int button2Index;
    private Image button2Image;
    private Sprite originalSprite;

    void Start()
    {
        button2Image = button2.GetComponent<Image>();
        originalSprite = button2Image.sprite;
        button2.onClick.AddListener(ResetImage);
    }

    void ResetImage()
    {
        button2Image.sprite = originalSprite;
        AbilityMenu.currentIndex = button2Index;
        AbilityMenu.imageSwitched[button2Index] = false;
    }
}
