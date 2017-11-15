using System.Collections.Generic;
using UnityEngine;

namespace Core.Domain.Phyisics
{
    public interface IBla
    {
        List<IEnemy> FindEnemies(Vector3 position, int radius);
    }
}