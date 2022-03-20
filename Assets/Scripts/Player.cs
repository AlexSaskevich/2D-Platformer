using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public Text scoreText;

    private int _score;

    public void AddCoin(int coinValue)
    {
        _score += coinValue;

        scoreText.text = _score.ToString();
    }
}