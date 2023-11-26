using UnityEngine;

public class IsVisible : MonoBehaviour
{
    Renderer m_Renderer;
    TrainerController trainerController;

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        trainerController = GetComponent<TrainerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Renderer.isVisible)
        {
            if (trainerController.terminouInteracao)
                gameObject.SetActive(false);
        }
    }
}