using System;
using System.Collections.Generic;
using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;
using UnityEngine;

namespace MassBattle.Logic.Units.Weapons
{
    public class Arrow : ExtendedMonoBehaviour, IAttack
    {
        private static readonly int COLOR = Shader.PropertyToID("_Color");

        [SerializeField]
        private float _movementSpeed = 50f;
        [SerializeField]
        protected float _hitRange = 1f;
        [SerializeField]
        protected float _flightDistance = 50f;

        [Space, SerializeField]
        private Renderer _renderer;

        public float AttackValue { get; private set; }
        public Vector3 AttackerPosition => _transform.position;

        private ArmyData _armyData;
        private Vector3 _initialPosition;
        private Vector3 _targetPosition;
        private IUpdateProvider _updateProvider;
        private IUnitsFactory _unitsFactory;

        private Vector3 _moveDirection;
        private bool _isInitialized;

        public void Initialize(
                BaseUnit sourceUnit, BaseUnit targetUnit, Color color, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory)
        {
            _armyData = sourceUnit.ArmyData;
            _initialPosition = sourceUnit._transform.position;
            _targetPosition = targetUnit._transform.position;
            _updateProvider = updateProvider;
            _unitsFactory = unitsFactory;
            AttackValue = sourceUnit.AttackValue;

            UpdateColor(color);
            CacheMoveDirection(sourceUnit);
            InitializeTransform(sourceUnit);
            AttachToEvents();

            _isInitialized = true;
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(COLOR, color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void CacheMoveDirection(BaseUnit sourceUnit)
        {
            _moveDirection = (_targetPosition - sourceUnit._transform.position).normalized;
        }

        private void InitializeTransform(BaseUnit sourceUnit)
        {
            _transform.position = sourceUnit._transform.position;
            _transform.forward = _moveDirection;
        }

        private void AttachToEvents()
        {
            _updateProvider.OnEarlyUpdate += CachePosition;
            _updateProvider.OnUpdate += ManualUpdate;
        }

        private void ManualUpdate()
        {
            Move();
            TryAttack();
            TryDestroyAfterReachDistance();
        }

        private void Move()
        {
            float speed = _movementSpeed * Time.deltaTime;
            _transform.position += _moveDirection * speed;
        }

        private void TryAttack()
        {
            float squaredHitRange = _hitRange * _hitRange;
            cachedPosition = _transform.position;

            for (var armyIndex = 0; armyIndex < _armyData.EnemyArmiesData.Count; armyIndex++)
            {
                List<BaseUnit> enemyUnits = _armyData.EnemyArmiesData[armyIndex].AllUnits;

                for (var unitIndex = 0; unitIndex < enemyUnits.Count; unitIndex++)
                {
                    BaseUnit enemyUnit = enemyUnits[unitIndex];
                    Vector3 offset = enemyUnit.cachedPosition - cachedPosition;

                    if (offset.sqrMagnitude <= squaredHitRange)
                    {
                        AttackUnit(enemyUnit);
                        break;
                    }
                }
            }
        }

        private void AttackUnit(BaseUnit unit)
        {
            unit.TakeDamage(this);
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_isInitialized)
            {
                DetachFromEvents();
                _unitsFactory.ReturnArrowInstance(this);
                _isInitialized = false;
            }
        }

        private void DetachFromEvents()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnEarlyUpdate -= CachePosition;
                _updateProvider.OnUpdate -= ManualUpdate;
            }
        }

        private void TryDestroyAfterReachDistance()
        {
            if (Vector3.Distance(_initialPosition, _transform.position) > _flightDistance)
            {
                ReturnToPool();
            }
        }

        private void OnDestroy()
        {
            DetachFromEvents();
        }
    }
}
