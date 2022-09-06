using UnityEngine;
using DG.Tweening;
using Enums;

public class GateAnimationControllers : MonoBehaviour
{
    [SerializeField]
    private Vector3 OpenRotateAngle = Vector3.zero;
    [Space]
    [SerializeField]
    private Vector3 CloseRotateAngle = Vector3.zero;
    public void ChangeAnimationState(GateState state)
    {
        if (state != GateState.Close)
            DoCloseGateAnim();
        else
            DoOpenGateAnim();
    }

    private void DoCloseGateAnim()
    {
        DOTween.Kill(this.gameObject, false);
        this.transform.DOLocalRotate(OpenRotateAngle,1f);
        Debug.Log("Close");
    }

    private void DoOpenGateAnim()
    {
        DOTween.Kill(this.gameObject, false);
        this.transform.DOLocalRotate(CloseRotateAngle, 1f);
        Debug.Log("Open");
    }
}
