﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GymManagement.Commands
{
    public class MouseDragWindowCommand : BaseCommand
    {
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
        public override void Execute(object? parameter)
        {
            FrameworkElement window = GetWindowParent(parameter as UserControl);
            var w = window as Window;
            if (w != null)
            {
                w.DragMove();
            }
        }
    }
}
