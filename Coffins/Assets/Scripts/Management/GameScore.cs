using TMPro;
using UnityEngine;

namespace Management
{
    public class GameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private const string RecordDataKey = "record";

        private const string ViewText = "Score: {0:d}\n<size=60%>record: {1:d}</size>";
        
        private int _record = 0;
        private int _score = 0;

        private void Awake()
        {
            _record = PlayerPrefs.GetInt(RecordDataKey);
            Set(0);
        }

        public void Set(int score)
        {
            _score = score;

            if (_score > _record)
            {
                _record = _score;
                PlayerPrefs.SetInt(RecordDataKey, _record);
            }

            _scoreText.text = string.Format(ViewText, _score, _record);
        }

        public void Add(int addend)
        {
            Set(_score + addend);
        }

        public int Get()
        {
            return _score;
        }
    }
}