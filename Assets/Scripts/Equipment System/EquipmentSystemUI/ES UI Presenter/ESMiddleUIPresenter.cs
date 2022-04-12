using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
namespace EquipSystem
{ 
    public class ESMiddleUIPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject Character;
        [SerializeField] private Button LeftRotationButton;
        [SerializeField] private Button RightRotationButton;
        [SerializeField] private Dropdown RaceMenuDropdown;
        [SerializeField] [Range(1,60)] private float rotationSpeed;
        [SerializeField] private Camera CharacterCamera;
        [SerializeField] Slider cameraScaleSlider;

        private Vector3 _cameraOffset;

        private Transform _cameraTransform;
        private Transform characterTransform;

        private void Awake()
        {
            if (Character!=null)
            {
                characterTransform = Character.transform;
            }
            else
            {
                throw new UnityException("Not emplemented Chatacter");
            }
            CharacterCamera = Camera.main;
            _cameraTransform = CharacterCamera.transform;
            _cameraOffset = _cameraTransform.position;
        }
        public void RotationCharacterLeft()
        {
            characterTransform.Rotate(0, -rotationSpeed, 0);
        }
        public void RotationCharacterRight()
        {
            characterTransform.Rotate(0, +rotationSpeed, 0);
        }
        public void CameraScale()
        {
            
            _cameraTransform.position =new Vector3 (_cameraOffset.x,_cameraOffset.y, _cameraOffset.z-cameraScaleSlider.value);
        }


    }
}
