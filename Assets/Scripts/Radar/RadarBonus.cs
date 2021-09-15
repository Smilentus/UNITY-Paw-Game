using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBonus : MonoBehaviour
{
	public GameObject extraArrowObject;

    public GameObject[] radarArrows;
    public GameObject[] radarShadowArrows;

	public Collider foundedTreasure;

	public bool isBonusArrow;
	[SerializeField]
	private int maxArrows = 3;
	[SerializeField]
	private int arrowsCounter;

    public int ArrowsCounter { 
		get 
		{ 
			return arrowsCounter;
		}
		set 
		{
			if (isBonusArrow)
				maxArrows = 4;
			else
				maxArrows = 3;

			arrowsCounter = value;

			if (arrowsCounter == 0) foundedTreasure = null;

			if (arrowsCounter < 0) arrowsCounter = 0;
			else if (arrowsCounter > maxArrows) arrowsCounter = maxArrows;
		} 
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
			ActivateExtraArrow();
		if (Input.GetKeyDown(KeyCode.N))
			DeActivateExtraArrow();
	}

	public void ActivateExtraArrow()
    {
		isBonusArrow = true;
		maxArrows = 4;
		extraArrowObject.SetActive(true);
	}
	
	public void DeActivateExtraArrow()
    {
		isBonusArrow = false;
		maxArrows = 3;
		extraArrowObject.SetActive(false);
	}

    public bool IsFounded(Collider check)
	{
		if (foundedTreasure == null)
			foundedTreasure = check;	

		if (check == foundedTreasure)
			return true;
		else
			return false;
	}

	public void NullProperties()
	{
		foundedTreasure = null;
		ArrowsCounter = 0;
		HideAllArrows();
		HideAllShadows();
	}

    public void HideAllArrows()
	{
        for (int i = 0; i < radarArrows.Length; i++)
		{
            radarArrows[i].SetActive(false);
		}
	}

    public void SetRadarArrows()
    {
        HideAllArrows();

        for (int i = 0; i < arrowsCounter; i++)
		{
            radarArrows[i].SetActive(true);
		}
    }

	public void SetRadarShadow()
	{
		HideAllShadows();

		for (int i = 0; i < arrowsCounter; i++)
		{
			radarShadowArrows[i].SetActive(true);
		}
	}

	public void HideAllShadows()
	{
		for (int i = 0; i < radarShadowArrows.Length; i++)
		{
			radarShadowArrows[i].SetActive(false);
		}
	}
}