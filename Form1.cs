using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        enum PlayerTurn {None, Player1, Player2 };
        enum Winner { None, Player1, Player2, Draw };
        
        PlayerTurn turn;
        Winner winner;

        void onNewGame()
        {
            PictureBox[] allPictures = { pictureBox0, pictureBox1, pictureBox2,
                                         pictureBox3, pictureBox4, pictureBox5,
                                         pictureBox6, pictureBox7, pictureBox8,
                                        };

            //limpa o tabuleiro
            foreach (var p in allPictures)
                p.Image = null;

            turn = PlayerTurn.Player1;
            winner = Winner.None;
            ShowTurn();            
        }
        /*       
            +---+---+---+
            | 0 | 1 | 2 |
            +---+---+---+
            | 3 | 4 | 5 |
            +---+---+---+
            | 6 | 7 | 8 |
            +---+---+---+
         */

        Winner GetWinner()
        {
            PictureBox[] allWinningMoves = { 
                                                //verifica as linhas
                                                pictureBox0, pictureBox1, pictureBox2,
                                                pictureBox3, pictureBox4, pictureBox5,
                                                pictureBox6, pictureBox7, pictureBox8,
                                                //verifica as colunas
                                                pictureBox0, pictureBox3, pictureBox6,
                                                pictureBox1, pictureBox4, pictureBox7,
                                                pictureBox2, pictureBox5, pictureBox8,
                                                //verifica as diagonais
                                                pictureBox0, pictureBox4, pictureBox8,
                                                pictureBox2, pictureBox4, pictureBox6,
                                            };

            for (int count = 0; count < allWinningMoves.Length; count +=3)
            {
                if (allWinningMoves[count].Image != null)
                {
                    if (allWinningMoves[count].Image == allWinningMoves[count+1].Image
                        && allWinningMoves[count].Image == allWinningMoves[count + 2].Image)
                    {
                        if (allWinningMoves[count].Image == pbPlayer1.Image)
                            return Winner.Player1;
                        else
                            return Winner.Player2;

                    }   
                }
            }
            //verifica células vazias
            PictureBox[] allPictures = { pictureBox0, pictureBox1, pictureBox2,
                                         pictureBox3, pictureBox4, pictureBox5,
                                         pictureBox6, pictureBox7, pictureBox8,
                                        };

            //limpa o tabuleiro
            foreach (var p in allPictures)
                if (p.Image == null)
                    return Winner.None;

            //verifica o vencedor
            return Winner.Draw;
        }

        void ShowTurn()
        {
            string status = "TURNO\n" + Player1.Text;

            switch (winner)
            {                 
                case Winner.None:                    
                    if (turn == PlayerTurn.Player1)
                        status = "TURNO\n" + Player1.Text;
                    else
                        status = "TURNO\n" + Player2.Text;
                    break;                
                case Winner.Player1:
                    status = Player1.Text + "VENCEU A PARTIDA";
                    break;
                case Winner.Player2:
                    status = Player2.Text + "VENCEU A PARTIDA";
                    break;
                default:
                    status = "VELHA - NINGUÉM VENCEU A PARTIDA";
                    break;
            }
            lblStatus.Text = status;
        }

        public Form1()
        {
            InitializeComponent();
        }       

        private void OnClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb.Image == null)
            {
                if (turn != PlayerTurn.None)
                {
                    if (turn == PlayerTurn.Player1)
                        pb.Image = pbPlayer1.Image;
                    else
                        pb.Image = pbPlayer2.Image;
                }
                //verifica o vencedor
                winner = GetWinner();
                if (winner == Winner.None)
                {
                    //troca de turnos
                    turn = (PlayerTurn.Player1 == turn) ? PlayerTurn.Player2 : PlayerTurn.Player1;
                }
                else 
                {
                    turn = PlayerTurn.None;
                }
                ShowTurn();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            onNewGame();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            var newGame = MessageBox.Show("Tem certeza que deseja iniciar um novo jogo?",
                                          "Novo Jogo",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (newGame == DialogResult.Yes)
                onNewGame();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var question = MessageBox.Show("Tem certeza que deseja fechar o jogo?",
                                          "Encerrar Jogo",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (question == DialogResult.No)
                e.Cancel = true;
        }
    }
}