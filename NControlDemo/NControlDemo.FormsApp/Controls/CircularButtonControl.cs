/************************************************************************
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2025 - Christian Falch
 * 
 * Permission is hereby granted, free of charge, to any person obtaining 
 * a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject 
 * to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************/

using System;
using NControl.Abstractions;
using NGraphics;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NControlDemo.FormsApp.Controls
{
    /// <summary>
    /// Circular button control.
    /// </summary>
    public class CircularButtonControl: NControlView
    {
        private readonly Label _label;
        private readonly NControlView _circles;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls.CircularButtonControl"/> class.
        /// </summary>
        public CircularButtonControl()
        {
            HeightRequest = 44;
            WidthRequest = 44;

            _label = new FontAwesomeLabel
            {
                Text = FontAwesomeLabel.FAAdjust,
                TextColor = Xamarin.Forms.Color.White,
                FontSize = 17,
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center,
                VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center,
            };
            
            _circles = new NControlView {
                
                DrawingFunction = (canvas1, rect) => {
                    var fillColor = new NGraphics.Color(FillColor.R,
                        FillColor.G, FillColor.B, FillColor.A);
                    
                    canvas1.FillEllipse(rect, fillColor);
                    rect.Inflate(new NGraphics.Size(-2, -2));
                    canvas1.FillEllipse(rect, Colors.White);
                    rect.Inflate(new NGraphics.Size(-4, -4));
                    canvas1.FillEllipse(rect, fillColor);
                }    
            };
            
            Content = new Grid{
                Children = {
                    _circles,
                    _label,
                }
            };
        }

        /// <summary>
        /// The Command property.
        /// </summary>
        public static BindableProperty CommandProperty = 
            BindableProperty.Create<CircularButtonControl, ICommand> (p => p.Command, null,
                propertyChanged: (bindable, oldValue, newValue) => {
                var ctrl = (CircularButtonControl)bindable;
                ctrl.Command = newValue;
            });

        /// <summary>
        /// Gets or sets the Command of the CircularButtonControl instance.
        /// </summary>
        /// <value>The color of the buton.</value>
        public ICommand Command {
            get{ return (ICommand)GetValue (CommandProperty); }
            set {
                SetValue (CommandProperty, value);
            }
        }

        /// <summary>
        /// The CommandParameter property.
        /// </summary>
        public static BindableProperty CommandParameterProperty = 
            BindableProperty.Create<CircularButtonControl, object> (p => p.CommandParameter, null,
                propertyChanged: (bindable, oldValue, newValue) => {
                var ctrl = (CircularButtonControl)bindable;
                ctrl.CommandParameter = newValue;
            });

        /// <summary>
        /// Gets or sets the CommandParameter of the CircularButtonControl instance.
        /// </summary>
        /// <value>The color of the buton.</value>
        public object CommandParameter {
            get{ return (object)GetValue (CommandParameterProperty); }
            set {
                SetValue (CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// The FillColor property.
        /// </summary>
        public static BindableProperty FillColorProperty = 
            BindableProperty.Create<CircularButtonControl, Xamarin.Forms.Color>(p => p.FillColor, 
                Xamarin.Forms.Color.Gray, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CircularButtonControl)bindable;
                    ctrl.FillColor = newValue;
                });

        /// <summary>
        /// Gets or sets the FillColor of the CircularButtonControl instance.
        /// </summary>
        /// <value>The color of the buton.</value>
        public Xamarin.Forms.Color FillColor
        {
            get{ return (Xamarin.Forms.Color)GetValue(FillColorProperty); }
            set
            {
                SetValue(FillColorProperty, value);
                _circles.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the FA icon.
        /// </summary>
        /// <value>The FA icon.</value>
        public string FAIcon
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);
            this.ScaleTo(0.8, 65, Easing.CubicInOut);
            return true;
        }

        public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);
            this.ScaleTo(1.0, 65, Easing.CubicInOut);
            return true;
        }

        public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);
            this.ScaleTo(1.0, 65, Easing.CubicInOut);
            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);
            
            return true;
        }
    }
}

