using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField]
    private int itemValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerMovementController = other.GetComponentInParent<PlayerMovement>();
        if (playerMovementController != null)
        {
            ScoreManager.Instance.AddValue(itemValue);
            Destroy(this.gameObject);
        }
    }

}
