using UnityEngine;

public class ChineseHouse : MonoBehaviour
{
    public bool IsEnemyPenetrating { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
            IsEnemyPenetrating = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
            IsEnemyPenetrating = false;
    }
}
