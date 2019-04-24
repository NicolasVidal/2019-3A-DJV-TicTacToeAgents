using Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Runners
{
    public abstract class UnityGameRunnerScript<TGameState> : MonoBehaviour
        where TGameState : IGameState<TGameState>, new()
    {
        public Text GameStateText;

        private readonly TGameState gameState = new TGameState();

        protected abstract IAgent<TGameState> GetAgent(int agentId);
        protected abstract int GetAgentCount();

        void Start()
        {
            GameStateText.text = gameState.ConvertToString();
        }
        
        void Update()
        {
            if (gameState.IsGameOver())
            {
                gameState.Reset();
                GameStateText.text = gameState.ConvertToString();
            }

            var currentPlayer = gameState.GetCurrentPlayer();

            var availableActions = gameState.GetAvailableActionsForPlayer(currentPlayer);
            var (ready, chosenAction) = GetAgent(currentPlayer)
                .Act(gameState,
                    currentPlayer,
                    availableActions);

            if (!ready)
            {
                return;
            }

            gameState.Step(currentPlayer, chosenAction);
            GameStateText.text = gameState.ConvertToString();

            if (gameState.IsGameOver())
            {
                Debug.Log($"Game Over, Final Scores : ");
                for (var i = 0; i < GetAgentCount(); i++)
                {
                    Debug.Log($"Agent 1 : {gameState.GetScoreForPlayer(i)}");
                }
            }
        }
    }
}