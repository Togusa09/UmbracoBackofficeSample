using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace UmbracoBackofficeSample.Trees
{
    [PluginController("CustomSection")]
    [Umbraco.Web.Trees.Tree("CustomSection", "CustomSectionTree", "Custom Section", iconClosed: "icon-doc")]
    public class CommerceManagementTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            // Create a new node in the menu tree, that will open the specified url
            var item = this.CreateTreeNode("1a", id, queryStrings, "Find Custom Item", "developerMacro.gif", false, "CustomSection/CustomSectionTree/ListCustomItem/0");
            nodes.Add(item);

            return nodes;
        }
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            menu.DefaultMenuAlias = ActionNew.Instance.Alias;
            return menu;
        }
    }
}