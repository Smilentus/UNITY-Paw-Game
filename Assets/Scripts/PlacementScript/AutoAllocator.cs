using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAllocator : MonoBehaviour
{
    private TreasureAllocator treasureAllocator;

    [Header("Префаб точки с сокровищем")]
    public GameObject pointPref;

    // Стандартные значения
    [Space(10)]
    [Header("Длина")]
    public float areaWidth = 340;
    [Header("Ширина")]
    public float areaHeight = 390;

    public static int currentPointsAmmountOnTheField; // Текущее количество точек на уровне
    public static int maximumPointsOnTheField;        // Максимум точек в один момент

    List<Bounds> spawnAreas = new List<Bounds>();
    
    Bounds selectedArea;
    bool allignAtField;
    
    List<GameObject> spawnPoints;
    Bounds spawnPointBounds;
    
    GameObject[] collisonObjects;
    List<Bounds> staticBounds = new List<Bounds>();

    GameObject dog;
    
    Vector3 debugVisionOffset = new Vector3(0, 1, 0);
    Vector3 BetweenVector = new Vector3(65, 0, 65); // Сказали по стандарту 65 0 65

    Vector3 spawnAreaCenter;
    Vector3 spawnAreaSize;

    private void Start()
    {
        treasureAllocator = FindObjectOfType<TreasureAllocator>();

        spawnAreaCenter = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        spawnAreaSize = new Vector3(Width(), 0, Height());

        dog = GameObject.FindGameObjectWithTag("Dog");

        InitializeLvlParameters();

        float start = Time.realtimeSinceStartup;
        Debug.Log("START GENERATING: " + start);
        OnAutoLocateClick();
        float end = Time.realtimeSinceStartup;
        Debug.Log("END GENERATING: " + end);
        Debug.Log("Passed time: " + (end - start));

        Klad_Up.OnSpawnPointFound += OnSpawnPointFound;
    }

    void OnSpawnPointFound(GameObject spawnPoint){
        currentPointsAmmountOnTheField--;
        spawnPoints.Remove(spawnPoint);

        if (currentPointsAmmountOnTheField == 2)
        {
            spawnAdditionalSpawnPoints();
        }
        
        if (currentPointsAmmountOnTheField == 0)
        {
            // Смена уровня или ещё что-то такое
            Debug.Log("Получается КЛАДЫ ЗАКОНЧИЛИСЬ :(");
        }
    }

    private void InitializeLvlParameters()
	{
        float lvlSquare = Width() * Height();
        
        if (lvlSquare >= 86000 && lvlSquare < 103000)
		{
            currentPointsAmmountOnTheField = 5;
            maximumPointsOnTheField = 5;
            BetweenVector = new Vector3(65, 0, 65);
        }
        else if (lvlSquare < 120000)
        {
            currentPointsAmmountOnTheField = 6;
            maximumPointsOnTheField = 6;
            BetweenVector = new Vector3(65, 0, 65);
        }
        else if (lvlSquare < 138000)
        {
            currentPointsAmmountOnTheField = 7;
            maximumPointsOnTheField = 7;
            BetweenVector = new Vector3(65, 0, 65);
        }
        else if (lvlSquare < 155000)
        {
            currentPointsAmmountOnTheField = 8;
            maximumPointsOnTheField = 8;
            BetweenVector = new Vector3(65, 0, 65);
        }
        else if (lvlSquare < 173000)
        {
            currentPointsAmmountOnTheField = 9;
            maximumPointsOnTheField = 9;
            BetweenVector = new Vector3(65, 0, 65);
        }
        else if (lvlSquare < 190000)
        {
            currentPointsAmmountOnTheField = 10;
            maximumPointsOnTheField = 10;
            BetweenVector = new Vector3(70, 0, 70);
        }
        else if (lvlSquare < 208000)
        {
            currentPointsAmmountOnTheField = 11;
            maximumPointsOnTheField = 11;
            BetweenVector = new Vector3(70, 0, 70);
        }
        else if (lvlSquare < 225000)
        {
            currentPointsAmmountOnTheField = 12;
            maximumPointsOnTheField = 12;
            BetweenVector = new Vector3(70, 0, 70);
        }
        else if (lvlSquare < 243000)
        {
            currentPointsAmmountOnTheField = 13;
            maximumPointsOnTheField = 13;
            BetweenVector = new Vector3(70, 0, 70);
        }
        else if (lvlSquare >= 243000)
        {
            currentPointsAmmountOnTheField = 14;
            maximumPointsOnTheField = 14;
            BetweenVector = new Vector3(70, 0, 70);
        }

        Debug.Log("[!] Настройки уровня [!]");
        Debug.Log("Площадь уровня: " + lvlSquare);
        Debug.Log("Количество точек спавна: " + currentPointsAmmountOnTheField);
        Debug.Log("Расстояние между точками: " + BetweenVector.x);
    }

    public void OnAutoLocateClick()
    {
        spawnAreas.Add(new Bounds(spawnAreaCenter, spawnAreaSize));
        spawnPointBounds = new Bounds(dog.transform.position, BetweenVector);

        MarkupSpawnAreas(spawnPointBounds.center);

        SetsStaticObjectsBounds();

        spawnPoints = new List<GameObject>(currentPointsAmmountOnTheField);

        var collider = pointPref.GetComponent<CircleCollider2D>();

        spawnPointBounds = new Bounds(Vector3.zero, BetweenVector);

        for (int i = 0; i < currentPointsAmmountOnTheField; i++)
        {
            CreateSpawnPoint(); 
            AutoLocateSpawnPoint(i);
        }
    }

    void SetsStaticObjectsBounds()
    {
        collisonObjects = GameObject.FindGameObjectsWithTag("CollisionObject");

        for (int i = 0; i < collisonObjects.Length; i++)
        {
            spawnPointBounds = new Bounds(collisonObjects[i].GetComponent<BoxCollider>().bounds.center,
                collisonObjects[i].GetComponent<BoxCollider>().bounds.size);

            MarkupSpawnAreas(spawnPointBounds.center);
        }

        staticBounds.AddRange(spawnAreas);
    }

    void spawnAdditionalSpawnPoints()
    {
        Debug.Log("[ДОСЕИВАНИЕ]");
        float start = Time.realtimeSinceStartup;
        Debug.Log("START: " + start);

        spawnAreas.Clear();
        spawnAreas.AddRange(staticBounds);

        spawnPointBounds = new Bounds(GameObject.Find("Dog").transform.position, BetweenVector);
        MarkupSpawnAreas(spawnPointBounds.center);
        foreach (var oldSpawnPoint in spawnPoints)
        {
            MarkupSpawnAreas(oldSpawnPoint.transform.position);
        }

        int additionalAmount = 4;

        if (currentPointsAmmountOnTheField + additionalAmount >= treasureAllocator.storageCount)
        {
            additionalAmount = treasureAllocator.storageCount - currentPointsAmmountOnTheField;
        }

        for (int i = 2; i < 2 + additionalAmount; i++)
        {
            CreateSpawnPoint();
            AutoLocateSpawnPoint(i);
        }
        currentPointsAmmountOnTheField += additionalAmount;

        // Проверяем видит ли игрок все клады
        if (FindObjectOfType<TreasureController>().isShowingThrough)
            FindObjectOfType<TreasureAllocator>().ShowAllTreasures();

        Debug.Log("Засеяли " + additionalAmount + " шт. клада");
        Debug.Log("На поле " + currentPointsAmmountOnTheField + " точек");

        float end = Time.realtimeSinceStartup;
        Debug.Log("END: " + end);
        Debug.Log("Passed time: " + (end - start));
    }

    float Width()
    {
        return areaWidth;
    }

    float Height()
    {
        return areaHeight;
    }

    void CreateSpawnPoint()
    {
        var spawnPoint = Instantiate(pointPref);
        spawnPoints.Add(spawnPoint);
    }

    void AutoLocateSpawnPoint(int index)
    {
        SelectArea();
        var x = UnityEngine.Random.Range(selectedArea.min.x, selectedArea.max.x);
        var z = UnityEngine.Random.Range(selectedArea.min.z, selectedArea.max.z);
        spawnPoints[index].transform.position = new Vector3(x, pointPref.transform.position.y, z);
        MarkupSpawnAreas(new Vector3(x, 0, z));
    }

    void SelectArea()
    {
        var areasWorkingList = CopyList(spawnAreas);
        for (int i = 0; i < 100; i++)
        {
            var areaNum = UnityEngine.Random.Range(0, areasWorkingList.Count);
            var randomArea = areasWorkingList[areaNum];

            selectedArea = new Bounds(randomArea.center, randomArea.size);
            var isAreaAppropriate = CheckAndAdjustSpawnArea();
            if (!isAreaAppropriate) areasWorkingList.Remove(randomArea);
            else break;
        }
    }

    List<T> CopyList<T>(List<T> list)
    {
        return new List<T>(list);
    }

    bool CheckAndAdjustSpawnArea()
    {
        var canStandHorizontally = selectedArea.size.x >= spawnPointBounds.size.x;
        var canStandVertically = selectedArea.size.z >= spawnPointBounds.size.z;

        if (!canStandHorizontally || !canStandVertically)
        {
            return false;
        }
        selectedArea.Expand(-spawnPointBounds.size);
        return true;
    }

    void MarkupSpawnAreas(Vector3 position)
    {
        float x = position.x;
        float z = position.z;

        var occupiedArea = new Bounds(position, spawnPointBounds.size);
        var spawnAreasCopy = CopyList(spawnAreas);
        int c = 0;

        for (int i = 0; i < spawnAreasCopy.Count; i++)
        {
            var area = spawnAreasCopy[i];

            if (!AreBoundsOverlap(area, occupiedArea)) continue;

            spawnAreas.Remove(area);

            SplitSpawnArea(area, occupiedArea);

            c++; if (c > 200000) break;
        }
    }

    bool AreBoundsOverlap(Bounds initial, Bounds occup)
    {
        if (initial == selectedArea) return true;
        var minMaxX = Mathf.Min(initial.max.x, occup.max.x);
        var maxMinX = Mathf.Max(initial.min.x, occup.min.x);
        var minMaxY = Mathf.Min(initial.max.z, occup.max.z);
        var maxMinY = Mathf.Max(initial.min.z, occup.min.z);

        var overlap = minMaxX > maxMinX && minMaxY > maxMinY;

        //Debug.Log($"old {initial.min} {initial.max}, new {occup.min} {occup.max},  {overlap}");
        return overlap;
    }

    void SplitSpawnArea(Bounds initialArea, Bounds occupiedArea)
    {
        CreateSubarea(initialArea, initialArea.max.z, occupiedArea.max.z, false);
        CreateSubarea(initialArea, initialArea.max.x, occupiedArea.max.x, true);
        CreateSubarea(initialArea, occupiedArea.min.z, initialArea.min.z, false);
        CreateSubarea(initialArea, occupiedArea.min.x, initialArea.min.x, true);
    }

    void CreateSubarea(Bounds initArea, float max, float min, bool isVertical)
    {
        if (max - min < 1) return;
        var subArea = new Bounds();
        if (isVertical)
        {
            subArea.center = new Vector3((max + min) / 2, 0, initArea.center.z);
            subArea.size = new Vector3(max - min, 0, initArea.size.z);
        }
        else
        {
            subArea.center = new Vector3(initArea.center.x, 0, (max + min) / 2);
            subArea.size = new Vector3(initArea.size.x, 0, max - min);
        }
        spawnAreas.Add(subArea);
        //Debug.Log($"area created {subArea.min} {subArea.max}");
    }

	void OnDrawGizmos()
	{
		// Отрисовка мини полей для спавна
		var colors = new Color[]
		{
			Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow, Color.black
		};

		int c = 0;

		foreach (var area in spawnAreas)
		{
			Gizmos.color = colors[c];
			Gizmos.DrawWireCube(area.center, area.size);
			c++;
			if (c == colors.Length) c = 0;
		}

		// Отрисовка поля для засеивания
		var debugCenter = new Vector3(this.transform.position.x, 0, this.transform.position.z);
		var debugSize = new Vector3(Width(), 0, Height()) + debugVisionOffset;
		Gizmos.color = new Color(255, 0, 0, 0.5f);
		Gizmos.DrawCube(debugCenter, debugSize);
	}
}