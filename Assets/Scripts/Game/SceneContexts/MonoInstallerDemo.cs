using System.ComponentModel;
using Characters;
using Characters.Controllers;
using Game.Configurations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Zenject;
using Character = Characters.Character;


public class MonoInstallerDemo : MonoInstaller
{
    [SerializeField] 
    private Character character;
    [SerializeField] 
    private PlayerInput playerInput;
    [SerializeField] 
    private CharactersGeneralConfiguration charactersGeneralConfiguration;
    
    public override void InstallBindings()
    {
        Container.Bind<CharactersGeneralConfiguration>().FromInstance(charactersGeneralConfiguration).AsSingle();
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
        BindCharacterAssociatedEntities();
        BindControllers();
    }
    private void BindCharacterAssociatedEntities()
    {
        Container.BindInterfacesAndSelfTo<AccelerationModule>().FromNew().AsSingle();
        Container.Bind<Character>().FromInstance(character).AsSingle();
    }
    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
    }
}