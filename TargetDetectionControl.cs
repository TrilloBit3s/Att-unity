using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class TargetDetectionControl : MonoBehaviour
    {
        public static TargetDetectionControl instance;

        [Header("Components")]
        public PlayerControl playerControl;

        [Header("Scene")]
        public List<Transform> allTargetsInScene = new List<Transform>();
        
        [Space]
        [Header("Target Detection")]
        public LayerMask whatIsEnemy;
        public bool canChangeTarget = true;

        [Tooltip("Detection Range: \n Alcance do jogador para detectar alvos.")]
        [Range(0f, 15f)] public float detectionRange = 10f;

        [Tooltip("Limite de produto escalar \n Valores mais altos: é necessário um alinhamento mais rigoroso \n Valores mais baixos: permite uma segmentação mais ampla")]
        [Range(0f, 1f)] public float LimiteDeProdutoEscalar = 0.15f;

        //[Space]
        //[Header("Debug")]
        //public bool debug;
        public Transform checkPos;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            PopulateTargetInScene();
            StartCoroutine(RunEveryXms());
        }

        private void PopulateTargetInScene()
        {
            //Encontre todos os GameObjects ativos na cena
            EnemyAtt[] allGameObjects = FindObjectsOfType<EnemyAtt>();

            // Converte o array em uma lista
            List<EnemyAtt> gameObjectList = new List<EnemyAtt>(allGameObjects);

            // Mostra o número de GameObjects encontrados
            /*if (debug)
                Debug.Log("Number of targets found: " + gameObjectList.Count);*/

            // Opcionalmente, itere sobre a lista e faça algo com cada GameObject
            foreach (EnemyAtt obj in gameObjectList)
            {
                allTargetsInScene.Add(obj.transform);
            }
        }

        private IEnumerator RunEveryXms()
        {
            while (true)
            {
                yield return new WaitForSeconds(.1f); // espere por 'x' millisegundos
                GetEnemyInInputDirection();
            }
        }

        #region Get Enemy In Input Direction

        public void GetEnemyInInputDirection()
        {
            if (canChangeTarget)
            {
                Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

                if (inputDirection != Vector3.zero)
                {
                    inputDirection = Camera.main.transform.TransformDirection(inputDirection);
                    inputDirection.y = 0;
                    inputDirection.Normalize();


                    Transform closestEnemy = GetClosestEnemyInDirection(inputDirection);

                    if (closestEnemy != null && (Vector3.Distance(transform.position, closestEnemy.position)) <= detectionRange)
                    {
                        playerControl.ChangeTarget(closestEnemy);
                        // Faça algo com o inimigo mais próximo na direção de entrada
                        // Debug.Log("Closest enemy in direction: " + closestEnemy.name);
                    }
                }

            }
        }
        
        Transform GetClosestEnemyInDirection(Vector3 inputDirection)
        {
            Transform closestEnemy = null;
            float maxDotProduct = LimiteDeProdutoEscalar; // Comece com o valor limite

            foreach (Transform enemy in allTargetsInScene)
            {
                Vector3 enemyDirection = (enemy.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(inputDirection, enemyDirection);

                if (dotProduct > maxDotProduct)
                {
                    maxDotProduct = dotProduct;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        #endregion

        #region Unused Code/ Might Delete Later

        #endregion
    }
}