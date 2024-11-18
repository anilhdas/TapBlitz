using System.Collections;
using UnityEngine;

namespace TapBlitzUtils
{
    public static class TimeUtil
    {
        public static IEnumerator WaitForSeconds(float seconds)
        {
            float timer = 0.0f;

            while(timer < seconds)
            {
                yield return null;
                timer += Time.deltaTime;
            }
        }
    }
}