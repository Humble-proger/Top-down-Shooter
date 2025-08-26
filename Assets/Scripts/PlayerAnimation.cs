using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerShooting _playerShooting;

    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Shoot = Animator.StringToHash("Shoot");

    private void OnEnable()
    {
        _playerMover.IsRunning += RunAnimation;
        _playerShooting.Shoot += ShootAnimation;
    }

    private void OnDisable()
    {
        _playerMover.IsRunning -= RunAnimation;
        _playerShooting.Shoot -= ShootAnimation;
    }

    private void ShootAnimation()
    {
        _gunAnimator.SetTrigger(Shoot);
    }

    private void RunAnimation(bool value) {
        _playerAnimator.SetBool(Run, value);
    }

}