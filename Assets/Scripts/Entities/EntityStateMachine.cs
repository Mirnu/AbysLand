using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class EntityStateMachine
{
    protected EntityStatsModel _Stats;
    protected Entity _EntityModel;

    public EntityStateMachine(Entity entity, EntityStatsModel stats)
    {
        _EntityModel = entity;
        _Stats = stats;
    }

    public abstract void Initialize();
}
