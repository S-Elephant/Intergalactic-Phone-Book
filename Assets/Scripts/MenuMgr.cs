using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The manager responsible for all menus. It will create, destroy, activate, deactivate, store, etc. them.
/// Contains a reference to the active menu.
/// </summary>
public class MenuMgr : SingletonNonPersistent<MenuMgr>
{
    // Note: Below enums don't belong here but doing this properly everywhere in the project takes up too much time for such a tiny project.
    public enum ECrudMode { None = 0, Create, Update };
    public ECrudMode MenuCrudMode = ECrudMode.None;

    /// <summary>
    /// I don't like Tuples, therefor this container.
    /// </summary>
    private struct MenuContainer
    {
        public readonly GameObject TheGameObject;
        public readonly MenuBase Script;

        public MenuContainer(GameObject prefab, MenuBase script) => (TheGameObject, Script) = (prefab, script);
    }

    /// <summary>
    /// Contains all the menus including the inactive ones.
    /// </summary>
    private readonly Stack<MenuContainer> MenuData = new Stack<MenuContainer>();
    
    /// <summary>
    /// The transform to use as the parent for all menus.
    /// Note that this transform does not need to be assigned in the Inspector because it is created here.
    /// </summary>
    private Transform GuiTransform;
    
    protected override void Awake()
    {
        base.Awake();
        CreateCanvasAndEventSystem();
    }

    private void Start()
    {
        //GuiTransform = new GameObject("GUI").transform;
        CreateDefaultMenu();
    }

    /// <summary>
    /// Creates a Canvas and EventSystem in the scene.
    /// Imo this is a bit silly to do it dynamic but Hapbtic likes dynamic and this is even more dynamic.
    /// </summary>
    private void CreateCanvasAndEventSystem()
    {
        GameObject canvasObj = new GameObject("GUI");
        GuiTransform = canvasObj.transform;
        GameObject es = Instantiate(PrefabMgr.Instance.EventSystem);
        es.name = "EventSystem";
    }

    /// <summary>
    /// Creates the default menu, which is the LoginMenu.
    /// I didn't want to set the default menu dynamically because I want PrefabMgr
    /// to be the only place for storing prefab references as much as possible.
    /// </summary>
    private void CreateDefaultMenu()
    {
        CreateAndAddMenu(PrefabMgr.Instance.LoginMenu);
        MenuData.Peek().TheGameObject.name = "LoginMenu";
    }

    /// <summary>
    /// Returns the menu-script of the current active menu.
    /// If currently there's no menu active then it'll return null.
    /// </summary>
    public MenuBase GetCurrentMenuScript()
    {
        if (MenuData.Count > 0)
        {
            MenuContainer currentMenu = MenuData.Peek();
            return currentMenu.Script;
        }
        return null;
    }

    /// <summary>
    /// Deactivates the current menu (if any) and adds the specified menu to the data-list and then activates it.
    /// </summary>
    private void Push(GameObject menu)
    {
        // Deactivate the current active menu (if any).
        if (MenuData.Count > 0) { MenuData.Peek().Script.Deactivate(); }

        // Add it to the data-list.
        MenuContainer newMenuData = new MenuContainer(menu, menu.GetComponent<MenuBase>());
        MenuData.Push(newMenuData);

        if (newMenuData.Script == null) { throw new Exception("Please add a BaseMenu.cs script to the root of every menu (or have a script that inherits from it). Missing one for menu: " + newMenuData.TheGameObject.name); }

        newMenuData.Script.Activate();
    }

    /// <summary>
    /// Removes the current menu, deactivates and destroys it.
    /// </summary>
    private void RemoveAndDestroyCurrentMenu()
    {
        MenuContainer menuContainer = MenuData.Pop();
        menuContainer.Script.Deactivate();
        Destroy(menuContainer.TheGameObject);
    }

    /// <summary>
    /// Closes the current active menu by removing it from the menu-list, deactivating it and then destroying it.
    /// Will throw an exception if there's currently no active menu.
    /// </summary>
    public void CloseCurrentMenu()
    {
        if (MenuData.Count > 0)
        {
            RemoveAndDestroyCurrentMenu();
            if (MenuData.Count > 0)
            {
                MenuData.Peek().Script.Activate();
            }
        }
        else
        {
            throw new Exception("There's no menu active, thus no menu to close.");
        }
    }

    /// <summary>
    /// Creates a new menu from the prefab. Then it deactivates the current menu (if any)
    /// and adds the specified menu to the data-list and then activates it.
    /// </summary>
    public void CreateAndAddMenu(GameObject menuPrefab)
    {
        Push(Instantiate(menuPrefab, GuiTransform));
    }
}