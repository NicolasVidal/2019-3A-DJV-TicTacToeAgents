using System.Collections.Generic;
using Contracts;
using Runners;

namespace Agents
{
    public class RandomRolloutAgent<TGameState> : IAgent<TGameState>
    where TGameState : IGameState<TGameState>, new()
    {
        private BasicBlockingRunner<TGameState> runner = new BasicBlockingRunner<TGameState>();

        private List<IAgent<TGameState>> rolloutAgents = new List<IAgent<TGameState>>
        {
            new RandomAgent<TGameState>(),
            new RandomAgent<TGameState>()
        };
        
        public (bool, int) Act(TGameState gameState, int playerId, List<int> availableActions)
        {
            var bestAction = int.MinValue;
            var bestScore = double.MinValue;
            
            for (var i = 0; i < availableActions.Count; i++)
            {
                var gs = gameState.Clone();
                gs.Step(playerId, availableActions[i]);
                var scores = runner.RunForNRounds(gs, rolloutAgents, 10000);
                if (scores[playerId] >= bestScore)
                {
                    bestAction = availableActions[i];
                    bestScore = scores[playerId];
                }
            }

            return (true, bestAction);
        }
    }
}