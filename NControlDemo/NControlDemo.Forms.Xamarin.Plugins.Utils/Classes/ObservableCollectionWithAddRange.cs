/****************************** Module Header ******************************\
Module Name:  ObservableCollectionWithAddRange.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NControlDemo.Forms.Xamarin.Plugins.Classes
{
    public class ObservableCollectionWithAddRange<T>: ObservableCollection<T>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableCollectionWithAddRange()
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items"></param>
        public ObservableCollectionWithAddRange(IEnumerable<T> items):base(items)
        {
        
        }
        #endregion

        #region Public Members

        /// <summary>
        /// Adds the range of new items
        /// </summary>
        /// <param name="list"></param>
        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
                Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion
    }
}
