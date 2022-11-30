using System;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    public GameObject obj;
    public float fadeSpeed = 1.5f;
    public float wait = 1.0f;

    private bool _isFading = false;
    private bool _isWaiting = false;
    private bool _isFadingOut = false;
    
    private float _alpha = 0.0f;
    private CanvasGroup _canvasGroup;
    
    private float _timer = 0.0f;
    
    private void Start()
    {
        obj.SetActive(false);
        _canvasGroup = obj.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0.0f;
    }

    private void Update()
    {
        if (!_isFading)
        {
            _timer = 0.0f;
            _alpha = 0.0f;
            return;
        }
        _canvasGroup.alpha = _alpha;
        if (!_isFadingOut && !_isWaiting)
        {
            _alpha += Time.deltaTime * fadeSpeed;
            if (_alpha >= 1.0f)
            {
                _alpha = 1.0f;
                _isWaiting = true;
            }
        }
        if (_isWaiting)
        {
            _timer += Time.deltaTime;
            if (_timer >= wait)
            {
                _isFadingOut = true;
                _isWaiting = false;
            }
        }
        if (_isFadingOut)
        {
            _alpha -= Time.deltaTime * fadeSpeed;
            if (_alpha <= 0.0f)
            {
                _alpha = 0.0f;
                _isFading = false;
                _isFadingOut = false;
                obj.SetActive(false);
            }
        }
    }
    
    public void Fade()
    {
        _isFading = true;
        obj.SetActive(true);
    }
}