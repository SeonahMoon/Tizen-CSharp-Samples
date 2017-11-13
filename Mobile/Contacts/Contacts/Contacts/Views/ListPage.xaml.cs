/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Contacts.Models;

namespace Contacts.Views
{
    public partial class ListPage : ContentPage
    {
        /// <summary>
        /// Creating a bindable property named "RecordList" of type List<RecordItem> which is owned by the ListPage and has default value of null.
        /// </summary>
        public static readonly BindableProperty RecordListProperty = BindableProperty.Create("RecordList", typeof(List<RecordItem>), typeof(ListPage), null);
        /// <summary>
        /// The RecordList is only updated by the ListPagetViewModel, so it is defined with the OneWay binding mode.
        /// Therefore, the property does not necessarily implement the setter.
        /// </summary>
        public List<RecordItem> RecordList
        {
            get
            {
                return (List<RecordItem>)GetValue(RecordListProperty);
            }
        }

        /// <summary>
        /// Creating a bindable property named "UpdateListCommandProperty" of type Command which is owned by the ListPage and has default value of null.
        /// </summary>
        public static readonly BindableProperty UpdateListCommandProperty = BindableProperty.Create("UpdateListCommand", typeof(Command), typeof(ListPage), null);
        /// <summary>
        /// The UpdateListCommand is only updated by the ListPageViewModel, so it is defined with the OneWay binding mode.
        /// Therefore, the property does not necessarily implement the setter.
        /// </summary>
        public Command UpdateListCommand
        {
            get
            {
                return (Command)GetValue(UpdateListCommandProperty);
            }
        }

        /// <summary>
        /// A method for updating list in ListPage.
        /// When RecordItem property is modified, this method will be called.
        /// </summary>
        private void UpdateList()
        {
            ListPageListView.ItemsSource = null;
            ListPageListView.ItemsSource = RecordList;
        }

        /// <summary>
        /// This is invoked when the bound properties are updated.
        /// The property is monitored to run the UpdateList method.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateList();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            RecordItem item = new RecordItem();
            ItemPage itemPage = new ItemPage(item, "Save", 0);
            await Navigation.PushAsync(itemPage);
        }

        /// <summary>
        /// This is invoked after the view appears on the screen.
        /// This handler executes the update command of the ListPageViewModel.
        /// The command is going to change the RecordList of the ListPageViewModel.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateListCommand.Execute(null);
        }

        public ListPage()
        {
            InitializeComponent();
            PropertyChanged += OnPropertyChanged;
            ListPageListView.ItemSelected += async (o, e) =>
            {
                RecordItem item = e.SelectedItem as RecordItem;
                ItemPage itemPage = new ItemPage(item, "Update", item.Index);
                await Navigation.PushAsync(itemPage);
            };

        }
    }
}