using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DogSounds : MonoBehaviour
{
    public GameObject DiggingSoud;
    public GameObject HatSound;
    public GameObject SpeedSound;
    public GameObject PurseSound;
    public GameObject BoneSound;
    public GameObject GoldenBoneSound;
    // Start is called before the first frame update
    void Start()
    {
        Hozain_Give.ActivateSpeedBoostSound += SpeedBoostSound;
        Klad_Up.Digging += OnDiging;
        Klad_Up.ActivateHatSound += OnHatFound;
        Klad_Up.ActivatePurseSound += OnPurseFound;
        ScoreAnimation.ActivateBoneSound += OnBoneFound;
        ScoreAnimation.ActivateGoldenBoneSound += OnGoldenBoneFound;
    }

    IEnumerator SpeedBoostSound()
    {
        SpeedSound.SetActive(true);
        yield return new WaitForSeconds(3);
        SpeedSound.SetActive(false);
    }

    IEnumerator OnBoneFound()
    {
        yield return new WaitForSeconds(1.5f);
        BoneSound.SetActive(true);
        yield return new WaitForSeconds(3);
        BoneSound.SetActive(false);
    }

    IEnumerator OnGoldenBoneFound()
    {
        yield return new WaitForSeconds(1.5f);
        GoldenBoneSound.SetActive(true);
        yield return new WaitForSeconds(3);
        GoldenBoneSound.SetActive(false);
    }

    IEnumerator OnPurseFound()
    {
        yield return new WaitForSeconds(1.5f);
        PurseSound.SetActive(true);
        yield return new WaitForSeconds(3);
        PurseSound.SetActive(false);
    }

    IEnumerator OnHatFound()
    {
        yield return new WaitForSeconds(1.5f);
        HatSound.SetActive(true);
        yield return new WaitForSeconds(3);
        HatSound.SetActive(false);
    }

    IEnumerator OnDiging()
    {
        DiggingSoud.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        DiggingSoud.SetActive(false);
    }
}
