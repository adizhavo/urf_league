using UnityEngine;
using System.Collections;

namespace URFLeague.Util
{
    public class AutoDisable : MonoBehaviour
    {
        public float lifeTime;
        public bool destroy;

        private void OnEnable()
        {
            StartCoroutine(Wait());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(lifeTime);

            if (destroy) Destroy(gameObject);
            else gameObject.SetActive(false);
        }
    }
}
