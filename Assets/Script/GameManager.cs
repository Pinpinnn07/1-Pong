using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerControl player1;
    private Rigidbody2D player1rb;

    public PlayerControl player2;
    private Rigidbody2D player2rb;

    public BallControl ball;
    private Rigidbody2D ballrb;
    private CircleCollider2D ballcollider;

    private bool isDebugWindowShown = false;

    public trajectory traject;

    public int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        player1rb = player1.GetComponent<Rigidbody2D>();
        player2rb = player2.GetComponent<Rigidbody2D>();
        ballrb = ball.GetComponent<Rigidbody2D>();
        ballcollider = ball.GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35,120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);

        }

        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYERONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = ballrb.mass;
            Vector2 ballVelocity = ballrb.velocity;
            float ballSpeed = ballrb.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballcollider.friction;

            float impulsePlayer1x = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2x = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball Velocity = " + ballVelocity + "\n" +
                "Ball Speed = " + ballSpeed + "\n" +
                "Ball Momentum = " + ballMomentum + "\n" +
                "Ball Friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1x + "," + impulsePlayer1y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2x + "," + impulsePlayer2y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;

        }

        if(GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            traject.enabled = !traject.enabled;
        }
    }
}
