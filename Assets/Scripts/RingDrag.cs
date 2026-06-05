using UnityEngine;
using UnityEngine.InputSystem;

public class RingDrag : MonoBehaviour
{
    [Header("Posição Final do Anel")]
    public GameObject targetPos;

    private bool correct = false;
    private Vector2 ringInicialPos;
    void Start()
    {
        //guardar a posicao inicial do ring pra retornar (caso solte)
        ringInicialPos = transform.position;
        Debug.Log(name + "local inicial de " + ringInicialPos);
    }
    public void DragRing() //ativada enquanto SEGURA o ring
    {
        //se ainda estiver errado, a posicao do ring pode ser alterada (e é a mesma que a do mouse) 
        if (!correct)
        {
            Debug.Log(transform.position);
            //pega o vetor3 da posicao do mouse e usa apenas x e y
            Vector3 vet = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(vet.x, vet.y);
        }
    }

    public void DropRing() //ativada ao SOLTAR o ring
    {
        //distancia entre o ring e o target correto dele
        float distance = Vector2.Distance(transform.position, targetPos.transform.position);
        if (distance < 3) //se a distancia for pequena, deixa correto e aloca na posicao em si
        {
            correct = true;
            transform.position = targetPos.transform.position;
        }
        else //se a distancia for grande, volta ele pra posicao inicial
        {
            transform.position = ringInicialPos;
            Debug.Log("local inicial (acabei de voltar pra ca): " + ringInicialPos);
        }
    }
}
