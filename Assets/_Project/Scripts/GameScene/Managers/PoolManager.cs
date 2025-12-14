using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using UnityEditor;
using TMPro;

public class PoolManager : SingleTon<PoolManager>
{
    [Header("Enemy Pools")]
    public GameObject enemyPoolsParent;
    public EnemyPoolConfig[] enemyGroups; // 프리팹/초기수 설정
    public List<EnemyPool> enemyPools = new();
    [SerializeField] private int _mutationProp;


    [Header("deathEffect Pools")]
    public ParticlePool deathEffectPools;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] public GameObject deathEffectPoolsParent;

    [Header("HitText Pools")]
    public HitTextPool hitTextPools;
    [SerializeField] private GameObject _hitText;
    [SerializeField] public GameObject hitTextPoolsParent;

    public void BuildEnemyPools()
    {
        enemyPools.Clear();
        foreach (var cfg in enemyGroups)
        {
            if (cfg.prefab == null) continue;
            enemyPools.Add(new EnemyPool(cfg.prefab, Mathf.Max(0, cfg.initialSize), Mathf.Max(cfg.initialSize, cfg.maxSize), _mutationProp, enemyPoolsParent.transform));
        }
    }

    public void BuildPools()
    {
        deathEffectPools = new ParticlePool(_deathEffect, 32, deathEffectPoolsParent.transform);
        hitTextPools = new HitTextPool(_hitText, 64, hitTextPoolsParent.transform);
    }

    protected override void Awake()
    {
        base.Awake();
        BuildPools();
    }

    [System.Serializable]
    public class EnemyPoolConfig
    {
        public GameObject prefab;
        public int initialSize;
        public int maxSize;
    }

    public class EnemyPool
    {
        public GameObject prefab;
        public int maxSize;
        private int _mutationProp = 100;
        private readonly Queue<GameObject> _q = new();
        public readonly HashSet<GameObject> active = new(); // 살아있는 애들 목록

        public EnemyPool(GameObject prefab, int initial, int max, int mutationProp, Transform parent = null)
        {
            this.prefab = prefab;
            maxSize = Mathf.Max(initial, max);
            _mutationProp = mutationProp;
            for (int i = 0; i < initial; i++)
            {
                var go = Instantiate(prefab, parent);
                go.SetActive(false);
                _q.Enqueue(go);
            }
        }

        public GameObject Get(Enemy enemy, Transform parent = null)
        {
            GameObject go;
            if (_q.Count > 0)
                go = _q.Dequeue();
            else
                go = Instantiate(prefab, parent);

            var enemyController = go.GetComponent<EnemyController>();
           
            enemyController.enemy = enemy;
            enemyController.OriginPool = this; 
            
            // 돌연변이 판정
            enemyController.isMutation = UnityEngine.Random.Range(0,100) < _mutationProp;

            if (parent != null)
                go.transform.SetParent(parent);

            go.SetActive(true);
            enemyController.EnemyInit();
            active.Add(go);      // 살아있는 리스트에 추가
            return go;
        }

        public void Return(GameObject go)
        {
            go.SetActive(false);
            active.Remove(go);   // 살아있는 리스트에서 제거
            _q.Enqueue(go);
        }

        public void ReturnAllEnemies()
        {
            // active를 복사해서 도는 게 안전
            var snapshot = new List<GameObject>(active);
            foreach (var go in snapshot)
            {
                Return(go);
            }
        }
    }

    [Serializable]
    public class ParticlePool
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private int _size;
        private Transform _parent;
        public Queue<ParticleSystem> particleQueue = new Queue<ParticleSystem>();

        public ParticlePool(ParticleSystem ParticlePrefab, int size, Transform parent = null)
        {
            _particlePrefab = ParticlePrefab;
            _size = size;
            _parent = parent;
            for (int i = 0; i < size; i++)
            {
                var p = Instantiate(_particlePrefab, _parent);
                p.gameObject.SetActive(false);
                particleQueue.Enqueue(p);
            }
        }

        public ParticleSystem GetParticleSystem(Vector3 pos)
        {
            ParticleSystem p;
            if(particleQueue.Count == 0)
            {
                p = Instantiate(_particlePrefab, _parent);
                p.gameObject.SetActive(false);
                particleQueue.Enqueue(p);
            }
            p = particleQueue.Dequeue();
            p.transform.position = pos;
            p.gameObject.SetActive(true);
            return p;
        }
    }

    [Serializable]
    public class HitTextPool
    {
        [SerializeField] private GameObject _hitTextPrefab;
        [SerializeField] private int _size = 64;
        private Transform _parent; 
        public Queue<GameObject> hitTextQueue = new Queue<GameObject>();

        public HitTextPool(GameObject hitTextPrefab, int size, Transform parent = null)
        {
            _hitTextPrefab = hitTextPrefab;
            _size = size;
            _parent = parent;
            for (int i = 0; i < size; i++)
            {
                var p = Instantiate(_hitTextPrefab, _parent);
                p.gameObject.SetActive(false);
                hitTextQueue.Enqueue(p);
            }
        }

        public GameObject GetHitText(EnemyController enemy, bool isCritical, float damage)
        {
            GameObject p;
            if(hitTextQueue.Count > 0)
                p = hitTextQueue.Dequeue();
            else
                p = Instantiate(_hitTextPrefab, _parent);
            p.transform.position = enemy.transform.position + new Vector3(0, 1.8f, 0);
            TextMeshProUGUI textMeshProUGUI =  p.GetComponentInChildren<TextMeshProUGUI>();
            textMeshProUGUI.text = $"{(int)damage}";
            if(isCritical)
            { 
                textMeshProUGUI.color = Color.red;
                p.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
            }
            else if(!isCritical)
            {
                textMeshProUGUI.color = Color.white;
                p.transform.localScale = new Vector3 (1f, 1f, 1f);
            }
            p.gameObject.SetActive(true);
            return p;
        }

        public void ReturnHitText(GameObject go)
        {
            go.SetActive(false);
            hitTextQueue.Enqueue(go);
        }
    }
}
