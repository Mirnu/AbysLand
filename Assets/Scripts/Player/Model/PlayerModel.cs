using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel 
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;

        public PlayerModel(Rigidbody2D rigidbody, Animator animator) 
        { 
            _rigidbody = rigidbody;
            _animator = animator;
        }

        public Vector3 LookDir
        {
            get { return -_rigidbody.transform.right; }
        }

        public float Rotation
        {
            get { return _rigidbody.rotation; }
            set { _rigidbody.rotation = value; }
        }

        public Vector2 Position
        {
            get { return _rigidbody.position; }
            set { _rigidbody.position = value; }
        }

        public void MovePosition(Vector2 position)
        {
            _rigidbody.MovePosition(_rigidbody.position + position);
        }

        public void SetMoveAnimation(int id)
        {
            _animator.SetInteger("Walk", id);
        }
    }
}