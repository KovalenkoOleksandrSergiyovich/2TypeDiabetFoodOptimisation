using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.Services
{
    public class NavigationService
    {
        //подія зміни сторінки
        public event Action<Page> OnPageChanged;
        //стек для збереження історії відвідувань
        private Stack<Page> navigationHistory;
        //метод для перевірки можливості повернутися на попередню сторінку
        public bool CanGoBack => navigationHistory.Any();
        //конструктор класу
        public NavigationService()
        {
            navigationHistory = new Stack<Page>();
        }
        //метод навігації на певну сторінку
        public void Navigate(Page page)
        {
            OnPageChanged?.Invoke(page);
            navigationHistory.Push(page);
        }
        //метод повернення на попередню сторінку в історії відвідувань
        public void GoBack()
        {
            navigationHistory.Pop();
            var page = navigationHistory.Peek();
            OnPageChanged?.Invoke(page);
        }
    }
}
