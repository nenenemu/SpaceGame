using UnityEngine;
using UnityEngine.SceneManagement;

public class RayClick : MonoBehaviour
{
    public GameObject AnimationObject;

    private Animator BottonAnimator;


    private void Start()
    {
        BottonAnimator = AnimationObject.GetComponent<Animator>();





    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("当たったオブジェクト: " + hit.collider.name);

                // クリックされたオブジェクトに何かしたい場合
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;

                if (hit.collider.gameObject.CompareTag("Botton"))
                {
                    BottonAnimator.SetBool("Click1" ,true);

                }
            }
        }
    }

}


