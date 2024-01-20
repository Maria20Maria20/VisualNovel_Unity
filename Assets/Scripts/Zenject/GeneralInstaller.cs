using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GeneralInstaller : MonoInstaller
{
    [SerializeField] private DialoguesInstaller dialoguesInstaller;
    public override void InstallBindings()
    {
        BindDialoguesInstaller();
    }

    private void BindDialoguesInstaller()
    {
        Container.Bind<DialoguesInstaller>().FromInstance(dialoguesInstaller).AsSingle();
    }
}
