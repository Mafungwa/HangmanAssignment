using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
        private string[][] words = {
            [ "EASYWORD1", "EASYWORD2", "EASYWORD3" ],
            ["MEDIUMWORD1", "MEDIUMWORD2", "MEDIUMWORD3"],            
            ["HARDWORD1", "HARDWORD2", "HARDWORD3"]  };
        private string[] difficultyLevels = { "Easy", "Medium", "Hard" };

        private string wordToGuess;
        private List<char> guessedLetters;
        private int hangmanImageIndex;

        public HangmanGamePage()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame(int difficultyLevelIndex = 0)
        {
            wordToGuess = GetRandomWord(difficultyLevelIndex);
            guessedLetters = new List<char>();
            hangmanImageIndex = 0;

            HangmanImage.Source = $"hang{hangmanImageIndex + 1}.png";
            UpdateWordDisplay();
        }

        private string GetRandomWord(int difficultyLevelIndex)
        {
            string[] wordArray = words[difficultyLevelIndex];
            return wordArray[new Random().Next(wordArray.Length)];
        }

        private void UpdateWordDisplay()
        {
            string display = "";
            foreach (char letter in wordToGuess)
            {
                display += guessedLetters.Contains(letter) ? letter : "_";
                display += " ";
            }
            WordLabel.Text = display.Trim();
        }

        private void HandleGuess(char guess)
        {
            if (!guessedLetters.Contains(guess))
            {
                guessedLetters.Add(guess);
                if (!wordToGuess.Contains(guess))
                {
                    hangmanImageIndex++;
                    UpdateHangmanImage();
                }
                UpdateWordDisplay();
                CheckGameStatus();
            }
        }

        private void UpdateHangmanImage()
        {
            if (hangmanImageIndex >= 8)
                hangmanImageIndex = 8;

            HangmanImage.Source = $"hang{hangmanImageIndex + 1}.png";
        }

        private void CheckGameStatus()
        {
            if (wordToGuess.All(letter => guessedLetters.Contains(letter)))
            {
                DisplayAlert("Congratulations!", "You've guessed the word!", "OK");
                NewGame();
            }
            else if (hangmanImageIndex >= 8)
            {
                DisplayAlert("Game Over", "You've been hanged! The word was: " + wordToGuess, "OK");
                NewGame();
            }   
        }

        private void GuessButton_Clicked(object sender, EventArgs e)
        {
            char guess = GuessEntry.Text.First();
            HandleGuess(guess);
        }
    }
}
