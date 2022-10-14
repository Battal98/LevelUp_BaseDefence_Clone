using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Interfaces;
using UnityEngine;
using Managers;
using Signals;

namespace Controllers
{
    public class MinerAIItemController : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        public Dictionary<MinerItems, GameObject> ItemList = new Dictionary<MinerItems, GameObject>();
       
        

        #endregion
        #region Serialized Variables

        [SerializeField] private GameObject pickaxe;
        [SerializeField] private GameObject gem;
        [SerializeField] private Transform gemHolder;

        #endregion
        #endregion

        private void Awake()
        {
            AddToDictionary();
            
        }


        private void AddToDictionary()
        {
            ItemList.Add(MinerItems.Gem, gem);
            ItemList.Add(MinerItems.Pickaxe, pickaxe);
        }

        public void OpenItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
                ItemList[currentItem].SetActive(true);
            }
            if (MinerItems.None == currentItem)
            {
                foreach (Transform child in gemHolder) {
                    ReleaseObject(child.gameObject, PoolType.Gem);
                }
            }
            if (MinerItems.Gem == currentItem)
            {
                gem = GetObject(PoolType.Gem);
                gem.transform.parent=gemHolder;
                //gem.Cop
                gem.transform.localPosition=Vector3.zero;
                gem.transform.localScale=Vector3.one*3;
                gem.transform.localRotation= Quaternion.identity;
            }
        }
        public void CloseItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
               ItemList[currentItem].SetActive(false);
            }
        }

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(poolType,obj);
        }

        public GameObject GetObject(PoolType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool(poolType);
        }
    }
}