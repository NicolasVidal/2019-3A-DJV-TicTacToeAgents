using TicTacToe;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        var tttGs = new TicTacToeGameState();
        tttGs.Reset();
        Debug.Log(tttGs.ConvertToString());

        tttGs.Step(0, 0);
        Debug.Log(tttGs.ConvertToString());
        
        tttGs.Step(1, 4);
        Debug.Log(tttGs.ConvertToString());
        
        tttGs.Step(0, 1);
        Debug.Log(tttGs.ConvertToString());
        
        tttGs.Step(1, 5);
        Debug.Log(tttGs.ConvertToString());
        
        tttGs.Step(0, 2);
        Debug.Log(tttGs.ConvertToString());
        
        tttGs.Step(1, 7);
        Debug.Log(tttGs.ConvertToString());

    }
}