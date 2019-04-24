using System.Collections.Generic;

namespace Contracts
{
    public interface IGameState<TGameState>
        where TGameState : IGameState<TGameState>, new()
    {
        void Reset();
        string ConvertToString();
        double Step(int playerId, int actionId);
        TGameState Clone();
        bool IsGameOver();
        double GetScoreForPlayer(int playerId);
        List<int> GetAvailableActionsForPlayer(int playerId);
        int GetCurrentPlayer();
    }
}