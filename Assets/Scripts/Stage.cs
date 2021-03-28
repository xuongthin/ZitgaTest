using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class Stage : MonoBehaviour
{
    private int id;
    private Button button;
    private Image mainImage;
    [SerializeField] private Image webImage;
    [SerializeField] private Text indexText;
    [SerializeField] private Image[] starImages;
    [SerializeField] private Image tutorial;

    private void Awake()
    {
        button = GetComponent<Button>();
        mainImage = GetComponent<Image>();
    }

    public void Init(int id, int numberOfStars)
    {
        this.id = id;
        tutorial.enabled = false;
        switch (id)
        {
            case 0:
                indexText.text = "";
                tutorial.enabled = true;
                break;
            case 999:
                indexText.text = "...";
                break;
            default:
                indexText.text = (id + 1).ToString();
                break;
        }

        if (numberOfStars < 0)
        {
            OnLock();
        }
        else
        {
            OnUnlock(numberOfStars);
        }
    }

    public void OnClick()
    {
        StageManager.Ins.ClickHandle(id);
    }

    public void Unlock(int star)
    {
        OnUnlock(star);
    }

    private void OnLock()
    {
        button.interactable = false;
        webImage.enabled = true;

        foreach (Image star in starImages)
        {
            star.enabled = false;
        }
    }

    private void OnUnlock(int numberOfStars)
    {
        button.interactable = true;
        webImage.enabled = false;

        for (var i = 0; i < 3; i++)
        {
            starImages[i].enabled = (i < numberOfStars);
        }
    }
}
