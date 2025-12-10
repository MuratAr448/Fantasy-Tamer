using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    public Image MonsterImage;
    public string monsterName;
    public TextMeshProUGUI NameText;
    public int LV;
    public TextMeshProUGUI LVText;
    public int HPMax;
    public int HPCurrent;
    public Slider ShowPlayerHP;
    private void Update()
    {
        NameText.text = monsterName;
        ShowPlayerHP.maxValue = HPMax;
        ShowPlayerHP.value = HPCurrent;
        LVText.text = "LV: " + LV;
    }
}
