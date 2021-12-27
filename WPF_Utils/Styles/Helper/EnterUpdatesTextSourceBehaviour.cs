using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WPF_Utils.Styles.Helper
{
    /// <summary>
    /// generic helper for supporting attached behaviours in style ressources
    /// see https://stackoverflow.com/questions/1647815/how-to-add-a-blend-behavior-in-a-style-setter
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <typeparam name="TBehavior"></typeparam>
    public class AttachableForStyleBehavior<TComponent, TBehavior> : Behavior<TComponent>
            where TComponent : DependencyObject
            where TBehavior : AttachableForStyleBehavior<TComponent, TBehavior>, new()
    {
        private static readonly DependencyProperty IsEnabledForStyleProperty =
            DependencyProperty.RegisterAttached("IsEnabledForStyle", typeof(bool),
            typeof(AttachableForStyleBehavior<TComponent, TBehavior>), new FrameworkPropertyMetadata(false, OnIsEnabledForStyleChanged));

        // Get
        public static bool GetIsEnabledForStyle(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledForStyleProperty);
        }

        // Set
        public static void SetIsEnabledForStyle(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledForStyleProperty, value);
        }

        //public bool IsEnabledForStyle
        //{
        //    get { return (bool)GetValue(IsEnabledForStyleProperty); }
        //    set { SetValue(IsEnabledForStyleProperty, value); }
        //}

        private static void OnIsEnabledForStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uie)
            {
                var behColl = Interaction.GetBehaviors(uie);
                var existingBehavior = behColl.FirstOrDefault(b => b.GetType() ==
                      typeof(TBehavior)) as TBehavior;

                if ((bool)e.NewValue == false && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }

                else if ((bool)e.NewValue == true && existingBehavior == null)
                {
                    behColl.Add(new TBehavior());
                }
            }
        }
    }
    /// <summary>
    /// Updates the binded source of a text box if the user clicks Enter
    /// for integrating into a style resource
    /// see https://stackoverflow.com/questions/1647815/how-to-add-a-blend-behavior-in-a-style-setter
    /// </summary>
    public class EnterUpdatesTextSourceAttachedBehaviour : AttachableForStyleBehavior<TextBox, EnterUpdatesTextSourceAttachedBehaviour>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (GetIsEnabledForStyle((DependencyObject)sender))
                {
                    var obj = sender as UIElement;
                    BindingExpression textBinding = BindingOperations.GetBindingExpression(
                        obj, TextBox.TextProperty);

                    if (textBinding != null)
                        textBinding.UpdateSource();
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }

    }
}
