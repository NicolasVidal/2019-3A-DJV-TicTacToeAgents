using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace TicTacToe
{
    public class TicTacToeGameState : IGameState<TicTacToeGameState>
    {
        private int currentPlayer;
        private readonly int[,] board = new int[3, 3];
        private readonly int[] scores = new int[2];
        private bool gameOver;

        private static readonly Dictionary<int, string> cellToString = new Dictionary<int, string>
        {
            {-1, "_"},
            {0, "X"},
            {1, "O"}
        };

        public TicTacToeGameState()
        {
            Reset();
        }

        public void Reset()
        {
            currentPlayer = 0;
            scores[0] = 0;
            scores[1] = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    board[i, j] = -1;
                }
            }

            gameOver = false;
        }

        public string ConvertToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Current Player : {currentPlayer}");
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    sb.Append(cellToString[board[i, j]]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public double Step(int playerId, int actionId)
        {
            if (gameOver)
            {
                throw new Exception($"Sorry, game is already Over !");
            }
            
            if (playerId != currentPlayer)
            {
                throw new Exception($"Player {playerId} can't play yet !");
            }
            
            var i = actionId / 3;
            var j = actionId % 3;

            if (board[i, j] != -1)
            {
                throw new Exception($"Cell {i},{j} is not empty !");
            }

            board[i, j] = playerId;

            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0, 1] == playerId
                || board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 1] == playerId
                || board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 1] == playerId
                || board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[1, 0] == playerId
                || board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[1, 1] == playerId
                || board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[1, 2] == playerId
                || board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[1, 1] == playerId
                || board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[1, 1] == playerId)
            {
                gameOver = true;
                scores[playerId] = 1;
                scores[(playerId + 1) % 2] = -1;
                return scores[playerId];
            }

            for (var ii = 0; ii < 3; ii++)
            {
                for (var jj = 0; jj < 3; jj++)
                {
                    if (board[ii, jj] != -1)
                    {
                        continue;
                    }
                    currentPlayer = (currentPlayer + 1) % 2;
                    return 0;
                }
            }

            gameOver = true;
            return scores[playerId];
        }

        public TicTacToeGameState Clone()
        {
            var copy = new TicTacToeGameState();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    copy.board[i, j] = board[i, j];
                }
            }

            copy.currentPlayer = currentPlayer;
            copy.scores[0] = scores[0];
            copy.scores[1] = scores[1];
            copy.gameOver = gameOver;
            
            return copy;
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public double GetScoreForPlayer(int playerId)
        {
            return scores[playerId];
        }

        public List<int> GetAvailableActionsForPlayer(int playerId)
        {
            if (playerId != currentPlayer)
            {
                return new List<int>();
            }
            
            var availableActions = new List<int>(9);
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board[i, j] == -1)
                    {
                        availableActions.Add(i * 3 + j);
                    }
                }
            }

            return availableActions;
        }

        public int GetCurrentPlayer()
        {
            return currentPlayer;
        }
    }
}