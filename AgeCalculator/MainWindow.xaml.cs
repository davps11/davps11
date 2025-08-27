using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AgeCalculator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BirthCalendar_SelectedDatesChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (BirthCalendar.SelectedDate is null)
            return;

        var birthDate = BirthCalendar.SelectedDate.Value;
        var today = DateTime.Today;

        int years = today.Year - birthDate.Year;
        int months = today.Month - birthDate.Month;
        int days = today.Day - birthDate.Day;

        if (days < 0)
        {
            months--;
            var prevMonth = today.AddMonths(-1);
            days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        AgeText.Text = $"{years} years, {months} months, {days} days";

        var fade = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        AgeText.BeginAnimation(OpacityProperty, fade);
    }
}
