using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private Transform _healthParent;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Sprite _filledHeart;
    [SerializeField] private Sprite _emptyHeartSprite;


    private List<Image> _hearts = new List<Image>();


    private int _health;
    public void UpdateHealth(int health)
    {
        _health++;
    }

    public void Initialize(int heartsAmount)
    {
        for (int i = 0; i < heartsAmount; i++)
        {
            var heartInstance = Instantiate(_heartPrefab, _healthParent).GetComponent<Image>();
            _hearts.Add(heartInstance);
            FillHeart();
        }
    }

    public void FillHeart()
    {
        foreach (var heart in _hearts)
        {
            if (heart.color == Color.white)
            {
                heart.color = Color.red;
                return;
            }
        }
    }

    public void EmptyHeart()
    {
        var reversedHearts = new List<Image>(_hearts);
        reversedHearts.Reverse();
        foreach(var heart in reversedHearts)
        {
            if (heart.color == Color.red)
            {
                heart.color = Color.white;
                return;
            }
        }


    }
}
