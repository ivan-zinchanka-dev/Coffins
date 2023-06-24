using System.Collections;
using UnityEngine;

namespace ObjectsPool
{
    public class FloatingObject : MonoBehaviour
    {       
        public IEnumerator ReturnToPool(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

    }
}
