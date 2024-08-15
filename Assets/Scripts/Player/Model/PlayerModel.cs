using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel 
    {
        private readonly Rigidbody _rb;
        private readonly Animator _animator;
        private readonly Transform _transform;
        private readonly Animator _armAnimator;

        public PlayerModel(Rigidbody rigidbody, Animator animator, 
            Transform transform, Animator armAnimator) 
        { 
            _rb = rigidbody;
            _animator = animator;
            _transform = transform;
            _armAnimator = armAnimator;
        }

        public Vector3 LookDir
        {
            get { return -_rb.transform.right; }
        }

        public Vector3 Position
        {
            get { return _transform.position; }
            set { _transform.position = value; }
        }

        public void Translate(Vector3 translation) => _transform.Translate(translation);

        public void MovePosition(Vector3 position)
        {
            _rb.MovePosition(_rb.position + position);
        }

        public void SetMoveAnimation(int id)
        {
            _animator.SetInteger("State", id);
            _armAnimator.SetInteger("State", id);
        }
    }
}