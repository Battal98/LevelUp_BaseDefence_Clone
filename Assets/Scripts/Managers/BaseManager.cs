using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject.LevelDatas;
using Signals;
using Enums;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField]
        private BaseTextController baseTextController;
        [SerializeField] 
        private BaseRoomExtentionController extentionController;

        #endregion

        #region Private Variables

        private LevelData _levelData;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            _levelData = GetLevelData();
        }

        private void GetReferences()
        {
            baseTextController.SetBaseLevelText();
        }


        #region Event Subscription
        private LevelData GetLevelData() => Resources.Load<CD_Level>("BaseDefense/CD_Level").LevelDatas[0];

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
        }
        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnChangeVisibility(BaseRoomTypes baseRoomType)
        {
            ChangeVisibility(baseRoomType);
        }
        private void ChangeVisibility(BaseRoomTypes baseRoomType)
        {
            extentionController.ChangeExtentionVisibility(baseRoomType);
        }

    }
}
