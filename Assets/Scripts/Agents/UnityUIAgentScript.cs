using System.Collections.Generic;
using Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Agents
{
    public abstract class UnityUIAgentScript<TGameState> : MonoBehaviour,
        IAgent<TGameState>
    {
        public InputField playerInput;

        private int actionChosen;
        private bool actionReady;

        public void Start()
        {
            playerInput.onEndEdit.AddListener((text) =>
                {
                    int action;
                    if (!int.TryParse(text, out action))
                    {
                        return;
                    }
                    actionChosen = action;
                    actionReady = true;
                }
            );
        }
        
        public (bool, int) Act(TGameState gameState, int playerId, List<int> availableActions)
        {
            if (!actionReady)
            {
                return (false, -1);
            }

            actionReady = false;
            playerInput.text = "";
            return (true, actionChosen);
        }
    }
}