using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Interfaces;

public class BossAttackState : IState
{
    private bool _attackRange;
    public bool InPlayerAttackRange() => _attackRange;

    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        throw new NotImplementedException();
    }

}
