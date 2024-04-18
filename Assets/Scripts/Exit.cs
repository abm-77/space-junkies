using UnityEngine;

using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
	[SerializeField]
	public string next_level;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(next_level);
		}
	}
}
