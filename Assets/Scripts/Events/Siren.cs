using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    [SerializeField, Min(0)] private float _deltaChanging;

    private AudioSource _audioSource;
    private bool _isEnemyPenetrating;

    private void Reset() =>
        _deltaChanging = 2;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void Update() =>
        ChangeVolume();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
            _isEnemyPenetrating = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
            _isEnemyPenetrating = false;
    }

    private void ChangeVolume()
    {
        int desiredVolume = System.Convert.ToInt32(_isEnemyPenetrating);

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, desiredVolume, _deltaChanging * Time.deltaTime);
    }
}
