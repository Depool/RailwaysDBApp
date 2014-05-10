using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using RailwaysDAL;

namespace RailwaysDBApp.Controllers
{
    internal static class PermissionManager
    {
        //private
        private static List<PERMISSION_TYPE> permissionTypes;
        private static int permissionID = int.MinValue;

        private static int getPermissionID(string permissionTag)
        {
            if (permissionTypes != null)
                foreach (PERMISSION_TYPE p in permissionTypes)
                    if (p.NAME == permissionTag)
                        return p.ID;
            return int.MaxValue;
        }

        //properties
        public static List<PERMISSION_TYPE> PermissionTypes
        {
            get
            {
                return permissionTypes;
            }
        }

        //public
        public static void LoadPermissionList()
        {
            RailwaysEntities context = RailwaysData.sharedContext;
            permissionTypes = (from rec in context.PERMISSION_TYPE select rec).ToList();
            foreach (PERMISSION_TYPE p in permissionTypes)
                p.NAME = p.NAME.Replace(" ", string.Empty);
        }

        public static void Authorized(int permission)
        {
            permissionID = permission;
        }

        //CheckPermission overloads
        public static void CheckPermission(Window el)
        {
            if (PermissionManager.getPermissionID(el.Tag as string) >= permissionID)
                el.Visibility = Visibility.Visible;
            else
                el.Visibility = Visibility.Collapsed;

            if (el.Content as Panel != null)
                PermissionManager.CheckPermission(el.Content as Panel);
        }

        public static void CheckPermission(Panel el)
        {
            if (PermissionManager.getPermissionID(el.Tag as string) >= permissionID)
                el.Visibility = Visibility.Visible;
            else
                el.Visibility = Visibility.Collapsed;

            foreach (UIElement elem in el.Children)
            {
                if (elem as Panel != null)
                    PermissionManager.CheckPermission(elem as Panel);
                else
                    if (elem as FrameworkElement != null)
                        PermissionManager.CheckPermission(elem as FrameworkElement);
            }
        }

        public static void CheckPermission(FrameworkElement el)
        {
            if (PermissionManager.getPermissionID(el.Tag as string) >= permissionID)
                el.Visibility = Visibility.Visible;
            else
                el.Visibility = Visibility.Collapsed;
        }
        //CheckPermission overloads end
    }
}

