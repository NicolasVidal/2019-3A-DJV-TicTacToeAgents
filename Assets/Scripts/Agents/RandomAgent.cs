using System.Collections.Generic;
using Contracts;
using UnityEngine;

namespace Agents
{
    public class RandomAgent<TGameState> : IAgent<TGameState>
    {
        public (bool, int) Act(TGameState gameState, int playerId, List<int> availableActions)
        {
            var rdm = Random.Range(0, availableActions.Count);

            return (true, availableActions[rdm]);
        }
    }
}