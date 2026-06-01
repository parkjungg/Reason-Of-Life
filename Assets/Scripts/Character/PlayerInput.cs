using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    void Update()
    {
        if (DialogueManager.instance != null && DialogueManager.instance.IsDialoguing)
        {
            MoveInput = Vector2.zero;
            return;
        }
        float x = 0f, y = 0f;

        if (Input.GetKey(KeyCode.A)) x = -1f;
        else if(Input.GetKey(KeyCode.D)) x = 1f;
        
        if(Input.GetKey(KeyCode.W)) y = 1f;
        else if(Input.GetKey(KeyCode.S)) y = -1f;

        if (x != 0f) y = 0f;
        
        MoveInput = new Vector2(x, y);
    }
}
