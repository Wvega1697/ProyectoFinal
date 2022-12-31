using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    // El GameObject del que queremos comprobar si está colisionando con otro
    public GameObject objectToCheck;

    // El tag del GameObject con el que queremos comprobar la colisión
    public string tagToCheck;

    void FixedUpdate()
    {
        // Creamos un rayo desde la posición del GameObject objectToCheck hacia adelante
        Ray ray = new Ray(transform.position, Vector3.down);

        // Creamos una variable para almacenar la información de la colisión del rayo
        RaycastHit hitInfo;
        Debug.Log(ray);
        // Enviamos el rayo y comprobamos si colisiona con algún collider que tenga el tag tagToCheck
        //if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 0, 0))
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, 0.2f))
        {
            Debug.Log("hitInfo 1: " + hitInfo);
            // Si el rayo colisiona con un collider, imprimimos el nombre del GameObject con el que colisiona y la distancia de la colisión
            Debug.Log("El rayo colisiona con el GameObject: " + hitInfo.collider.gameObject.name + " a una distancia de " + hitInfo.distance + " unidades.");
        }
        else
        {
            Debug.Log("hitInfo 2: " + hitInfo);
            // Si el rayo no colisiona con ningún collider, imprimimos un mensaje
            Debug.Log("El rayo no colisiona con ningún GameObject con el tag " + tagToCheck);
        }
    }
}
