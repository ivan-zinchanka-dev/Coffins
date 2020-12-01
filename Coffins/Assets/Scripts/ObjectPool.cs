using UnityEngine;
using System.Collections;
using System.Collections.Generic;


class ObjectPool
{
    private int size;
    private LinkedList<FloatingObject> objects;

    private class FloatingObject
    {
        public bool isAvail;
        public bool isInit;
        public GameObject gameObj;
        public int id;

        static int objectsCount = 0;

        public FloatingObject(GameObject obj)
        {
            gameObj = obj;
            isAvail = true;
            isInit = false;
        }

    }


    public ObjectPool(int size)
    {
        this.size = size;
    }

    public void AddObject(GameObject obj)
    {
        objects.AddLast(new FloatingObject(obj));
    }
    
    public void Instantiate(Vector2 position)
    {
        foreach(var it in objects)
        {
            if (!it.isInit)
            {
                GameObject.Instantiate(it.gameObj, position, Quaternion.identity);
                it.isInit = true;
                it.isAvail = false;

            }
            else if (it.isAvail)
            {
                it.gameObj.transform.position = position;
                it.isAvail = false;
                break;
            }
        }

    }




}

