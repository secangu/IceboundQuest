using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    LineRenderer trajectoryLine; 
    [SerializeField] Transform hitMarker;  // Transform del marcador de impacto
    [SerializeField, Range(10, 100)] int maxPoints = 50;  // N�mero m�ximo de puntos en la l�nea de trayectoria
    [SerializeField, Range(0.01f, 0.5f)] float increment = 0.025f;  // Incremento de tiempo entre puntos en el c�lculo de la trayectoria
    [SerializeField, Range(1.05f, 2f)] float rayOverlap = 1.1f;  // Superposici�n de rayos entre puntos de la trayectoria

    private void Start()
    {
        if (trajectoryLine == null)
            trajectoryLine = GetComponent<LineRenderer>();

        SetTrajectoryVisible(true);  // Hace visible la trayectoria al inicio
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        Vector3 velocity = projectile.initialSpeed / projectile.mass * projectile.direction;  // Calcula la velocidad inicial del proyectil
        Vector3 position = projectile.initialPosition;  // Posici�n inicial del proyectil
        Vector3 nextPosition;
        float overlap;

        UpdateLineRender(maxPoints, (0, position));  // Inicializa el LineRenderer con el primer punto

        for (int i = 1; i < maxPoints; i++)
        {
            // Estima la velocidad y actualiza la siguiente posici�n predicha
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            // Superpone los rayos ligeramente para asegurarse de no perder ninguna superficie
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            // Si se golpea una superficie, muestra el marcador y detiene el c�lculo de la l�nea
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                UpdateLineRender(i, (i - 1, hit.point));  // Actualiza la l�nea hasta el punto de impacto
                MoveHitMarker(hit);  // Mueve el marcador al punto de impacto
                break;
            }

            // Si no se golpea nada, contin�a dibujando el arco de la trayectoria
            hitMarker.gameObject.SetActive(false);  // Desactiva el marcador
            position = nextPosition;  // Actualiza la posici�n actual
            UpdateLineRender(maxPoints, (i, position));  // Actualiza la l�nea con el nuevo punto
            Debug.Log("asd");
        }
    }

    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        trajectoryLine.positionCount = count;  // Actualiza la cantidad de puntos en la l�nea
        trajectoryLine.SetPosition(pointPos.point, pointPos.pos);  // Establece la posici�n del punto en la l�nea
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;  // A�ade la gravedad a la velocidad
        velocity *= Mathf.Clamp01(1f - drag * increment);  // Aplica el efecto del arrastre
        return velocity;  // Devuelve la nueva velocidad calculada
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        hitMarker.gameObject.SetActive(true);  // Activa el marcador

        // Desplaza el marcador desde la superficie para evitar colisiones visuales
        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;  // Posiciona el marcador en el punto de impacto con un desplazamiento
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);  // Rota el marcador para que se alinee con la normal de la superficie
    }

    public void SetTrajectoryVisible(bool visible)
    {
        Debug.Log("aa");
        trajectoryLine.enabled = visible;  // Establece la visibilidad de la l�nea de trayectoria
        hitMarker.gameObject.SetActive(visible);  // Establece la visibilidad del marcador de impacto
    }
}
