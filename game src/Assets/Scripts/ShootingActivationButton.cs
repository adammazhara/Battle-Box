using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootingActivationButton : MonoBehaviour {
    [SerializeField] public Button button;
    private TextMeshProUGUI text;
    private ColorBlock colors;
    private static bool equiped = false;

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        colors = button.colors;
    }

    void Update() {
        if (equiped) {
            UpdateText("Unequip");
            UpdateColor(new Color(.51f, 0.67f, 0.86f)); // if equipped set button to rgb(130, 170, 220)
        } else {
            UpdateText("Equip");
            UpdateColor(new Color(1f, 1f, 1f)); // when unequipped set to rgb(255, 255, 255)
        }
    }
    public void EquipShooting() {
        equiped = !equiped;
        Shooting.active = equiped;
    }

    private void UpdateText(string _text) {
        if (text != null) text.text = _text;
        else Debug.Log("Error: text object is null");
    }

    private void UpdateColor(Color _color) {
        colors.normalColor = _color;
        button.colors = colors;
    }
}
