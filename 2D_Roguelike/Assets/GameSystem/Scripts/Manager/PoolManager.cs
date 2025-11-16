using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using UnityEditor;

public class PoolManager : SingleTon<PoolManager>
{
    [Header("Enemy Pools")]
    public GameObject enemyPoolsParent;
    public EnemyPoolConfig[] enemyGroups; // 프리팹/초기수 설정
    public List<EnemyPool> enemyPools = new();


    [Header("deathEffect Pools")]
    public ParticlePool deathEffectPools;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] public GameObject deathEffectPoolsParent;

    public void BuildEnemyPools()
    {
        enemyPools.Clear();
        foreach (var cfg in enemyGroups)
        {
            if (cfg.prefab == null) continue;
            enemyPools.Add(new EnemyPool(cfg.prefab, Mathf.Max(0, cfg.initialSize), Mathf.Max(cfg.initialSize, cfg.maxSize), enemyPoolsParent.transform));
        }
    }

    public void BuilddeathEffectPools()
    {
        deathEffectPools = new ParticlePool(deathEffect, 32, deathEffectPoolsParent.transform);
    }

    protected override void Awake()
    {
        base.Awake();
        BuilddeathEffectPools();
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
        private readonly Queue<GameObject> q = new();
        private readonly HashSet<GameObject> active = new(); // 살아있는 애들 목록

        public EnemyPool(GameObject prefab, int initial, int max, Transform parent = null)
        {
            this.prefab = prefab;
            maxSize = Mathf.Max(initial, max);
            for (int i = 0; i < initial; i++)
            {
                var go = Instantiate(prefab, parent);
                go.SetActive(false);
                q.Enqueue(go);
            }
        }

        public GameObject Get(Enemy enemy, Transform parent = null)
        {
            GameObject go;
            if (q.Count > 0)
                go = q.Dequeue();
            else
                go = Instantiate(prefab, parent);

            var enemyController = go.GetComponent<EnemyController>();
            enemyController.enemy = enemy;
            enemyController.OriginPool = this; 

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
            q.Enqueue(go);
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
        [SerializeField] private ParticleSystem ParticlePrefab;
        [SerializeField] private int size = 32;
        public Queue<ParticleSystem> particleQueue = new Queue<ParticleSystem>();

        public ParticlePool(ParticleSystem ParticlePrefab, int size, Transform parent = null)
        {
            this.ParticlePrefab = ParticlePrefab;
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                var p = Instantiate(ParticlePrefab, parent);
                p.gameObject.SetActive(false);
                particleQueue.Enqueue(p);
            }
        }

        public ParticleSystem GetParticleSystem(Vector3 pos)
        {
            var p = particleQueue.Dequeue();
            p.transform.position = pos;
            p.gameObject.SetActive(true);
            return p;
        }
    }
}
