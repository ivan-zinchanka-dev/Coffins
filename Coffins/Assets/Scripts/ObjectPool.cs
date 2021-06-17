using UnityEngine;
using System.Collections.Generic;

class ObjectPool
{
    private List<FloatingObject> _objects;

    public ObjectPool(int count, FloatingObject source) {

        _objects = new List<FloatingObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(source);
        }
    }

    public void Initialize(int count, FloatingObject source)
    {
        _objects = new List<FloatingObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(source);
        }
    }

    private void AddObject(FloatingObject source) {

        GameObject temp = GameObject.Instantiate(source.gameObject);
        temp.name = source.name;
        _objects.Add(temp.GetComponent<FloatingObject>());
        temp.SetActive(false);
    }

    public FloatingObject GetObject() {

        for (int i = 0; i < _objects.Count; i++) {

            if (_objects[i].gameObject.activeInHierarchy == false) {

                _objects[i].gameObject.SetActive(true);
                return _objects[i];
            }
        }

        AddObject(_objects[0]);
        return _objects[_objects.Count - 1];
    }
   
}

