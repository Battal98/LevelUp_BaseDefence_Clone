
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Pool
{
    public class CreateEnemyPool : MonoBehaviour

    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField]
        private int initialEnemyStock = 10;
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private List<GameObject> createdEnemyObjects;
        //[SerializeField] private GameObject olusanObje;
        #endregion

        #region Private Variables
        private float timer;
        private float timer2;
        private float counter;
        #endregion
        #endregion
        
        
        
        public void Awake()
        {
            initPool();
        }

        private void Update()
        {
            ExampleObjectCreator();
        }

        private void ExampleObjectCreator()
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
            if (timer>1)
            {
                counter++;
                Debug.Log("Objeler Release Edildi");
                GetFromPool();
                timer = 0;
            }

            if (timer2 > 5)
            {
                for (int i = 0; i < counter; i++)
                {
                    Debug.Log("Objeler Release Edildi");
                    ReleaseObjects(i);
                }
                timer2 = 0;
            }
        }

        private void ReleaseObjects(int index)
        {
            ObjectPoolManager.Instance.ReturnObject(createdEnemyObjects[index],"Enemy");
        }
        private void GetFromPool()
        {
            createdEnemyObjects.Add(ObjectPoolManager.Instance.GetObject<GameObject>("Enemy"));
        }

        private void initPool()
        {
            ObjectPoolManager.Instance.AddObjectPool<GameObject>(CreateObject,
                TurnOnEnemy,TurnOffEnemy,"Enemy",initialEnemyStock,true);
        }

        private void TurnOnEnemy(GameObject gameObject)
        {
            gameObject.SetActive(true);//Obje Acildiginda ne olacagini burada giriyoruz
        }
        private void TurnOffEnemy(GameObject gameObject)
        {
            gameObject.SetActive(false);//Obje Kapandiginda ne olacagini burada giriyoruz
        }
        private GameObject CreateObject()
        {
            return Instantiate(enemyObject,Vector3.zero,Quaternion.identity,transform);
          
        }
    }
}