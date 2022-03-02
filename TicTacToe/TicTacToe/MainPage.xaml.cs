using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        #region Private fields
        private bool checkForWin = false;
        private bool checkForTie = false;
        private bool playWithAndroid = false;
        private int playWithAndroidClickCount = 0;
        private readonly string x = "X";
        private readonly string o = "0";
        private int player = 1;
        private int moveCount = 1;
        private readonly List<Button> btnList = new List<Button>();
        #endregion

        public MainPage()
        {
            InitializeComponent();
            InitialSettings();
            InitialLabelSettings();
            PopulateBtnList();
        }

        #region Helper methods
        private void InitialLabelSettings()
        {
            firstPlayerLabel.Text = "First \r\nplayer";
            secondPlayerLabel.Text = "Second \r\nplayer";
        }
        private void InitialSettings()
        {
            firstPlayerLabel.TextColor = Color.Green;
            secondPlayerLabel.TextColor = Color.Gray;
            player = 1;
            moveCount = 1;
            checkForWin = false;
            checkForTie = false;
        }
        void PopulateBtnList()
        {
            btnList.Add(btn11);
            btnList.Add(btn12);
            btnList.Add(btn13);

            btnList.Add(btn21);
            btnList.Add(btn22);
            btnList.Add(btn23);

            btnList.Add(btn31);
            btnList.Add(btn32);
            btnList.Add(btn33);

        }
        private void PlayerLabelColoring(Button button)
        {
            if (button.Text.Equals(x))
            {
                secondPlayerLabel.TextColor = Color.Green;
                firstPlayerLabel.TextColor = Color.Gray;
            }
            else
            {
                secondPlayerLabel.TextColor = Color.Gray;
                firstPlayerLabel.TextColor = Color.Green;
            }
        }
        private void ResetScore() 
        {
            firstPlayerLabelScore.Text = "0";
            secondPlayerLabelScore.Text = "0";
        }
        private void ResetFields()
        {
            foreach (var btn in btnList)
            {
                btn.Text = "";
                btn.IsEnabled = true;
            }
        }
        #endregion
        #region Check methods
        private bool CheckForWin(Button btn, string player)
        {
            //first row
            if (btn == btn11)
            {
                if (btn21.Text == player && btn31.Text == player)
                {
                    return true;
                }
                else if (btn12.Text == player && btn13.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn33.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn12)
            {
                if (btn11.Text == player && btn13.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn32.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn13)
            {
                if (btn23.Text == player && btn33.Text == player)
                {
                    return true;
                }
                else if (btn12.Text == player && btn11.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn31.Text == player)
                {
                    return true;
                }
            }

            //second row
            if (btn == btn21)
            {
                if (btn11.Text == player && btn31.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn23.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn22)
            {
                if (btn12.Text == player && btn32.Text == player)
                {
                    return true;
                }
                else if (btn21.Text == player && btn23.Text == player)
                {
                    return true;
                }
                else if (btn11.Text == player && btn33.Text == player)
                {
                    return true;
                }
                else if (btn31.Text == player && btn13.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn23)
            {
                if (btn13.Text == player && btn33.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn21.Text == player)
                {
                    return true;
                }
            }

            //third row
            if (btn == btn31)
            {
                if (btn21.Text == player && btn11.Text == player)
                {
                    return true;
                }
                else if (btn32.Text == player && btn33.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn13.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn32)
            {
                if (btn31.Text == player && btn33.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn12.Text == player)
                {
                    return true;
                }
            }
            if (btn == btn33)
            {
                if (btn13.Text == player && btn23.Text == player)
                {
                    return true;
                }
                else if (btn32.Text == player && btn31.Text == player)
                {
                    return true;
                }
                else if (btn22.Text == player && btn11.Text == player)
                {
                    return true;
                }
            }

            return false;
        }
        private bool CheckForTie()
        {
            foreach (var btn in btnList)
            {
                if (string.IsNullOrEmpty(btn.Text))
                {
                    return false;
                }
            };

            return true;
        }
        #endregion
        #region Button methods
        void ResetFieldsClick(object sender, EventArgs e)
        {
            ResetFields();
            InitialSettings();
        }
        void ResetGameClick(object sender, EventArgs e)
        {
            InitialSettings();
            ResetFields();
            ResetScore();
        }
        void PlayWithAndroidClick(object sender, EventArgs e)
        {
            if (playWithAndroidClickCount % 2 > 0)
            {
                playWithAndroid = false;
                playWithAndroidBtn.BackgroundColor = Color.Default;
            }
            else
            {
                playWithAndroid = true;
                playWithAndroidBtn.BackgroundColor = Color.Green;
            }

            InitialSettings();
            ResetFields();
            playWithAndroidClickCount++;
        }
        #endregion

        void OnBtnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = player % 2 > 0 ? x : o;

            if (moveCount > 4)
            {
                if (moveCount == 9)
                {
                    //this is only method to be sure
                    checkForTie = CheckForTie();
                }

                checkForWin = CheckForWin(button, button.Text);
            }

            if (checkForWin)
            {
                //set score
                if (button.Text == x)
                {
                    int result = Convert.ToInt32(firstPlayerLabelScore.Text);
                    result++;
                    firstPlayerLabelScore.Text = result.ToString();

                    whoWinsLabel.Text = "First player wins!";
                }
                else
                {
                    int result = Convert.ToInt32(secondPlayerLabelScore.Text);
                    result++;
                    secondPlayerLabelScore.Text = result.ToString();

                    whoWinsLabel.Text = "Second player wins!";
                }

                //set initialization settings
                InitialSettings();
                ResetFields();
            }
            else if (checkForTie) 
            {
                whoWinsLabel.Text = "Its a tie!";
                InitialSettings();
                ResetFields();
            }
            else
            {
                moveCount++;
                player++;
                button.IsEnabled = false;
                PlayerLabelColoring(button);
            }
        }
    }
}
