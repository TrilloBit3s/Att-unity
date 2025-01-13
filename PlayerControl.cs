using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class PlayerControl : MonoBehaviour
    {
        [Space]
        public Transform target;
        [SerializeField] private LayerMask enemyLayer;

        private void FixedUpdate()
        {
            if (target == null)
            {
                return;
            }

            if ((Vector3.Distance(transform.position, target.position) >= TargetDetectionControl.instance.detectionRange))
            {
                NoTarget();
            }
        }

        #region Change Target

        private EnemyAtt oldTarget;
        private EnemyAtt currentTarget;

        public void ChangeTarget(Transform target_)
        {
            if (target_ == null) return;

            if (target != null)
            {
                // Verifica se o componente existe antes de tentar desativar o alvo
                if (oldTarget != null)
                {
                    oldTarget.ActiveTarget(false); // Limpa o antigo alvo
                }
            }

            target = target_;

            oldTarget = target_.GetComponent<EnemyAtt>(); // Define o novo alvo
            if (oldTarget != null)
            {
                currentTarget = oldTarget;
                currentTarget.ActiveTarget(true);  // Ativa o novo alvo
            }
        }

        private void NoTarget() // Quando o jogador sai do alcance do alvo atual
        {
            if (currentTarget != null)
            {
                currentTarget.ActiveTarget(false);
            }

            currentTarget = null;
            oldTarget = null;
            target = null;
        }

        #endregion

        #region MoveTowards, Target Offset and FaceThis

        public void MoveTowardsTarget(Vector3 target_, float deltaDistance, string animationName_)
        {
            FaceThis(target_);
            Vector3 finalPos = TargetOffset(target_, deltaDistance);
            finalPos.y = 0;
            transform.position = finalPos;  // Atualiza a posição do jogador
        }

        public void GetClose() // Evento de animação - para se mover perto do alvo
        {
            Vector3 getCloseTarget;
            if (target == null)
            {
                getCloseTarget = oldTarget.transform.position;
            }
            else
            {
                getCloseTarget = target.position;
            }
            FaceThis(getCloseTarget);
            Vector3 finalPos = TargetOffset(getCloseTarget, 1.4f);
            finalPos.y = 0;
            transform.position = finalPos;  // Atualiza a posição do jogador
        }

        public Vector3 TargetOffset(Vector3 target, float deltaDistance)
        {
            Vector3 position;
            position = target;
            return Vector3.MoveTowards(position, transform.position, deltaDistance);
        }

        public void FaceThis(Vector3 target)
        {
            Vector3 target_ = new Vector3(target.x, target.y, target.z);
            Quaternion lookAtRotation = Quaternion.LookRotation(target_ - transform.position);
            lookAtRotation.x = 0;
            lookAtRotation.z = 0;
            transform.rotation = lookAtRotation;  // Aplica a rotação ao transform
        }

        #endregion
    }
}
