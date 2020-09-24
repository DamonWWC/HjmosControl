using System.Windows;

namespace Hjmos.BaseControls.Interactivity
{
    public interface IAttachedObject
    {
        void Attach(DependencyObject dependencyObject);
        void Detach();

        DependencyObject AssociatedObject { get; }
    }
}
