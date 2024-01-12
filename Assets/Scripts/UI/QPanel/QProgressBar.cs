using UnityEngine;
using UnityEngine.UI;

public class QProgressBar : MonoBehaviour
{
    [SerializeField] private float _fillSpeed = 0.5f;
    [SerializeField] private ParticleSystem _particleSystemProgress;
    [SerializeField] private ParticleSystem _particleSysLack;
    [SerializeField] private Slider _sliderLack;

    private Slider _sliderProgress; 
    private float _curProgress = 0;
    private float _curLack = 0;

    private void Awake()
    {
        _sliderProgress = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (_sliderProgress.value < _curProgress)
        {
            _sliderProgress.value += _fillSpeed * Time.deltaTime;
            if(!_particleSystemProgress.isPlaying)
                _particleSystemProgress.Play();
        }
        else
        {
            _particleSystemProgress.Stop();
        }

        if (_sliderLack.value < _curLack)
        {
            _sliderLack.value += _fillSpeed * Time.deltaTime;
            if (!_particleSysLack.isPlaying)
                _particleSysLack.Play();
        }
        else
        {
            _particleSysLack.Stop();
        }
    }

    public void AnimationOutEnded()
    {
        gameObject.SetActive(false);
    }

    public void IncrementProgress(float value)
    {
        _curProgress = _sliderProgress.value + value;
    }

    public void IncrementLack(float value)
    {
        _curLack  = _sliderLack.value + value;
    }

    private void OnDisable()
    {
        _curProgress = 0;
        _sliderProgress.value = 0;

        _curLack = 0;
        _sliderLack.value = 0;
    }
}
