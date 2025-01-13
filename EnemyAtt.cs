//Ativa e desativa o quad e rotaciona na direção do player, caso queira ou não, com objeto no proprio objeto.
using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class EnemyAtt : MonoBehaviour
    {
        [Header("Referências")]
        [Tooltip("Objeto que será referência")]
        [SerializeField] private GameObject activeTargetObject;
        [Tooltip("Próprio objeto Player")]
        [SerializeField] private Transform player;  // Referência ao jogador para rotação.
        [Tooltip("Objeto transform para rotacionar na frente do player")]
        [SerializeField] private Transform quad;  // Referência ao objeto quad que vai rotacionar.

        [Header("Opções de Rotação")]
        [Tooltip("Ativa ou desativa a rotação do quad em direção ao jogador.")]
        [SerializeField] private bool rotateTowardsPlayer = true;  // Controla se o quad deve rotacionar ou não.

        private void Start()
        {
            ActiveTarget(false);
        }

        private void Update()
        {
            if (activeTargetObject.activeSelf)  // Só rotaciona se o alvo estiver ativo.
            {
                if (rotateTowardsPlayer)  // Verifica se a rotação está habilitada.
                {
                    RotateTowardsPlayer();
                }
            }
        }

        public void ActiveTarget(bool bool_)
        {
            activeTargetObject.SetActive(bool_);
        }

        private void RotateTowardsPlayer()
        {
            if (player == null || quad == null) return;  // Verifica se o jogador e o quad foram atribuídos.

            // Calcula a direção para o jogador.
            Vector3 direction = player.position - quad.position;
            direction.y = 0;  // Mantém a rotação no eixo Y para evitar inclinação.

            // Calcula a rotação para a direção desejada.
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Aplica a rotação suavemente (opcional, dependendo do comportamento desejado).
            quad.rotation = Quaternion.Slerp(quad.rotation, targetRotation, Time.deltaTime * 5f);  // O valor 5f pode ser ajustado para controlar a velocidade de rotação.
        }
    }
}

/*
//ativa e desativa o quad
using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class EnemyAtt : MonoBehaviour
    {
        [SerializeField] private GameObject activeTargetObject;

        // Start is called before the first frame update
        void Start()
        {
            ActiveTarget(false);
        }

        public void ActiveTarget(bool bool_)
        {
            activeTargetObject.SetActive(bool_);
        }
    }
}
*/

/*
//Ativa e desativa o quad e rotaiona na direção do player
using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class EnemyAtt : MonoBehaviour
    {
        [SerializeField] private GameObject activeTargetObject;
        [SerializeField] private Transform player;  // Referência ao jogador para rotação

        private void Start()
        {
            ActiveTarget(false);
        }

        private void Update()
        {
            if (activeTargetObject.activeSelf)  // Só rotaciona se o alvo estiver ativo
            {
                RotateTowardsPlayer();
            }
        }

        public void ActiveTarget(bool bool_)
        {
            activeTargetObject.SetActive(bool_);
        }

        private void RotateTowardsPlayer()
        {
            if (player == null) return;  // Verifica se a referência ao jogador foi atribuída

            // Calcula a direção para o jogador
            Vector3 direction = player.position - transform.position;
            direction.y = 0;  // Mantém a rotação no eixo Y para evitar inclinação

            // Calcula a rotação para a direção desejada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Aplica a rotação suavemente (opcional, dependendo do comportamento desejado)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);  // O valor 5f pode ser ajustado para controlar a velocidade de rotação
        }
    }
}
*/