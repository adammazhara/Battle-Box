using UnityEngine;
using UnityEngine.UI;

public class AbilityMenu : MonoBehaviour
{
    public Button button1;
    public Button[] button2s;
    private Image button1Image;
    private Image[] button2Images;
    public static int currentIndex = 0;
    public static bool[] imageSwitched;

    void Start()
    {
        button1Image = button1.GetComponent<Image>();
        button2Images = new Image[button2s.Length];
        imageSwitched = new bool[button2s.Length];
        
        for (int i = 0; i < button2s.Length; i++)
        {
            button2Images[i] = button2s[i].GetComponent<Image>();
            imageSwitched[i] = false;
        }
        button1.onClick.AddListener(ChangeImage);
    }

    void ChangeImage()
    {
        if (!imageSwitched[currentIndex])
        {
            button2Images[currentIndex].sprite = button1Image.sprite;
            imageSwitched[currentIndex] = true;
            currentIndex = (currentIndex + 1) % button2Images.Length;
        }
    }
}
