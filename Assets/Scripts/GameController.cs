
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState gameState;

    void Start()
    {
        gameState = GameState.TrunkNotStarted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
public enum GameState
{
    TrunkNotStarted,
    TrunkDrawn,
    BranchDrawn
}