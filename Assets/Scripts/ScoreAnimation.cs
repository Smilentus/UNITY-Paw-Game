using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreAnimation : MonoBehaviour
{
    private GameObject data;

    public GameObject dog;
    public GameObject bone;
    public GameObject boneGold;

    TreasureController treasureController;

    public Animator k_Animator;
    public Animator kg_Animator;
    public GameObject scorePrefab;
    public static bool scoreGo = false;

    public static event BoneSound ActivateBoneSound;
    public delegate IEnumerator BoneSound();

    public static event BoneGoldenSound ActivateGoldenBoneSound;
    public delegate IEnumerator BoneGoldenSound();

    void Start()
    {
        dog = GameObject.Find("Dog");
        data = GameObject.Find("Data");
        treasureController = FindObjectOfType<TreasureController>();
    }

    public void BoneAnimation()
    {
        StartCoroutine(ActivateBoneSound());
        bone.transform.position = new Vector3(dog.transform.position.x, dog.transform.position.y + 15, dog.transform.position.z);
        bone.SetActive(true);
        //k_Animator.SetBool("Yes", true); // Тригер анимации
        PoyavlenieChastic();
        Invoke("BoneAnimationEnd", 1.5f);
    }

    public void GoldenBoneAnimation()
    {
        StartCoroutine(ActivateGoldenBoneSound());
        boneGold.transform.position = new Vector3(dog.transform.position.x, dog.transform.position.y + 15, dog.transform.position.z);
        boneGold.SetActive(true);
        //kg_Animator.SetBool("Yes", true); // Тригер анимации
        PoyavlenieChastic();
        Invoke("GoldenBoneAnimationEnd", 1.5f);
    }

    public void BoneAnimationEnd()
    {
        //k_Animator.SetBool("Yes", false);
        bone.SetActive(false);
    }

    public void GoldenBoneAnimationEnd() // Вырубаем анимацию и все прячим
    {
        //kg_Animator.SetBool("Yes", false);
        boneGold.SetActive(false);
    }

    public void PoyavlenieChastic() // Появление префаба очков
    {
        scorePrefab.transform.position = new Vector3(dog.transform.position.x, dog.transform.position.y + 15, dog.transform.position.z);
        GameObject particles = Instantiate(scorePrefab);
        scoreGo = true;
        Destroy(particles, 1.5f);
    }
}
