using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TicTacToe
{
    public partial class MainPage
    {
        private GameDifficulty PlayerX = GameDifficulty.Human;
        private GameDifficulty PlayerO = GameDifficulty.Experienced;
        private bool IsFirstX = true; //TODO: Make this a user setting

        private int turn = -1;
        private int xWon = 0;
        private int oWon = 0;
        private int catsGame = 0;

        private const string sCross = "X";
        private const string sZero = "O";
        private const string sTagRed = "Red";
        private const string sTagGreen = "Green";

        private void NextTurn()
        {
            turn = -turn;

            if (turn == 1)
            {
                switch (PlayerX)
                {
                    case GameDifficulty.Novice:
                        BeginnerMove();
                        break;

                    case GameDifficulty.Intermediate:
                        IntermediateMove();
                        break;

                    case GameDifficulty.Experienced:
                        ExperiencedMove();
                        break;

                    case GameDifficulty.Expert:
                        PerfectMove();
                        break;
                }
            }
            else
            {
                switch (PlayerO)
                {
                    case GameDifficulty.Novice:
                        BeginnerMove();
                        break;

                    case GameDifficulty.Intermediate:
                        IntermediateMove();
                        break;

                    case GameDifficulty.Experienced:
                        ExperiencedMove();
                        break;

                    case GameDifficulty.Expert:
                        PerfectMove();
                        break;
                }
            }
        }

        private int GetLegalMoves(int state)
        {
            int moves = 0;

            for (int i = 0; i < 9; i++)
            {
                if ((state & (1 << (i * 2 + 1))) == 0)
                {
                    moves |= 1 << i;
                }
            }

            return moves;
        }

        private void MoveRandom(int moves)
        {
            int numMoves = 0;

            for (int i = 0; i < 9; i++)
            {
                if ((moves & (1 << i)) != 0) numMoves++;
            }

            if (numMoves > 0)
            {
                int moveNum = (int)Math.Ceiling(GetRandomNumber() * (double)numMoves);
                numMoves = 0;

                for (int j = 0; j < 9; j++)
                {
                    if ((moves & (1 << j)) != 0) numMoves++;

                    if (numMoves == moveNum)
                    {
                        Move(cells[j]);
                        return;
                    }
                }
            }
        }

        private int OpeningBook(int state)
        {
            int mask = state & 0x2AAAA;

            if (mask == 0x00000) return 0x1FF;
            if (mask == 0x00200) return 0x145;
            if (mask == 0x00002 || mask == 0x00020 || mask == 0x02000 || mask == 0x20000) return 0x010;
            if (mask == 0x00008) return 0x095;
            if (mask == 0x00080) return 0x071;
            if (mask == 0x00800) return 0x11C;
            if (mask == 0x08000) return 0x152;

            return 0;
        }

        private void PerfectMove()
        {
            int state = GetState();
            int winner = DetectWin(state);

            if (winner == 0)
            {
                int moves = GetLegalMoves(state);
                int hope = -999;
                int goodMoves = OpeningBook(state);

                if (goodMoves == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if ((moves & (1 << i)) != 0)
                        {
                            int value = MoveValue(state, i, turn, turn, 15, 1);

                            if (value > hope)
                            {
                                hope = value;
                                goodMoves = 0;
                            }

                            if (hope == value)
                            {
                                goodMoves |= (1 << i);
                            }
                        }
                    }
                }

                MoveRandom(goodMoves);
            }
        }

        private int MoveValue(int istate, int move, int moveFor, int nextTurn, int limit, int depth)
        {
            int state = StateMove(istate, move, nextTurn);
            int winner = DetectWin(state);

            if ((winner & 0x300000) == 0x300000)
            {
                return 0;
            }
            else if (winner != 0)
            {
                if (moveFor == nextTurn)
                    return 10 - depth;
                else
                    return depth - 10;
            }

            int hope = 999;

            if (moveFor != nextTurn)
                hope = -999;

            if (depth == limit)
                return hope;

            int moves = GetLegalMoves(state);

            for (int i = 0; i < 9; i++)
            {
                if ((moves & (1 << i)) != 0)
                {
                    int value = MoveValue(state, i, moveFor, -nextTurn, 10 - Math.Abs(hope), depth + 1);

                    if (Math.Abs(value) != 999)
                    {
                        if (moveFor == nextTurn && value < hope)
                            hope = value;
                        else if (moveFor != nextTurn && value > hope)
                            hope = value;
                    }
                }
            }

            return hope;
        }

        private int DetectWinMove(int state, int cellNum, int nextTurn)
        {
            int value = 0x3;

            if (nextTurn == -1)
                value = 0x2;

            int newState = state | (value << cellNum * 2);

            return DetectWin(newState);
        }

        private void BeginnerMove()
        {
            int state = GetState();
            int winner = DetectWin(state);

            if (winner == 0)
                MoveRandom(GetLegalMoves(state));
        }

        private int GetGoodMove(int state)
        {
            int moves = GetLegalMoves(state);

            for (int i = 0; i < 9; i++)
            {
                if ((moves & (1 << i)) != 0)
                {
                    if (DetectWinMove(state, i, turn) == 1)
                    {
                        Move(cells[i]);
                        return 0;
                    }
                }
            }

            for (int j = 0; j < 9; j++)
            {
                if ((moves & (1 << j)) != 0)
                {
                    if (DetectWinMove(state, j, -turn) == 1)
                    {
                        Move(cells[j]);
                        return 0;
                    }
                }
            }

            return moves;
        }

        private void IntermediateMove()
        {
            int state = GetState();
            int winner = DetectWin(state);

            if (winner == 0)
                MoveRandom(GetGoodMove(state));
        }

        private void ExperiencedMove()
        {
            int state = GetState();
            int winner = DetectWin(state);

            if (winner == 0)
            {
                int moves = OpeningBook(state);

                if (state == 0)
                    moves = 0x145;

                if (moves == 0)
                    moves = GetGoodMove(state);

                MoveRandom(moves);
            }
        }

        private int GetState()
        {
            int state = 0;

            for (int i = 0; i < 9; i++)
            {
                TextBlock cell = cells[i];
                int value = 0;

                if (cell.Text == sCross)
                    value = 0x3;

                if (cell.Text == sZero)
                    value = 0x2;

                state |= value << (i * 2);
            }

            return state;
        }

        private int DetectWin(int state)
        {
            if ((state & 0x3F000) == 0x3F000) return 0x13F000;
            if ((state & 0x3F000) == 0x2A000) return 0x22A000;
            if ((state & 0x00FC0) == 0x00FC0) return 0x100FC0;
            if ((state & 0x00FC0) == 0x00A80) return 0x200A80;
            if ((state & 0x0003F) == 0x0003F) return 0x10003F;
            if ((state & 0x0003F) == 0x0002A) return 0x20002A;
            if ((state & 0x030C3) == 0x030C3) return 0x1030C3;
            if ((state & 0x030C3) == 0x02082) return 0x202082;
            if ((state & 0x0C30C) == 0x0C30C) return 0x10C30C;
            if ((state & 0x0C30C) == 0x08208) return 0x208208;
            if ((state & 0x30C30) == 0x30C30) return 0x130C30;
            if ((state & 0x30C30) == 0x20820) return 0x220820;
            if ((state & 0x03330) == 0x03330) return 0x103330;
            if ((state & 0x03330) == 0x02220) return 0x202220;
            if ((state & 0x30303) == 0x30303) return 0x130303;
            if ((state & 0x30303) == 0x20202) return 0x220202;
            if ((state & 0x2AAAA) == 0x2AAAA) return 0x300000;

            return 0;
        }

        private void RecordWin(int winner)
        {
            if ((winner & 0x300000) == 0x100000)
                xWon++;
            else if ((winner & 0x300000) == 0x200000)
                oWon++;
            else if ((winner & 0x300000) == 0x300000)
                catsGame++;
        }

        private void ClearStats()
        {
            xWon = 0;
            oWon = 0;
            catsGame = 0;
        }

        private void DrawState(int state)
        {
            int winner = DetectWin(state);

            if ((winner & 0x300000) != 0)
            {
                if ((winner & 0x300000) == 0x100000)
                {
                    xWon++;
                }
                else if ((winner & 0x300000) == 0x200000)
                {
                    oWon++;
                }
                else
                {
                    catsGame++;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                string value = String.Empty;

                if ((state & (1 << (i * 2 + 1))) != 0)
                {
                    if ((state & (1 << (i * 2))) != 0)
                        value = sCross;
                    else
                        value = sZero;
                }

                Border wrapper = cells[i].Parent as Border;

                if ((winner & (1 << (i * 2 + 1))) != 0)
                {
                    if (wrapper != null)
                        wrapper.Background = new SolidColorBrush(Color.FromArgb(0xff, 0, 0x80, 0));
                }
                else
                {
                    if (wrapper != null)
                        wrapper.Background = null;
                }

                cells[i].Text = value;
            }
        }

        private int StateMove(int state, int move, int nextTurn)
        {
            int value = 0x3;

            if (nextTurn == -1)
                value = 0x2;

            return (state | (value << (move * 2)));
        }

        private void Move(TextBlock cell)
        {
            if (cell.Text == String.Empty)
            {
                int state = GetState();
                int winner = DetectWin(state);

                if (winner == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (cells[i] == cell)
                        {
                            state = StateMove(state, i, turn);
                        }
                    }

                    DrawState(state);
                    NextTurn();
                }
            }
        }

        private int CountMoves(int state)
        {
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                if ((state & (1 << (i * 2 + 1))) != 0)
                {
                    count++;
                }
            }

            return count;
        }

        private void NewGame()
        {
            int state = GetState();
            int winner = DetectWin(state);

            if (winner == 0 && CountMoves(state) > 1)
            {
                if (turn == 1)
                    oWon++;
                else
                    xWon++;
            }

            DrawState(0);

            if (IsFirstX)
                turn = -1;
            else
                turn = 1;

            NextTurn();
        }

        private double GetRandomNumber()
        {
            Random random = new Random();
            return random.NextDouble();
        }
    }

    public enum GameDifficulty
    {
        Human,
        Novice,
        Intermediate,
        Experienced,
        Expert
    }
}
