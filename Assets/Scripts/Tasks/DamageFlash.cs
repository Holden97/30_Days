﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeWar
{
    public class DamageFlash : MonoBehaviour
    {
        [ColorUsage(true, true)]
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashTime = 0.25f;
        [SerializeField] private AnimationCurve _flashSpeedCurve;
        [SerializeField] private Transform _spriteParent;

        private SpriteRenderer[] _spriteRenderers;
        private Material[] _materials;

        private Coroutine _damageFlashCoroutine;

        private void Awake()
        {
            _spriteRenderers = _spriteParent.GetComponentsInChildren<SpriteRenderer>();
            Init();
        }

        private void Init()
        {
            _materials = new Material[_spriteRenderers.Length];

            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].material = GameObject.Instantiate(_spriteRenderers[i].material);
                _materials[i] = _spriteRenderers[i].material;
            }
        }

        public void CallDamageFlash()
        {
            _damageFlashCoroutine = StartCoroutine(DamageFlasher());
        }

        private IEnumerator DamageFlasher()
        {
            SetFlashColor();

            float currentFlashAmount = 0f;
            float elapsedTime = 0f;
            while (elapsedTime < _flashTime)
            {
                elapsedTime += Time.deltaTime;
                currentFlashAmount = Mathf.Lerp(1f, _flashSpeedCurve.Evaluate(elapsedTime / _flashTime), (elapsedTime / _flashTime));
                SetFlashAmount(currentFlashAmount);
                yield return null;
            }
            SetFlashAmount(0);
        }

        private void SetFlashColor()
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetColor("_FlashColor", _flashColor);
            }
        }

        private void SetFlashAmount(float amount)
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat("_FlashAmount", amount);
            }
        }

        private void OnDisable()
        {
            //TODO：临时使用，后续需要搞懂这个脚本的含义
            SetFlashAmount(0);
        }
    }
}
