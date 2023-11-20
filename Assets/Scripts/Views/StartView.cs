using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] 
        private Button _tapToStart;

        [SerializeField] 
        private GameObject[] _objectForStart;

        private void OnEnable()
        {
            _tapToStart.onClick.AddListener(OnTapToStart);
        }

        private void OnDisable()
        {
            _tapToStart.onClick.RemoveListener(OnTapToStart);
        }

        private void OnTapToStart()
        {
            foreach (var obj in _objectForStart)
            {
                obj.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
