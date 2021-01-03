using UnityEngine;
using System.Collections.Generic;

class ObjectPool
{
    private List<FloatingObject> objects;

    public ObjectPool(int count, FloatingObject source) {

        objects = new List<FloatingObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(source);
        }

    }

    public void Initialize(int count, FloatingObject source)
    {

        objects = new List<FloatingObject>();

        for (int i = 0; i < count; i++)
        {

            AddObject(source);
        }

    }

    private void AddObject(FloatingObject source) {

        GameObject temp = GameObject.Instantiate(source.gameObject);
        temp.name = source.name;
        objects.Add(temp.GetComponent<FloatingObject>());
        temp.SetActive(false);
    }

    public FloatingObject GetObject() {

        for (int i = 0; i < objects.Count; i++) {

            if (objects[i].gameObject.activeInHierarchy == false) {

                objects[i].gameObject.SetActive(true);

                return objects[i];
            }
        }

        AddObject(objects[0]);
        return objects[objects.Count - 1];
    }
   



}

