using System;
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
        protected float _attackRange = 1f;

        [Space, SerializeField]
        private Renderer _renderer;

        public float AttackValue { get; private set; }
        public Vector3 AttackPosition => _transform.position;

        private ArmyData _armyData;
        private Vector3 _target;
        private IUpdateProvider _updateProvider;
        private IUnitsFactory _unitsFactory;

        public void Initialize(
                BaseUnit sourceUnit, BaseUnit targetUnit, Color color, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory)
        {
            _armyData = sourceUnit.ArmyData;
            _target = targetUnit._transform.position;
            _updateProvider = updateProvider;
            _unitsFactory = unitsFactory;
            AttackValue = sourceUnit.AttackValue;

            UpdateColor(color);
            UpdateInitialPosition(sourceUnit);
            AttachToEvents();
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(COLOR, color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void UpdateInitialPosition(BaseUnit sourceUnit)
        {
            _transform.position = sourceUnit._transform.position;
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
            TryDestroyAfterReachTarget();
        }

        private void Move() // TODO simplify, we can cache direction and forward during initialization
        {
            float speed = _movementSpeed * Time.deltaTime;
            Vector3 direction = (_target - cachedPosition).normalized;
            _transform.position += direction * speed;
            _transform.forward = direction;
        }

        private void TryAttack()
        {
            Vector3 position = _transform.position;

            foreach (BaseUnit unit in _armyData.enemyArmyData.FindAllUnits())
            {
                Vector3 offset = unit._transform.position - position;

                if (offset.magnitude <= _attackRange)
                {
                    AttackUnit(unit);
                    break;
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
            Dispose();
            _unitsFactory.ReturnArrowInstance(this);
        }

        private void Dispose()
        {
            DetachFromEvents();
        }

        private void DetachFromEvents()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnEarlyUpdate -= CachePosition;
                _updateProvider.OnUpdate -= ManualUpdate;
            }
        }

        private void TryDestroyAfterReachTarget()
        {
            if (Vector3.Distance(_transform.position, _target) < _attackRange)
            {
                ReturnToPool();
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
