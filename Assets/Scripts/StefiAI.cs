using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StefiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (health > 0f)
        {
            switch (health)
            {
                case float _ when (health > 75f):
                    
                    if (DetectedEnemies.Count == 1)
                    {
                        if (specialAttackBar == 100f)
                        {
                            SpecialAttack();
                            yield return null;
                        }
                        else
                        {
                            yield return Move(GetClosestEnemy().Position);
                        }
                    }
                    else if (DetectedEnemies.Count > 1)
                    {
                        if (specialAttackBar == 100f)
                        {
                            SpecialAttack();
                        }
                        else
                        {
                            yield return Move(GetClosestEnemy().Position + new Vector3(20f, 0f, 20f));
                        }
                    }

                    if (DetectedFood.Count > 0f)
                    {
                        yield return Move(GetClosestFood().Position);
                    }

                    break;
                
                // case float _ when (health > 50f):
                //     
                //     break;
                //
                // case float _ when (health < 50f):
                //     
                //     break;
                
                default:

                    break;
            }
            yield return Move(wanderTarget);
        }
    }

    public override void EnemyInRangeEvent(BasePlayer enemy)
    {
        BasicAttack(enemy);
    }
}
