using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField]
    private int itemValue;
	public AudioClip CoinSound;
	AudioSource audio;
	private void Awake()
	{
		audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        var playerMovementController = other.GetComponentInParent<PlayerMovement>();
        if (playerMovementController != null)
        {
			audio.PlayOneShot(CoinSound);
            //if(ScoreManager.Instance.totalItems < ScoreManager.Instance.maxItems)
            //{ 
				ScoreManager.Instance.AddValue(itemValue);
				ScoreManager.Instance.totalItems++;
				Destroy(this.gameObject,0.5f);
            //}
        }
    }

}
