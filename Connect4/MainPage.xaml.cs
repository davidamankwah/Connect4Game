using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4
{
    public partial class MainPage : ContentPage
    {
        const int MAX_ROWS = 6;
        const int MAX_COLS = 7;
        int turn;
        int colorturn = 1;
        int startgame = 0;
        int row = 6, row2 = 6, row3 = 6, row4 = 6, row5 = 6;
        int col;
        int player1 = 0, player2=0;
        Color token;
        BoxView bv;
        BoxView q;
        BoxView sq;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            turn = 1;

            bv = (BoxView)sender;
            // move the boxview.
            double yDistance = 5 * (GridC4.Height / 8);
            await bv.TranslateTo(0, yDistance, 600);
            bv.TranslationY = 0; // reset after translation to avoid unexpected moving
           // bv.SetValue(Grid.RowProperty, 3);
            //bv.LayoutTo

            GridC4.RaiseChild(ImgC4Grid);   // raise the z axis value
            TapGestureRecognizer t = new TapGestureRecognizer();
            t.Tapped += TapGestureRecognizer_Tapped_1;

            bv.BackgroundColor = Color.Red;
            bv.HeightRequest = GridC4.Height / 8;
            bv.WidthRequest = GridC4.Width / 9;
            bv.CornerRadius = bv.WidthRequest / 2;
            bv.GestureRecognizers.Add(t);
            bv.SetValue(Grid.RowProperty, 0);
            bv.SetValue(Grid.ColumnProperty, 3);
            col = (int)bv.GetValue(Grid.ColumnProperty);

            GridC4.Children.Add(bv);
            GridC4.LowerChild(bv);


            
            
        }
        private void DropPiece(int r, int c)
        {
            if (turn == 1)
            {

                sq = new BoxView();
                sq.BackgroundColor = Color.Red;

                sq.HeightRequest = GridC4.Height / 7;
                sq.WidthRequest = GridC4.Width / 6;
                sq.CornerRadius = sq.WidthRequest / 2;
                sq.SetValue(Grid.RowProperty, r);
                sq.SetValue(Grid.ColumnProperty, c);
                GridC4.Children.Add(sq);
                LblFeedback.Text = "Player1 turn";
                token = sq.BackgroundColor;
                player1++;
                turn = 2;
            }

            else if (turn == 2)
            {

                q = new BoxView();
                q.BackgroundColor = Color.Yellow;

                q.HeightRequest = GridC4.Height / 7;
                q.WidthRequest = GridC4.Width / 6;
                q.CornerRadius = q.WidthRequest / 2;
                q.SetValue(Grid.RowProperty, r);
                q.SetValue(Grid.ColumnProperty, c);
                GridC4.Children.Add(q);
                LblFeedback.Text = "Player2 turn";
                token = sq.BackgroundColor;
                player2++;
                turn = 1;

            }

        }

        private void BtnWriteFile_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            BoxView b = (BoxView)sender;

            Rectangle originalRect = new Rectangle(b.X, b.Y, b.Width, b.Height);

            Rectangle smallRect = new Rectangle(b.X + b.Width / 2, b.Y,
                                                1, b.Height);

            await AnimateIn(b, smallRect);
            await AnimateOut(b, originalRect);



            col = (int)b.GetValue(Grid.ColumnProperty);
            if (col == 2)
            {
                DropPiece(row2, col);
                row2--;
                checkForWinner(token, player1);
                checkForWinner(token, player2);
            }

            if (col == 3)
            {
                
                DropPiece(row, col);
                row--;
                checkForWinner(token, player1);
                checkForWinner(token, player2);
            }

            if (col == 4)
            {
                DropPiece(row3, col);
                row3--;
                checkForWinner(token, player1);
                checkForWinner(token, player2);
            }

            if (col == 5)
            {
                DropPiece(row4, col);
                row4--;
                checkForWinner(token, player1);
                checkForWinner(token, player2);
            }

            if (col == 6)
            {
                DropPiece(row5, col);
                row5--;
                checkForWinner(token, player1);
                checkForWinner(token, player2);
            }


            if (colorturn == 1)
            {
                b.BackgroundColor = Color.Red;
                colorturn = 2;
            }
            else if (colorturn == 2)
            {
                b.BackgroundColor = Color.Yellow;
                colorturn = 1;
            }


        }

        private Task AnimateIn(BoxView b, Rectangle r)
        {
            return b.LayoutTo(r, 350, Easing.SinIn);
        }

        private Task AnimateOut(BoxView b, Rectangle r)
        {
            return b.LayoutTo(r, 350, Easing.SinOut);
        }

        private void BtnStartGame_Clicked(object sender, EventArgs e)
        {
            GridC4.Children.Remove(q);
            GridC4.Children.Remove(sq);

            row = 6;
            row2 = 6;
            row3 = 6;
            row4 = 6;
            row5 = 6; 

        }

        private void checkForWinner(Color c, int p)
        {
            for (int i =0; i < MAX_ROWS; i++) 
            {
                if (c == Color.Red && p == 4)
                {
                   
                    LblFeedback.Text = "Player 1 wins";
                }

                else if (c == Color.Yellow && p == 4)
                {
                   
                    LblFeedback.Text = "Player 2 wins";
                }
                
            }
        }
    }
}
