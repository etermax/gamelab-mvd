using System.Collections.Generic;
using System.Linq;
using Core.Domain.Phyisics;
using UnityEngine;

namespace Core.Infrastructure.Phisycs
{
    public class Bla : IBla
    {
        public List<IEnemy> FindEnemies(Vector3 position, int radius)
        {
            return GetEnemiesCollidersOnRadious(position, radius)
                .Select(ExtractEnemyFromCollider)
                .Cast<IEnemy>()
                .ToList();
        }

        private static Enemy ExtractEnemyFromCollider(Collider2D enemy)
        {
            return enemy.GetComponent<Rigidbody2D>().gameObject.GetComponent<Enemy>();
        }

        private static Collider2D[] GetEnemiesCollidersOnRadious(Vector3 position, int radius)
        {
            return Physics2D.OverlapCircleAll(position, radius, 1 << LayerMask.NameToLayer("Enemies"));
        }
    }
}