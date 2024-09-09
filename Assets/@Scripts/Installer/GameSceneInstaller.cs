using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reflex;
using Reflex.Core;

public class GameSceneInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private CameraController _cameraController;

    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(_ => _cameraController);
    }
}
