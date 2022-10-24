using UnityEngine;

namespace Game.Components
{
	public class Parallax : MonoBehaviour
	{
		[SerializeField] private GameObject cameraObject;
		[SerializeField] private float multiplier;
		
		private float _length;
		private float _startPos;
		
		void Start () 
		{
			_startPos = transform.position.x;
			_length = GetComponent<SpriteRenderer>().bounds.size.x;
		}
	
		void Update () 
		{
			float temp = cameraObject.transform.position.x * (1-multiplier);
			float dist = cameraObject.transform.position.x * multiplier;

			transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

			if (temp > _startPos + _length)
			{
				_startPos += _length;
			}
			else if (temp < _startPos - _length)
			{
				_startPos -= _length;
			}
		}

	}
}
