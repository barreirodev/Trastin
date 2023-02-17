using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;
using DG.Tweening;

namespace Recycle {
    public class ChooseOrigin : MonoBehaviour
    {
        [SerializeField] List<BellConfiguration> _configurations;
        [SerializeField] EventBus _newBoxCreated;
        [SerializeField] RecycleMediator _upperBell;
        [SerializeField] RecycleMediator _lowerBell;

        private void Awake()
        {
            _newBoxCreated.Event += ChooseNewOrigins;
            DOTween.Init();
        }
        private void OnDisable()
        {
            _newBoxCreated.Event -= ChooseNewOrigins;
        }
        private void ChooseNewOrigins(BoxMediator box)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(_upperBell.originText.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutQuart))
              .Join(_lowerBell.originText.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutQuart)).OnComplete(()=>AssignOrigin(box));
        }

        private void AssignOrigin(BoxMediator box)
        {
            for (int i = 0; i < _configurations.Count; ++i)
            {
                if (box.GetOrigin == _configurations[i].GetOrigin)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        _upperBell._bellConfiguration = _configurations[i];
                        _upperBell.originText.text = _configurations[i].GetOrigin.GetID.ToString();
                        int counter = i;
                        do
                        {
                            counter = Random.Range(0, _configurations.Count);
                        } while (counter == i);
                        _lowerBell._bellConfiguration = _configurations[counter];
                        _lowerBell.originText.text = _configurations[counter].GetOrigin.GetID.ToString();
                    }
                    else
                    {
                        _lowerBell._bellConfiguration = _configurations[i];
                        _lowerBell.originText.text = _configurations[i].GetOrigin.GetID.ToString();
                        int counter = i;
                        do
                        {
                            counter = Random.Range(0, _configurations.Count);
                        } while (counter == i);
                        _upperBell._bellConfiguration = _configurations[counter];
                        _upperBell.originText.text = _configurations[counter].GetOrigin.GetID.ToString();
                    }
                }
            }
            ShowNewText();
        }

        private void ShowNewText()
        {
            _upperBell.originText.gameObject.transform.DOScale(new Vector3(0.002f, 0.002f, 1), 1.0f).SetEase(Ease.OutBack);
            _lowerBell.originText.gameObject.transform.DOScale(new Vector3(0.002f, 0.002f, 1), 1.0f).SetEase(Ease.OutBack);

        }
    }
}