using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : BasicScreen
{
    public static Game Instance;

    public Button close;
    public TMP_Text score;
    public TMP_Text timer;
    public TMP_Text reward;
    public TMP_Text result;

    public string[] _colors;
    public Sprite[] _planes;
    public Sprite[] destroyedRoad;
    public Sprite[] correctRoad;
    public Sprite _defaultRoadSkin;
    public GameObject[] _defauldRoads;
    public GameObject[] _destroyedRoads;
    public GameObject[] _correectRoads;
    public GameObject[] _planesObjects;
    public Transform[] _planeBasePos;

    private List<string> _currentColors = new List<string>();
    private string[] _placedColors = new string[3];
    private int _compeatedRows;

    private Coroutine _coroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        close.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
    }
    public override void ResetScreen()
    {
        StopAllCoroutines();
        FinishGame();
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
        timer.text = "1:30";
        StartGame();
    }

    private void StartGame()
    {
        _compeatedRows = 0;
        Time.timeScale = 1;
        StartCoroutine(Timer());
        SetRow(false);
    }
    private void FinishGame()
    {
        StopCoroutine(Timer());
    }

    private void Lose()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPopup(PopupTypes.Lose);
    }

    private void Win()
    {
        string key;
        if (_compeatedRows >= 5)
        {
            key = "Achieve" + 1;
            PlayerPrefs.SetInt(key, 1);
        }
        if (_compeatedRows >= 10)
        {
            key = "Achieve" + 2;
            PlayerPrefs.SetInt(key, 1);
        }
        key = "Achieve" + 0;
        PlayerPrefs.SetInt(key, 1);

        reward.text =  (_compeatedRows * 300).ToString();
        result.text = "x" + _compeatedRows;

        int newScore = (_compeatedRows * 300) + PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", newScore);

        Time.timeScale = 0;
        UIManager.Instance.ShowPopup(PopupTypes.Win);
    }

    private void SetRow(bool isRestart = false)
    {
        
        for (int i = 0; i < _defauldRoads.Length; i++)
        {
            _defauldRoads[i].GetComponent<RoadManager>().isBusy = false;
        }
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
        for (int i = 0; i < _placedColors.Length; i++)
            _placedColors[i] = "";
        foreach (var road in _destroyedRoads)
            road.gameObject.SetActive(false);

        foreach (var road in _correectRoads)
            road.gameObject.SetActive(false);

        foreach (var road in _defauldRoads)
            road.gameObject.SetActive(true);

        for (int i = 0; i < _planesObjects.Length; i++)
        {
            _planesObjects[i].GetComponent<DragAndDropUI>().canDrug = true;
            _planesObjects[i].transform.position = _planeBasePos[i].position;
            _planesObjects[i].SetActive(true);
        }

        if (isRestart) return;
        
        _currentColors.Clear();

        // Створюємо список доступних індексів і перемішуємо
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < _colors.Length; i++)
            availableIndexes.Add(i);

        ShuffleList(availableIndexes);

        // Призначаємо унікальні кольори та спрайти
        for (int i = 0; i < _planesObjects.Length; i++)
        {
            _planesObjects[i].transform.position = _planeBasePos[i].position;

            int colorIndex = availableIndexes[i]; // беремо без повторень
            _currentColors.Add(_colors[colorIndex]);
            _planesObjects[i].GetComponent<Image>().sprite = _planes[colorIndex];
            _planesObjects[i].GetComponent<DragAndDropUI>().color = _colors[colorIndex];
        }

        // Якщо ще потрібно перемішати список кольорів після призначення
        ShuffleList(_currentColors);
    }
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void OnPlaneClaimed(string color, GameObject road)
    {
        road.GetComponent<RoadManager>().isBusy = true;
        for (int i = 0; i < _defauldRoads.Length; i++)
        {
            if(road == _defauldRoads[i])
            {
                _placedColors[i] = color;
            }
        }
        CheckIsWin();
    }
    public void CheckIsWin()
    {
        int result = 0;
        for (int i = 0; i < _placedColors.Length; i++)
        {
            if(_placedColors[i] != "")
            {
                result++;
            }
        }
        if(result == 3)
        {
            int score = 0;
            for(int i = 0; i < _placedColors.Length; i++)
            {
                if (_placedColors[i] == _currentColors[i])
                {
                    score++;
                }
            }
            if(score == 3)
            {
                _compeatedRows++;
                _coroutine = StartCoroutine(Fly());
                foreach (var road in _defauldRoads)
                    road.gameObject.SetActive(false);

                for (int i = 0; i < _currentColors.Count; i++)
                {
                    for (int j = 0; j < _colors.Length; j++)
                    {
                        if (_colors[j] == _currentColors[i])
                        {
                            _correectRoads[i].GetComponent<Image>().sprite = correctRoad[j];
                            _correectRoads[i].SetActive(true);
                        }
                    }
                }
                Invoke("StartNewRow", 3);
            }
            else
            {
                foreach (var plane in _planesObjects)
                {
                    plane.SetActive(false);
                }

                foreach (var road in _defauldRoads)
                    road.gameObject.SetActive(false);

                for (int i = 0; i < _currentColors.Count; i++)
                {
                    for (int j = 0; j < _colors.Length; j++)
                    {
                        if (_colors[j] == _currentColors[i])
                        {
                            _destroyedRoads[i].GetComponent<Image>().sprite = destroyedRoad[j];
                            _destroyedRoads[i].SetActive(true);
                        }
                    }
                }
                Invoke("RestartRow", 1);

            }
        }

        
    }

    private void RestartRow()
    {
        SetRow(true);
    }

    private void StartNewRow()
    {
        SetRow(false);
    }
    private IEnumerator Fly()
    {
        while (true)
        {
            
            foreach (var plane in _planesObjects)
            {
                RectTransform rt = plane.GetComponent<RectTransform>();
                rt.anchoredPosition += Vector2.up * 600f * Time.deltaTime; // рух вгору
            }
            yield return null; 
        }
    }
    private IEnumerator Timer()
    {
        float time = 90;
        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            timer.text = $"{minutes}:{seconds:D2}";
        }
        if(_compeatedRows > 0)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }
    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
