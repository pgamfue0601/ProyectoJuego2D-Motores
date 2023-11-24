using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformController : MonoBehaviour
{
    public int velocidad;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    public int topeX, topeY;
    private bool moviendoFin;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        posicionFinal = new Vector3(posicionInicial.x + topeX, posicionFinal.y + topeY, posicionInicial.z);
        moviendoFin = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoverPlataforma());
    }

    IEnumerator MoverPlataforma()
    {
        Vector3 posicionDestino = (moviendoFin) ? posicionFinal : posicionInicial;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        if (transform.position == posicionFinal)
        {
            yield return new WaitForSeconds(3f);
            moviendoFin = false;
        }
        else if (transform.position == posicionInicial)
        {
            yield return new WaitForSeconds(3f);
            moviendoFin = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
