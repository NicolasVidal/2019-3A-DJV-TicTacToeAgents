using System.Collections.Generic;

namespace Contracts
{
    public interface IAgent<TGameState>
    {
        int Act(TGameState gameState, int playerId, List<int> availableActions);
    }
}