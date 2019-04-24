using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace Runners
{
    public class BasicBlockingRunner<TGameState>
        where TGameState : IGameState<TGameState>, new()
    {
        public List<double> RunForNRounds(TGameState gameState,
            List<IAgent<TGameState>> agents,
            int rounds)
        {
            var accumulatedScores = new double[agents.Count];

            for (var r = 0; r < rounds; r++)
            {
                var gs = gameState.Clone();

                while (!gs.IsGameOver())
                {
                    var currentPlayer = gs.GetCurrentPlayer();

                    var availableActions = gs.GetAvailableActionsForPlayer(currentPlayer);
                    var (ready, chosenAction) = agents[currentPlayer]
                        .Act(gs,
                            currentPlayer,
                            availableActions);

                    if (!ready)
                    {
                        continue;
                    }

                    gs.Step(currentPlayer, chosenAction);
                }

                for (var i = 0; i < agents.Count; i++)
                {
                    accumulatedScores[i] += gs.GetScoreForPlayer(i);
                }
            }

            return accumulatedScores.ToList();
        }
    }
}