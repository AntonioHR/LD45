using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;

public class FrameWaiterHelper : MonoBehaviour
{
    public static FrameWaiterHelper Instance{ get; private set; }


    public void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static async Task WaitFrame()
    {
        TaskCompletionSource<object> task = new TaskCompletionSource<object>();

        Instance.StartCoroutine(Instance.WaitFrame(task));

        await task.Task;
    }

    private IEnumerator WaitFrame(TaskCompletionSource<object> task)
    {
        yield return new WaitForEndOfFrame();
        task.TrySetResult(null);
    }

    public static async Task WaitTime(float seconds)
    {
        TaskCompletionSource<object> task = new TaskCompletionSource<object>();

        Instance.StartCoroutine(Instance.WaitSeconds(task, seconds));

        await task.Task;
    }

    private IEnumerator WaitSeconds(TaskCompletionSource<object> task, float time)
    {
        yield return new WaitForSeconds(time);
        task.TrySetResult(null);
    }

}
public static class Wait
{
   public static async Task For(float seconds)
   {
       try {
            await FrameWaiterHelper.WaitTime(seconds);
       } catch (Exception e) {
           Debug.LogError(e);
       }
    }
}
