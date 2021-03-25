using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class Stage : MonoBehaviour
{
    private int index;
    private Button button;
    private Image mainImage;
    [SerializeField] private Image webImage;
    [SerializeField] private Text indexText;
    [SerializeField] private Image[] starImages;

    [Header("Sprites")]
    public Sprite unlockedSprite;
    public Sprite lockSprite;

    private void Start()
    {
        button = GetComponent<Button>();
        mainImage = GetComponent<Image>();
    }

    public void Init(int index, int numberOfStars)
    {
        this.index = index;
        indexText.text = (index + 1).ToString();

        if (index > LevelController.Instance.MaxUnlockedStages)
        {
            SetOnLock();
        }
        else
        {
            SetOnUnlock(numberOfStars);
        }
    }

    public void Unlock()
    {
        SetOnUnlock(0);
    }

    public void OnClick()
    {
        LevelController.Instance.ClickHandle(index);
    }

    private void SetOnLock()
    {
        button.interactable = false;
        mainImage.sprite = lockSprite;
        webImage.enabled = true;
        indexText.enabled = false;

        foreach (Image star in starImages)
        {
            star.enabled = false;
        }
    }

    private void SetOnUnlock(int numberOfStars)
    {
        button.interactable = true;
        mainImage.sprite = unlockedSprite;
        webImage.enabled = false;
        indexText.enabled = true;

        for (var i = 0; i < 3; i++)
        {
            starImages[i].enabled = (i < numberOfStars);
        }
    }
}
