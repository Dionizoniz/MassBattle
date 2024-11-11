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

        public void Initialize(BaseUnit sourceUnit, BaseUnit targetUnit, Color color, IUpdateProvider updateProvider)
        {
            _armyData = sourceUnit.ArmyData;
            _target = targetUnit._transform.position;
            _updateProvider = updateProvider;
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

        private void Move()
        {
            float speed = _movementSpeed * Time.deltaTime;
            Vector3 direction = (_target - _transform.position).normalized;
            _transform.position += direction * speed; // TODO ??
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
            Destroy(_gameObject);
        }

        private void TryDestroyAfterReachTarget()
        {
            if (Vector3.Distance(_transform.position, _target) < _attackRange)
            {
                Destroy(_gameObject);
            }
        }

        private void OnDestroy()
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
    }
}
