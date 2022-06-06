using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.Services
{
    public class NavigationService
    {
        public event Action<Page> OnPageChanged;
        private Stack<Page> navigationHistory;
        public bool CanGoBack => navigationHistory.Any();
        public NavigationService()
        {
            navigationHistory = new Stack<Page>();
        }
        public void Navigate(Page page)
        {
            OnPageChanged?.Invoke(page);
            navigationHistory.Push(page);
        }
        public void GoBack()
        {
            navigationHistory.Pop();
            var page = navigationHistory.Peek();
            OnPageChanged?.Invoke(page);
        }
    }
}
