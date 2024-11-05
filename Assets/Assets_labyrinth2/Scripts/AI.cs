using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Classe AI pour g�rer le comportement du NPC avec un syst�me d'�tats
public class AI : MonoBehaviour
{

    // Composants NavMeshAgent et Animator pour le mouvement et l'animation
    NavMeshAgent agent;      // R�f�rence � l'agent de navigation
    Animator anim;           // R�f�rence � l'animateur pour les animations du NPC
    State currentState;      // �tat actuel du NPC

        // Variable s�rialis�e expos�e dans l'inspecteur pour ajuster la vitesse du robot dans Unity
    [SerializeField] float speed = 1f;
    
    // D�claration d'un rayon qui sera utilis� pour d�tecter les obstacles devant le robot
    Ray rayon;

    // D�claration d'une variable pour stocker les informations de collision lorsque le rayon touche un objet
    RaycastHit hit;

    // Variables s�rialis�es pour assigner les capteurs gauche et droit du robot dans l'�diteur Unity
    [SerializeField] Transform leftSensor, rightSensor;

    public Transform player; // R�f�rence au joueur pour interagir avec le NPC

    // Initialisation au d�marrage de la sc�ne
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    // Initialisation de l'agent de navigation
        anim = GetComponent<Animator>();         // Initialisation de l'animateur
        currentState = new Idle(gameObject, agent, anim, player); // Le NPC commence en �tat "Idle"
    }

    // Mise � jour � chaque frame pour traiter l'�tat actuel
    void Update()
    {
        currentState = currentState.Process(); // Mise � jour de l'�tat actuel du NPC
    
        // Cr�ation d'un rayon partant de la position du capteur gauche, orient� vers l'avant du robot
        rayon = new Ray(leftSensor.position, transform.TransformDirection(Vector3.forward));

        // Si le rayon touche un objet dans le monde
        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            // Affiche le nom de l'objet touch� et la distance dans la console pour le capteur gauche
            Debug.Log("Left Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);

            // Si l'objet d�tect� est � moins de 1 unit� de distance
            if (hit.distance < 1)
            {
                // G�n�re un angle al�atoire entre 100 et 300 degr�s pour que le robot tourne
                float angle = Random.Range(100f, 300f);
                // Fait tourner le robot autour de l'axe vertical (Y) selon l'angle al�atoire g�n�r�
                transform.Rotate(Vector3.up * angle * (Time.deltaTime / 4));
            }
        }

        // Visualise le rayon du capteur gauche dans la vue Sc�ne de Unity (une ligne jaune de 10 unit�s)
        Debug.DrawRay(leftSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);

        // Cr�ation d'un rayon partant de la position du capteur droit, orient� vers l'avant du robot
        rayon = new Ray(rightSensor.position, transform.TransformDirection(Vector3.forward));

        // Si le rayon touche un objet dans le monde
        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            // Affiche le nom de l'objet touch� et la distance dans la console pour le capteur droit
            Debug.Log("Right Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);

            // Si l'objet d�tect� est � moins de 1 unit� de distance
            if (hit.distance < 1)
            {
                // G�n�re un angle al�atoire entre 100 et 300 degr�s pour que le robot tourne
                float angle = Random.Range(100f, 300f);

                // Fait tourner le robot autour de l'axe vertical (Y) selon l'angle al�atoire g�n�r�
                transform.Rotate(Vector3.up * angle * (Time.deltaTime / 4));
            }
        }

        // Visualise le rayon du capteur droit dans la vue Sc�ne de Unity (une ligne jaune de 10 unit�s)
        Debug.DrawRay(rightSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);

        // D�place le robot vers l'avant selon sa direction actuelle avec la vitesse d�finie
        transform.Translate(Vector3.forward * speed * (Time.deltaTime / 4));
    
    }
}