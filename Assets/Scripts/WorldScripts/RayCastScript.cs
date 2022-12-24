using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    // El GameObject del que queremos comprobar si est� colisionando con otro
    public GameObject objectToCheck;

    // El tag del GameObject con el que queremos comprobar la colisi�n
    public string tagToCheck;

    void FixedUpdate()
    {
        // Creamos un rayo desde la posici�n del GameObject objectToCheck hacia adelante
        Ray ray = new Ray(transform.position, Vector3.down);

        // Creamos una variable para almacenar la informaci�n de la colisi�n del rayo
        RaycastHit hitInfo;
        Debug.Log(ray);
        // Enviamos el rayo y comprobamos si colisiona con alg�n collider que tenga el tag tagToCheck
        //if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 0, 0))
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, 0.2f))
        {
            Debug.Log("hitInfo 1: " + hitInfo);
            // Si el rayo colisiona con un collider, imprimimos el nombre del GameObject con el que colisiona y la distancia de la colisi�n
            Debug.Log("El rayo colisiona con el GameObject: " + hitInfo.collider.gameObject.name + " a una distancia de " + hitInfo.distance + " unidades.");
        }
        else
        {
            Debug.Log("hitInfo 2: " + hitInfo);
            // Si el rayo no colisiona con ning�n collider, imprimimos un mensaje
            Debug.Log("El rayo no colisiona con ning�n GameObject con el tag " + tagToCheck);
        }
    }
}
