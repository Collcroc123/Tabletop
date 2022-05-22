using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public PlayerData player;
    public TMP_Text nameField;
    public RawImage playerIcon;
    public Slider musicSlider, soundSlider;
    public Texture[] icons;
    
    void Start()
    {
        musicSlider.value = player.musicVolume;
        soundSlider.value = player.soundVolume;
        playerIcon.texture = icons[player.playerIcon];
        nameField.text = player.userName;
    }

    public void SetVolume()
    {
        player.musicVolume = musicSlider.value;
        player.soundVolume = soundSlider.value;
    }
    
    public void SetUserName()
    {
        player.userName = nameField.text;
    }
    
    public void SetIcon(int change)
    {
        player.playerIcon += change;
        if (player.playerIcon >= icons.Length)
        {
            player.playerIcon = 0;
        }
        else if (player.playerIcon < 0)
        {
            player.playerIcon = icons.Length - 1;
        }
        playerIcon.texture = icons[player.playerIcon];
    }
    
}
