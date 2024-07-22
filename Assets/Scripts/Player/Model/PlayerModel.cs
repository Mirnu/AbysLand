using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel 
    {
        private readonly Rigidbody _rigidbody;
        private readonly Animator _animator;
        private readonly Transform _transform;

        public PlayerModel(Rigidbody rigidbody, Animator animator, Transform transform) 
        { 
            _rigidbody = rigidbody;
            _animator = animator;
            _transform = transform;
        }

        public Vector3 LookDir
        {
            get { return -_rigidbody.transform.right; }
        }

        public Vector3 Position
        {
            get { return _transform.position; }
            set { _transform.position = value; }
        }

        public void Translate(Vector3 translation) => _transform.Translate(translation);

        public void MovePosition(Vector3 position)
        {
            _rigidbody.MovePosition(_rigidbody.position + position);
        }

        public void SetMoveAnimation(int id)
        {
            _animator.SetInteger("Walk", id);
        }
    }
}