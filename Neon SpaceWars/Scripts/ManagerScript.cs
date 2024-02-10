using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
/*    Vector2 _pPos;
    Transform _emptlyPos, desiredTransform;*/

    public GameObject _scenario;
/*    GameObject[] _activeScenarios;

    Transform[] _scenarioPos;

    int multiplicatorIndex = 1, generalIndex;
    float _indexPosX, _indexPosY;

    bool left, right, up, down, leftUp, leftDown, rightUp, rightDown, allClear;*/

    [System.NonSerialized]
    public int maxCount = 50, currentCount = 0, kills;


    void Start()
    {
        //_pPos = GameObject.FindWithTag("Player").transform.position;

        /*for(int x = 0; x < 9; x++)
        {
            ProceduralGeneration();
        }*/

        /*        for (int x = 0; x < 100; x++)
                {
                    OldInitialGeneration();
                }*/

        InitialGeneration();
    }

/*    void Update()
    {
        //ProceduralGeneration();

        if (currentCount >= maxCount)
        {
            print("Max count of enemies reached");
        }
    }*/

    /*void OldInitialGeneration()
    {
        //desiredTransform.parent = null;
        Vector3 desiredPos = _scenario.transform.GetChild(0).position;
        //print(_scenario.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size);
        float initialHeight = 150, initialWidth = 150;
        float height = initialHeight * multiplicatorIndex, width = initialWidth * multiplicatorIndex;
        string name = "";

        
        

*//*        if (!left)
        {
            allClear = false;
            left = true;
            desiredPos.x -= width;
            desiredPos.y = 0;
            name = "Left";
        }
        else if (!right)
        {
            allClear = false;
            right = true;
            desiredPos.x += width;
            desiredPos.y = 0;
            name = "Right";
        }
        else if (!up)
        {
            allClear = false;
            up = true;
            desiredPos.y += height;
            desiredPos.x = 0;
            name = "Up";
        }
        else if (!down)
        {
            allClear = false;
            down = true;
            desiredPos.y -= height;
            desiredPos.x = 0;
            name = "Down";
        }//////
        else if (!leftUp)
        {
            allClear = false;
            leftUp = true;
            desiredPos.x -= width;
            desiredPos.y += height;
            name = "Left Up";
        }
        else if (!rightUp)
        {
            allClear = false;
            rightUp = true;
            desiredPos.x += width;
            desiredPos.y += height;
            name = "Right Up";
        }
        else if (!leftDown)
        {
            allClear = false;
            leftDown = true;
            desiredPos.y -= height;
            desiredPos.x -= width;
            name = "Left Down";
        }
        else if (!rightDown)
        {
            allClear = false;
            rightDown = true;
            desiredPos.y -= height;
            desiredPos.x += width;
            name = "Right Down";
        }
        else
        {
            allClear = true;
            multiplicatorIndex++;

            left = false;
            right = false;
            up = false;
            down = false;
            leftUp = false;
            rightUp = false;
            leftDown = false;
            rightDown = false;
        }*//*

        //desiredTransform.position = desiredPos;
        //print(desiredTransform.position);
        if (!allClear)
        {
            generalIndex++;
            GameObject clone = Instantiate(_scenario, desiredPos, Quaternion.identity);
            clone.name = name;
        }
    }*/

    void InitialGeneration()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Vector3 desiredPos = _scenario.transform.GetChild(0).position;
                float initialHeight = 150 * y, initialWidth = 150 * x;
                float height = initialHeight, width = initialWidth;
                desiredPos = new Vector2(width, height);

                GameObject clone = Instantiate(_scenario, desiredPos, Quaternion.identity);
                clone.transform.parent = gameObject.transform;
            }
        }
    }

    void ProceduralGeneration()
    {
        
/*        //if (_pPos.x > )
        //GetComponent<SpriteRenderer>().bounds.size
        _indexPosX = _index * _scenario.transform.localScale.x;
        _indexPosY = _index * _scenario.transform.localScale.y;
        //_scenarioPos[_index].position = new Vector2(_indexPosX, _indexPosY);
        _index++;

        _emptlyPos.position = new Vector2(_indexPosX, _indexPosY); ;

        GameObject.Instantiate(_scenario, _emptlyPos);*/
    }
}
