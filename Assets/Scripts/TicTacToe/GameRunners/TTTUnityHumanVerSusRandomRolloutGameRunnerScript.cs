using System;
using Agents;
using Contracts;
using Runners;
using TicTacToe.Agents;
using UnityEngine.UI;

namespace TicTacToe.GameRunners
{
    public class TTTUnityHumanVerSusRandomRolloutGameRunnerScript : UnityGameRunnerScript<TicTacToeGameState>
    {
        
        public TTTUnityUIAgentScript agent0;      
        private readonly RandomRolloutAgent<TicTacToeGameState> agent1 = new RandomRolloutAgent<TicTacToeGameState>();          
        
        protected override IAgent<TicTacToeGameState> GetAgent(int agentId)
        {
            if (agentId == 0)
            {
                return agent0;
            }
            
            if (agentId == 1)
            {
                return agent1;
            }
            
            throw new Exception("Agent Not Available !");
        }

        protected override int GetAgentCount()
        {
            return 2;
        }
    }
}