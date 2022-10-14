using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class BaseExtentionController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> OpenUpExtentions;
        [SerializeField]
        private List<GameObject> CloseDownExtentions;
        [SerializeField]
        private List<GameObject> SideOpenClose;

        public void ChangeExtentionVisibility(BaseRoomTypes roomTypes)
        {

            SideOpenClose[(int)roomTypes].SetActive(false);
            OpenUpExtentions[(int)roomTypes].SetActive(true);
            CloseDownExtentions[(int)roomTypes].SetActive(false);

            if (SideOpenClose.Count > (int)roomTypes + 2)
            {
                SideOpenClose[(int)roomTypes + 2].SetActive(true);
            }
        }
    } 
}
