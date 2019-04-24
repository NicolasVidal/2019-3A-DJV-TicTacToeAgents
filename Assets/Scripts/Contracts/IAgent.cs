using System.Collections.Generic;

namespace Contracts
{
    public interface IAgent<TGameState>
    {
        (bool, int) Act(TGameState gameState, int playerId, List<int> availableActions);
    }
}