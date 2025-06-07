using UnityEngine;

public class AnimationHandler
{
    // Update is called once per frame
    public void UpdateFrame()
    {
        Debug.Log(GlobalHandler.Instance.sceneHandler.bouncingBall.velocity.y);
        GlobalHandler.Instance.sceneHandler.bouncingBall.animator.SetFloat("SpeedY", GlobalHandler.Instance.sceneHandler.bouncingBall.velocity.y);

        if (GlobalHandler.Instance.sceneHandler.bouncingBall.velocity.x >= 0)
        {
            GlobalHandler.Instance.sceneHandler.bouncingBall.circleObject.transform.rotation = (Quaternion.Euler(0, 0, 0));
        }
        else 
        {
            GlobalHandler.Instance.sceneHandler.bouncingBall.circleObject.transform.rotation = (Quaternion.Euler(0, 180f, 0));
        }
    }
}
