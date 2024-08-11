using System.Collections;

namespace Utils
{
    /// <summary>
    /// A helper object to be in sync with unity main thread.
    /// Not to spend time on writing proper synchronization types
    /// </summary>
    public class CoroutineRunner
    {
        public void StartCoroutine(IEnumerator coroutine)
        {
            GameManager.Instance.StartCoroutine(coroutine);
        }
    }
}