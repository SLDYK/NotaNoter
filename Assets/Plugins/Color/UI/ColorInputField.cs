using UnityEngine;
using UnityEngine.UI;
//using TMPro;

namespace HSVPicker
{

    //[RequireComponent(typeof(TMP_Text))]
    public class ColorInputField : MonoBehaviour
    {
        public ColorPicker picker;

        public ColorValues type;

        public string prefix = "R: ";
        public float minValue = 0;
        public float maxValue = 255;

        public int precision = 0;

        [SerializeField, HideInInspector]
        private InputField _InputField;

        private void Awake()
        {
            _InputField = GetComponent<InputField>();

        }

        private void OnEnable()
        {
            _InputField.onEndEdit.AddListener(ChangeColorByInputfield);
            if (Application.isPlaying && picker != null)
            {
                picker.onValueChanged.AddListener(ColorChanged);
                picker.onHSVChanged.AddListener(HSVChanged);
            }
        }

        private void OnDestroy()
        {
            _InputField.onValueChanged.RemoveListener(ChangeColorByInputfield);
            if (picker != null)
            {
                picker.onValueChanged.RemoveListener(ColorChanged);
                picker.onHSVChanged.RemoveListener(HSVChanged);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _InputField = GetComponent<InputField>();
            UpdateValue();
        }
#endif

        private void ColorChanged(Color color)
        {
            UpdateValue();
        }

        private void HSVChanged(float hue, float sateration, float value)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            if (_InputField == null)
                return;

            if (picker == null)
            {
                _InputField.text = prefix + "-";
            }
            else
            {
                float value = minValue + (picker.GetValue(type) * (maxValue - minValue));

                _InputField.text = prefix + ConvertToDisplayString(value);
            }
        }

        private string ConvertToDisplayString(float value)
        {
            if (precision > 0)
                return value.ToString("f " + precision);
            else
                return Mathf.FloorToInt(value).ToString();
        }
        private void ChangeColorByInputfield(string str)
        {
            float value;
            if (string.IsNullOrEmpty(str)) value = 0;            
            if (float.TryParse(str, out value))
            {
                if (value>255)
                {
                    value = 255;                   
                }                
            }
            value /= (maxValue - minValue);
            picker.AssignColor(type, value);
        }
     
    }

}