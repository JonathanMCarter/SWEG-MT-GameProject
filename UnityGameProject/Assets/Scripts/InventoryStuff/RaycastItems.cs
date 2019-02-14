﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItems : MonoBehaviour
{
	public float Range;
	public GameObject UI;
	private bool UIOpen;

	private PickupUIText PickupScript;

    // Anything to do with NPC's in this script was done by Ed so talk to him

    public NPCInteractionScrpt NPCInteractionScrpt; // the main controll script for npc dialogue interactions
    public string NameofNPCHit; // Used to send the name of the NPC to the NPC interaction script

	private void Start()
	{
		PickupScript = GameObject.Find("PickupPopup").GetComponent<PickupUIText>();

		InRangeUI();
	}


	private void FixedUpdate()
	{
		RaycastHit Hit;
        


		if (Physics.Raycast(transform.position, transform.forward, out Hit, Range)) // Check to see if raycast hits anything
		{
            // Check to see if hit object is a npc or item
            if (Hit.transform.tag == "NPC") // if npc do this
            {
                NameofNPCHit = Hit.transform.name; // getting the name of the NPC hit 

                if (Input.GetButtonDown("Pickup")) // E Key
                {
                    NPCInteractionScrpt.StartInteraction();
                }
            }
            else // if item do this
            {
				if (Input.GetButtonDown("Pickup")) // E Key
                {
					AddHitToInv(Hit);
				}

				if (Hit.transform.gameObject.GetComponent<Item>())
				{
					if (UI.activeInHierarchy)
					{
						PickupScript.SetText(Hit.transform.gameObject.GetComponent<Item>().ItemName);
					}
				}
				else
				{
					if (UI.activeInHierarchy)
					{
						PickupScript.BlankText();
					}
				}

                UIOpen = true;
            }

		}
		else
		{
			UIOpen = false;
		}

		InRangeUI(); // if looking at an item then show ui
	}



	private void AddHitToInv(RaycastHit Hit)
	{
		if (Hit.transform.gameObject.GetComponent<Item>())
		{
			Hit.transform.gameObject.GetComponent<Item>().AddToInv(Hit.transform.gameObject.GetComponent<Item>());
			Hit.transform.gameObject.transform.SetParent(GameObject.Find("InvItems").transform);
			Hit.transform.gameObject.SetActive(false);
			PickupScript.BlankText();
		}
	}


	private void InRangeUI()
	{
		if (UIOpen)
		{
			UI.SetActive(true);
		}

		if (!UIOpen)
		{
			UI.SetActive(false);
		}
	}
}
