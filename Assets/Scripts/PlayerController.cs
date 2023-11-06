using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	private void Awake()
	{
		instance = this;
	}

	public float moveSpeed;
    public LayerMask obstacleLayer;

    public Animator animator;

	public float pickupRange = 1.5f;

	[Header("Weapons")]
    public int maxWeapons = 3;
    public List<Weapon> unassignedWeapons;
	public List<Weapon> assignedWeapons;
	public List<Weapon> activeWeapons;
	[HideInInspector]
	public List<Weapon> fullyLevelledWeapons = new List<Weapon>();
	public Weapon usedWeapon;
	private int usedWeaponIndex;

    void Start () 
	{
		if (SaveSystem.instance.saveData.loadout.Count != 0)
		{
			for (int i = 0; i < unassignedWeapons.Count; i++)
			{
				if (SaveSystem.instance.saveData.loadout.Contains(unassignedWeapons[i].weaponName))
				{
					assignedWeapons.Add(unassignedWeapons[i]);
				}
			}
		}
		else if (assignedWeapons.Count == 0)
		{
            AddWeapon(Random.Range(0, unassignedWeapons.Count));

        }

		SetAssignedWeapons();

		if (SaveSystem.instance != null)
		{
            moveSpeed = PlayerStatLogic.instance.moveSpeed[SaveSystem.instance.saveData.playerSpeedLevel].value;
            pickupRange = PlayerStatLogic.instance.pickupRange[SaveSystem.instance.saveData.playerPickupRangeLevel].value;
            maxWeapons = Mathf.RoundToInt(PlayerStatLogic.instance.maxWeapons[SaveSystem.instance.saveData.playerMaxWeaponsLevel].value);
        }
		else
		{
            moveSpeed = PlayerStatLogic.instance.moveSpeed[0].value;
            pickupRange = PlayerStatLogic.instance.pickupRange[0].value;
            maxWeapons = Mathf.RoundToInt(PlayerStatLogic.instance.maxWeapons[0].value);
        }
		

		if(activeWeapons.Count != 0)
		{
			usedWeapon = activeWeapons[0];
			usedWeapon.gameObject.SetActive(true);

			for (int i = 1; i < activeWeapons.Count; i++)
			{
				activeWeapons[i].gameObject.SetActive(false);
			}
		}
	}

    private void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Left Stick Horizontal");
        moveInput.y = -Input.GetAxisRaw("Left Stick Vertical");

        moveInput.Normalize();

        if (IsClearToMove(moveInput))
        {
            transform.position += moveInput * moveSpeed * Time.deltaTime;
        }

        if (moveInput != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Dright") || Input.GetKeyDown(KeyCode.RightArrow) && activeWeapons.Count != 0)
        {
            usedWeaponIndex++;

            if (usedWeaponIndex >= activeWeapons.Count)
            {
                usedWeaponIndex = 0;
            }

            usedWeapon.gameObject.SetActive(false);
            usedWeapon = activeWeapons[usedWeaponIndex];
            usedWeapon.gameObject.SetActive(true);
        }
        else if (Input.GetButtonDown("Dleft") || Input.GetKeyDown(KeyCode.LeftArrow) && activeWeapons.Count != 0)
        {
            usedWeaponIndex--;

            if (usedWeaponIndex < 0)
            {
                usedWeaponIndex = activeWeapons.Count - 1;
            }

            usedWeapon.gameObject.SetActive(false);
            usedWeapon = activeWeapons[usedWeaponIndex];
            usedWeapon.gameObject.SetActive(true);
        }
    }

    private bool IsClearToMove(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, 0.2f, obstacleLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                return false; 
            }
        }

        return true; 
    }

    public void AddWeapon(int weaponNumber)
	{
		if (weaponNumber < unassignedWeapons.Count)
		{
			assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            if (unassignedWeapons[weaponNumber].gameObject.tag == "activeWeapon")
            {
                activeWeapons.Add(unassignedWeapons[weaponNumber]);
            }
			else
			{
                unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            }

            unassignedWeapons.RemoveAt(weaponNumber);
		}
	}

	public void AddWeapon(Weapon weaponToAdd)
	{
		assignedWeapons.Add(weaponToAdd);

        if (weaponToAdd.gameObject.tag == "activeWeapon")
        {
            activeWeapons.Add(weaponToAdd);
			if (activeWeapons.Count == 1)
			{
                weaponToAdd.gameObject.SetActive(true);
				usedWeapon = activeWeapons[0];
            }
        }
		else
		{
            weaponToAdd.gameObject.SetActive(true);
        }

        unassignedWeapons.Remove(weaponToAdd);
	}

	public void SetAssignedWeapons()
	{
		for (int i = 0; i < unassignedWeapons.Count; i++)
		{
			if (assignedWeapons.Contains(unassignedWeapons[i]))
			{
				unassignedWeapons.Remove(unassignedWeapons[i]);
			}
		}

		for (int i = 0; i < assignedWeapons.Count; i++)
		{
			if (!activeWeapons.Contains(assignedWeapons[i]))
			{
				if (assignedWeapons[i].gameObject.tag == "activeWeapon")
				{
					activeWeapons.Add(assignedWeapons[i]);
				}
			}

			assignedWeapons[i].gameObject.SetActive(true);
		}
	}
}
