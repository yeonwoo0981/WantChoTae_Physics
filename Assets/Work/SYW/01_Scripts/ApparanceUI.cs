using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ApparanceUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _tmp;
   
   private void Start()
   {
      _tmp.DOFade(1f, 2.5f);
   }
}
