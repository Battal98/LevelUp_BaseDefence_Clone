
using UnityEngine;
public interface IStackable
{
    void SetInit(Transform initTransform, Vector3 position);

    void SetVibration(bool isVibrate);

    void SetSound();

    void EmitParticle();

    void PlayAnimation();

    GameObject SendToStack();
    void SendPosition(Transform transform);
}