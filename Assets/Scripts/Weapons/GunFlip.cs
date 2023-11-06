using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlip : MonoBehaviour {

	public GameObject sprite;
	public GameObject otherSide;

	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "Gun")
		{
            Vector3 newScale = sprite.transform.localScale;
            newScale.y = newScale.y * -1;
            sprite.transform.localScale = newScale;

			otherSide.gameObject.SetActive(true);
			gameObject.SetActive(false);
        }
    }
}
