using UnityEngine;
using Extentions;
public interface IStackable
{
    void SetInit(Transform initTransform, Vector3 position);
    void SetVibration(bool isVibrate);
    void SetSound();
    void EmitParticle();
    void PlayAnimation();
    GameObject SendToStack();
    void SendStackable(Stackable stackableMoney);
    bool IsSelected { get; set; }
    bool IsCollected { get; set; }
}