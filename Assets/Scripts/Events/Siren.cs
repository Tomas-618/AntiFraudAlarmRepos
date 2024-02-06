using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    [SerializeField, Min(0)] private float _deltaChanging;

    [SerializeField] private ChineseHouse _house;

    private AudioSource _audioSource;

    private void Reset() =>
        _deltaChanging = 2;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void Update() =>
        ChangeVolume();

    private void ChangeVolume()
    {
        int desiredVolume = System.Convert.ToInt32(_house.IsEnemyPenetrating);

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, desiredVolume, _deltaChanging * Time.deltaTime);
    }
}
