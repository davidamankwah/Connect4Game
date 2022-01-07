using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

namespace Connect4
{
    public partial class MainPage : ContentPage
    {
        //Global varuable
        const int MAX_ROWS = 6;
        const int MAX_COLS = 7;
        int playerTurn;
        int colorturn = 1;
        int row = 6, row2 = 6, row3 = 6, row4 = 6, row5 = 6;
        int col;
        Color colorPiece;
        BoxView bv;
        BoxView yellow;
        BoxView red;
        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            playerTurn = 1;

            bv = (BoxView)sender;
            // move the boxview.
            double yDistance = 5 * (GridC4.Height / 8);
            await bv.TranslateTo(0, yDistance, 600);
            bv.TranslationY = 0; // reset to avoid unexpected mov
           
            GridC4.RaiseChild(ImgC4Grid);   // raise the z axis value

            //create tap gesture
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
            //drop red piece for player 1
            if (playerTurn == 1)
            {
                red = new BoxView();
                red.BackgroundColor = Color.Red;

                red.HeightRequest = GridC4.Height / 7;
                red.WidthRequest = GridC4.Width / 6;
                red.CornerRadius = red.WidthRequest / 2;
                red.SetValue(Grid.RowProperty, r);
                red.SetValue(Grid.ColumnProperty, c);
                GridC4.Children.Add(red);
                LblFeedback.Text = "Player1 turn";
                colorPiece = red.BackgroundColor;
                playerTurn = 2;
            }

            //drop yellow piece for player 2
            else if (playerTurn == 2)
            {

                yellow = new BoxView();
                yellow.BackgroundColor = Color.Yellow;

                yellow.HeightRequest = GridC4.Height / 7;
                yellow.WidthRequest = GridC4.Width / 6;
                yellow.CornerRadius = yellow.WidthRequest / 2;
                yellow.SetValue(Grid.RowProperty, r);
                yellow.SetValue(Grid.ColumnProperty, c);
                GridC4.Children.Add(yellow);
                LblFeedback.Text = "Player2 turn";
                colorPiece = red.BackgroundColor;
                playerTurn = 1;

            }

        }

        private void BtnWriteFile_Clicked(object sender, EventArgs e)
        {
            // Environment Variables
            string fullFileName;
            string path;
            string line = "connect 4";

            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Path is in System.io
            fullFileName = Path.Combine(path, EntryFileName.Text);

            // call from Utils clas
            WriteToFile(fullFileName, line);
        }

        //use to write file
        public static void WriteToFile(string fullFileName, string StuffToWrite)
        {
            using (var writer = new StreamWriter(fullFileName, false))
            {
                writer.WriteLine(StuffToWrite);
            }
            
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            BoxView b = (BoxView)sender; //piece selection

            Rectangle originalRect = new Rectangle(b.X, b.Y, b.Width, b.Height);

            Rectangle smallRect = new Rectangle(b.X + b.Width / 2, b.Y,
                                                1, b.Height);
            //spin animation
            await AnimatesIn(b, smallRect);
            await AnimatesOut(b, originalRect);



            col = (int)b.GetValue(Grid.ColumnProperty);

            //check which column the piece will drop down the row
            if (col == 2)
            {
                DropPiece(row2, col);
                row2--;
                checkForWinner(colorPiece);
                checkForWinner(colorPiece);
            }

            if (col == 3)
            {
                
                DropPiece(row, col);
                row--;
                checkForWinner(colorPiece);
                checkForWinner(colorPiece);
            }

            if (col == 4)
            {
                DropPiece(row3, col);
                row3--;
                checkForWinner(colorPiece);
                checkForWinner(colorPiece);
            }

            if (col == 5)
            {
                DropPiece(row4, col);
                row4--;
                checkForWinner(colorPiece);
                checkForWinner(colorPiece);
            }

            if (col == 6)
            {
                DropPiece(row5, col);
                row5--;
                checkForWinner(colorPiece);
                checkForWinner(colorPiece);
            }

            //call method
            ColorTurn(b);
        }

        private void ColorTurn(BoxView bb)
        {
            if (colorturn == 1)
            {
                bb.BackgroundColor = Color.Red;
                colorturn = 2;
            }
            else if (colorturn == 2)
            {
                bb.BackgroundColor = Color.Yellow;
                colorturn = 1;
            }

        }

        private Task AnimatesIn(BoxView b, Rectangle r)
        {
            return b.LayoutTo(r, 350, Easing.SinIn);
        }

        private Task AnimatesOut(BoxView b, Rectangle r)
        {
            return b.LayoutTo(r, 350, Easing.SinOut);
        }

        private void BtnStartGame_Clicked(object sender, EventArgs e)
        {
            //remove pieces
            GridC4.Children.Remove(yellow);
            GridC4.Children.Remove(red);

            //reset row to start new game
            row = 6;
            row2 = 6;
            row3 = 6;
            row4 = 6;
            row5 = 6;
        }

        private void checkForWinner(Color c)
        {
            //check win
            for (int i =0; i < MAX_ROWS; i++) 
            {
                int count = 0;
                for (int x = 0; x < MAX_COLS; x++)
                {
                    if (c == Color.Red && count == 4)
                    {

                        LblFeedback.Text = "Player 1 wins";
                    }

                    else if (c == Color.Yellow && count == 4)
                    {

                        LblFeedback.Text = "Player 2 wins";
                    }
                }
                
            }
        }
    }
}
