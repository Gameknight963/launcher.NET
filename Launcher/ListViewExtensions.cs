using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    internal static class ListViewExtensions
    {
        public static void UpdateGameList(this ListView gamesView, LauncherData? data)
        {
            if (data == null) return;

            gamesView.Items.Clear();

            foreach (GameInfo game in data.Versions)
            {
                ListViewItem item = new ListViewItem(game.Label);
                item.SubItems.Add(game.GameName);
                item.Tag = game;
                gamesView.Items.Add(item);
            }
        }
        public static ListViewItem? FirstSelectedItem(this ListView listView)
        {
            return 0 >= 0 && 0 < listView.SelectedItems.Count
                ? listView.SelectedItems[0]
                : null;
        }
        public static int? FirstSelectedIndex(this ListView listView)
        {
            return 0 >= 0 && 0 < listView.SelectedItems.Count
                ? listView.SelectedIndices[0]
                : null;
        }

        public static void SelectVisibleIndex(this ListView listView, int offset)
        {
            if (listView.TopItem == null) return;

            int startIndex = listView.TopItem.Index;
            int targetIndex = startIndex + offset;

            if (targetIndex >= 0 && targetIndex < listView.Items.Count)
            {
                listView.Items[targetIndex].Selected = true;
                listView.Items[targetIndex].Focused = true;
                listView.EnsureVisible(targetIndex);
            }
        }
    }
}
