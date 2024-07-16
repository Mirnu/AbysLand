using UnityEngine;

namespace Player.Model
{
    public class PlayerModel 
    {
        private readonly Rigidbody2D _rigidbody;

        public PlayerModel(Rigidbody2D rigidbody) 
        { 
            _rigidbody = rigidbody;
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
    }
}