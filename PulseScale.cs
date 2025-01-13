using UnityEngine;

namespace TrilloBit3sIndieGames
{
    public class PulseScale : MonoBehaviour
    {
        [Header("Configurações de Escala")]
        public Vector3 minScale = new Vector3(0.2f, 0.2f, 0.2f); // Tamanho mínimo
        public Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f); // Tamanho máximo
        public float speed = 2.0f; // Velocidade do pulsar

        private float timer;

        void Update()
        {
            // Incrementa o timer baseado no tempo
            timer += Time.deltaTime * speed;

            // Calcula o valor da curva senoidal entre 0 e 1
            float scaleValue = (Mathf.Sin(timer) + 1.0f) / 2.0f;

            // Interpola entre a escala mínima e máxima
            transform.localScale = Vector3.Lerp(minScale, maxScale, scaleValue);
        }
    }
}