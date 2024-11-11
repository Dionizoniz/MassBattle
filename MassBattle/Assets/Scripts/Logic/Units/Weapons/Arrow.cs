using MassBattle.Logic.Armies;
using UnityEngine;

namespace MassBattle.Logic.Units.Weapons
{
    public class Arrow : MonoBehaviour, IAttack
    {
        private static readonly int COLOR = Shader.PropertyToID("_Color");

        [SerializeField]
        private float _speed = 50f;
        [SerializeField]
        protected float _attackRange = 0.5f;

        [Space, SerializeField]
        private Renderer _renderer;

        private ArmyData _armyData;
        private Vector3 _target;

        public float AttackValue { get; private set; }
        public Vector3 AttackPosition => transform.position;

        public void Initialize(BaseUnit sourceUnit, BaseUnit targetUnit, Color color)
        {
            UpdateInitialPosition(sourceUnit);

            _armyData = sourceUnit.ArmyData;
            _target = targetUnit.transform.position;
            AttackValue = sourceUnit.AttackValue;

            UpdateColor(color);
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

        public void Update()
        {
            Vector3 direction = (_target - transform.position).normalized;
            transform.position += direction * (_speed * Time.deltaTime);
            transform.forward = direction;

            foreach (var unit in _armyData.enemyArmyData.FindAllUnits())
            {
                float dist = Vector3.Distance(unit.transform.position, transform.position);

                if (dist < _attackRange)
                {
                    unit.TakeDamage(this);
                    Destroy(gameObject);
                    return;
                }
            }

            if (Vector3.Distance(transform.position, _target) < _attackRange)
            {
                Destroy(gameObject);
            }
        }
    }
}
