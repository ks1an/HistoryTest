using UnityEngine;
using UnityEngine.UI;

public class QProgressBar : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSys;
    [SerializeField] private float _fillSpeed = 0.5f;

    private Slider _slider;
    private float _curProgress = 0;

    private void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (_slider.value < _curProgress)
        {
            _slider.value += _fillSpeed * Time.deltaTime;
            if(!_particleSys.isPlaying)
                _particleSys.Play();
        }
        else
            _particleSys.Stop();
    }

    public void AnimationOutEnded()
    {
        gameObject.SetActive(false);
    }

    public void IncrementProgress(float value)
    {
        _curProgress = _slider.value + value;
    }

    private void OnDisable()
    {
        _curProgress = 0;
        _slider.value = 0;
    }
}
