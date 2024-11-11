using MassBattle.Logic.Armies;
using UnityEngine;

namespace MassBattle.Logic.Units.Weapons
{
    public class Arrow : MonoBehaviour, IAttack
    {
        private static readonly int COLOR = Shader.PropertyToID("_Color");

        [SerializeField]
        private float _movementSpeed = 50f;
        [SerializeField]
        protected float _attackRange = 1f;

        [Space, SerializeField]
        private Renderer _renderer;

        public float AttackValue { get; private set; }
        public Vector3 AttackPosition => transform.position;

        private ArmyData _armyData;
        private Vector3 _target;

        public void Initialize(BaseUnit sourceUnit, BaseUnit targetUnit, Color color)
        {
            _armyData = sourceUnit.ArmyData;
            _target = targetUnit.transform.position;
            AttackValue = sourceUnit.AttackValue;

            UpdateColor(color);
            UpdateInitialPosition(sourceUnit);
        }

        private void UpdateInitialPosition(BaseUnit sourceUnit)
        {
            transform.position = sourceUnit.transform.position;
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(COLOR, color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void Update()
        {
            Move();
            TryAttack();
            TryDestroyAfterReachTarget();
        }

        private void Move()
        {
            float speed = _movementSpeed * Time.deltaTime;
            Vector3 direction = (_target - transform.position).normalized;
            transform.position += direction * speed;
            transform.forward = direction;
        }

        private void TryAttack()
        {
            Vector3 position = transform.position;

            foreach (BaseUnit unit in _armyData.enemyArmyData.FindAllUnits())
            {
                Vector3 offset = unit.transform.position - position;

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
            Destroy(gameObject);
        }

        private void TryDestroyAfterReachTarget()
        {
            if (Vector3.Distance(transform.position, _target) < _attackRange)
            {
                Destroy(gameObject);
            }
        }
    }
}
